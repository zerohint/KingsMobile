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
        [SerializeField] private Castle castlePrefab;
        [SerializeField] private Army armyPrefab;
        [Space]
        [SerializeField] private Transform entitiesParent;
        // 19 tane waypoint referansý, Inspector üzerinden atanacak
        [SerializeField] private Transform[] waypointTransforms;
        
        private FeudatoryDataSC[] feudatories;
        
        public FirebaseFirestore firestore;

        private void Start()
        {
            firestore = FirebaseFirestore.DefaultInstance;
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
        /// Castle’leri instantiate eder.
        /// </summary>
        private void DrawEntities()
        {
            // pooling etc.
            feudatories ??= SCDB.GetAll<FeudatoryDataSC>().ToArray();

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
        /// Firestore’daki "Armies" koleksiyonunu okuyup, her bir Army dokümaný için prefab instantiate eder.
        /// </summary>
        private async void LoadArmiesFromFirestore()
        {
            try
            {
                QuerySnapshot snapshot = await firestore.Collection("Armies").GetSnapshotAsync();
                Debug.Log("Firestore sorgusu tamamlandý. Doküman sayýsý: " + snapshot.Count);

                foreach (DocumentSnapshot doc in snapshot.Documents)
                {
                    // Dokümandan ArmyFirestoreData modeline parse et
                    ArmyFirestoreData data = doc.ConvertTo<ArmyFirestoreData>();
                    Debug.Log("Ýþlenen doküman ID: " + doc.Id);

                    // Army prefab’ýný instantiate et
                    Army armyInstance = Instantiate(armyPrefab, entitiesParent);
                    // Firestore doküman referansýný gönderiyoruz
                    armyInstance.InitializeFromFirestore(data, waypointTransforms, doc.Reference);
                }
            }
            catch (System.Exception ex)
            {
                Debug.LogError("Armies koleksiyonu okunamadý: " + ex);
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

