using Map;
using Firebase.Firestore;
using Firebase.Extensions;
using System.Collections.Generic;
using UnityEngine;
using ZeroGame;
using System.Linq;

namespace Game.Map
{
    public class MapView : MonoBehaviour
    {
        public static MapView Instance { get; private set; }
        [SerializeField] private Castle castlePrefab;
        [SerializeField] private Army armyPrefab;
        [Space]
        [SerializeField] private Transform entitiesParent;
        [SerializeField] private Transform[] waypointTransforms;

        public FeudatoryDataSC[] feudatories;

        public FirebaseFirestore firestore;

        private void Awake()
        {
            Instance = this;
            firestore = FirebaseFirestore.DefaultInstance;
        }

        private void Start()
        {
            DrawEntities();
        }

        private void OnEnable()
        {
            LoadArmiesFromFirestore();
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
        /// Castle Instantiate
        /// </summary>
        private void DrawEntities()
        {
            // pooling etc.
            feudatories = SCDB.GetAll<FeudatoryDataSC>().ToArray();

            foreach (var feudatory in feudatories)
            {
                for (int i = 0; i < feudatory.Castles.Length; i++)
                {
                    var castle = feudatory.Castles[i];
                    var castleGO = Instantiate(castlePrefab, CoordinateToV3(castle.Coordinate), Quaternion.identity);
                    castleGO.transform.SetParent(entitiesParent);
                    castleGO.name = $"{feudatory.Name} Castle {i}";
                }
                break;
            }
        }

        /// <summary>
        /// Army Instantiate from firestore
        /// </summary>
        private async void LoadArmiesFromFirestore()
        {
            try
            {
                QuerySnapshot snapshot = await firestore.Collection("Armies").GetSnapshotAsync();
                Debug.Log("Firestore query completed. Document count: " + snapshot.Count);

                foreach (DocumentSnapshot doc in snapshot.Documents)
                {
                    ArmyFirestoreData data = doc.ConvertTo<ArmyFirestoreData>();
                    Debug.Log("Processed document ID: " + doc.Id);

                    Army armyInstance = Instantiate(armyPrefab, entitiesParent);
                    armyInstance.InitializeFromFirestore(data, waypointTransforms, doc.Reference);
                }
            }
            catch (System.Exception ex)
            {
                Debug.LogError("Armies collection could not be read: " + ex);
            }
        }

        public FeudatoryDataSC datatowrite;
        public Transform[] trans;
        [ContextMenu("Write")]
        public void Write()
        {
            for (int i = 0; i < 4; i++)
            {
                datatowrite.Castles[i].Coordinate = V3ToCoordinate(trans[i].position);
            }
        }
    }
}