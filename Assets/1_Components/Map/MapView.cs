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
            //Write();
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
            feudatories = SCDB.GetAll<FeudatoryDataSC>().ToArray();

            foreach (var feudatory in feudatories)
            {
                for (int i = 0; i < feudatory.Castles.Length; i++)
                {
                    var castleData = feudatory.Castles[i];
                    Vector3 spawnPosition = CoordinateToV3(castleData.Coordinate);
                    var castleInstance = Instantiate(castlePrefab, spawnPosition, Quaternion.identity, entitiesParent);
                    castleInstance.name = $"{feudatory.Name} Castle {i}";
                }
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

        [ContextMenu("Write")]
        public void Write()
        {
            if (feudatories == null || waypointTransforms == null || feudatories.Length != waypointTransforms.Length)
            {
                Debug.LogError("Number of Feudatories and waypointTransforms do not match!");
                return;
            }

            for (int i = 0; i < waypointTransforms.Length; i++)
            {
                Transform waypoint = waypointTransforms[i];

                if (waypoint.childCount < 4)
                {
                    Debug.LogError($"Waypoint '{waypoint.name}' does not contain enough (4) children!");
                    continue;
                }

                for (int j = 0; j < 4; j++)
                {
                    Transform castleTransform = waypoint.GetChild(j);

                    if (feudatories[i].Castles != null && feudatories[i].Castles.Length > j)
                    {
                        feudatories[i].Castles[j].Coordinate = V3ToCoordinate(castleTransform.position);
                    }
                    else
                    {
                        Debug.LogError($"Feudatory Castles array for '{feudatories[i].Name}' is missing!");
                    }
                }
            }
        }


    }
}