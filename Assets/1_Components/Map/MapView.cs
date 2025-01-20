using Map;
using ZeroGame; 
using System.Linq;
using UnityEngine;

namespace Game.Map
{
    public class MapView : MonoBehaviour
    {
        [SerializeField] private Castle castlePrefab;
        [SerializeField] private Army armyPrefab;
        [Space]
        [SerializeField] private Transform entitiesParent;


        private FeudatoryDataSC[] feudatories;

        private void Start()
        {
            DrawEntities();
        }

        public Vector3 CoordinateToV3(Coordinate coordinate)
        {
            return transform.TransformPoint(coordinate.x, 0, coordinate.y);
        }

        public Coordinate V3ToCoordinate(Vector3 vector3)
        {
            Vector3 localPoint = transform.InverseTransformPoint(vector3);
            return new Coordinate(localPoint.x, localPoint.z);
        }

        /// <summary>
        /// Instantiate entities on the map
        /// </summary>
        private void DrawEntities()
        {
            // pooling etc.
            feudatories ??= SCDB.GetAll<FeudatoryDataSC>().ToArray();

            foreach (var feudatory in feudatories)
            {
                for(int i = 0; i< feudatory.Castles.Length; i++)
                {
                    var castle = feudatory.Castles[i];
                    var castleGO = Instantiate(castlePrefab, CoordinateToV3(castle.Coordinate), Quaternion.identity);
                    castleGO.transform.SetParent(entitiesParent);
                    castleGO.name = $"{feudatory.Name} Castle {i}";
                }
                break;
            }
        }


        public FeudatoryDataSC datatowrite;
        public Transform[] trans;
        [ContextMenu("Write")]  
        public void Write()
        {
            for(int i = 0; i<4; i++)
            {
                datatowrite.Castles[i].Coordinate = V3ToCoordinate(trans[i].position);
            }
        }
    }
}
