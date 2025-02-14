using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase.Firestore;
using Firebase.Extensions;
using Game.Map;

namespace Map
{

    [System.Serializable]
    public class Feudatory
    {
        public string feudatoryName;
        public Transform location;  
    }

    public class Army : MapObjectBase
    {
        [SerializeField] private LayoutGroup starNest;
        [SerializeField] private ArmyStar armyStarPrefab;

        [SerializeField] private float distanceTimeMultiplier = 60f;

        private ArmyData armyData;
        private Transform[] waypoints;
        private Transform startWaypoint;
        private Transform destinationWaypoint;

        private DateTime lastUpdateTime; 
        private DateTime startTime;      
        private float journeyDuration;   

        private float progress;

        private DocumentReference firestoreDocRef;
        private float lastFirestoreUpdateTime = 0f;
        private const float firestoreUpdateInterval = 1200f;

        #region Unity Lifecycle

        public override void OnPress()
        {
            PanelManager.Instance.ShowPanel(PanelType.Army);
            
            int startIndex = Array.IndexOf(waypoints, startWaypoint);
            int destinationIndex = Array.IndexOf(waypoints, destinationWaypoint);

            if (startIndex < 0 || destinationIndex < 0)
            {
                Debug.LogError("No start or destination found in Waypoints array!");
                return;
            }
            
            FeudatoryDataSC[] feudatories = MapView.Instance.feudatories;


            FeudatoryDataSC startFeudatory = feudatories[startIndex];
            FeudatoryDataSC destinationFeudatory = feudatories[destinationIndex];
            
            ArmyPanel armyPanel = PanelManager.Instance.GetPanel<ArmyPanel>(PanelType.Army);
            if (armyPanel != null)
            {
                armyPanel.UpdateArmyPanelFeudatories(startFeudatory, destinationFeudatory, progress, journeyDuration, armyData);
            }
        }

        private void Update()
        {
            if (startWaypoint == null || destinationWaypoint == null) return;

            DateTime currentTime = DateTime.UtcNow;
            float elapsedTime = (float)(currentTime - startTime).TotalSeconds;
            progress = Mathf.Clamp01(elapsedTime / journeyDuration);
            transform.position = Vector3.Lerp(startWaypoint.position, destinationWaypoint.position, progress);

            if (Time.time - lastFirestoreUpdateTime > firestoreUpdateInterval)
            {
                lastFirestoreUpdateTime = Time.time;
                UpdateProgressInFirestore();
            }

            if (progress >= 1f)
            {
                ChooseNewDestination();
            }
        }

        #endregion

        #region Public Methods

        public override PanelData GetPanelData() => new PanelData
        {
            name = armyData.armyName,
            owner = armyData.armyLeader,
            level = armyData.armyLevel
        };

        public ArmyData GetArmyData() => armyData;

        public void Visualize(int soldierCount)
        {
            for (int i = 0; i < soldierCount; i++)
            {
                Instantiate(armyStarPrefab, starNest.transform);
            }
        }

        /// <summary>
        /// Initializes Army with data from Firestore.
        /// </summary>
        /// <param name="data">Firestore data</param>
        /// <param name="waypointArray">Waypoint array</param>
        /// <param name="docRef">Firestore Document reference</param>
        public void InitializeFromFirestore(ArmyFirestoreData data, Transform[] waypointArray, DocumentReference docRef)
        {
            armyData = new ArmyData
            {
                armyName = data.armyName,
                armyLeader = data.armyLeader,
                armyLevel = data.armyLevel
            };

            Visualize(data.soldierCount);
            progress = data.progress;
            waypoints = waypointArray;
            firestoreDocRef = docRef;

            if (waypointArray == null || waypointArray.Length == 0)
            {
                Debug.LogError("Waypoint array is empty!");
                return;
            }

            int curIndex = Mathf.Clamp(data.currentWaypoint, 0, waypointArray.Length - 1);
            int targetIndex = Mathf.Clamp(data.targetWaypoint, 0, waypointArray.Length - 1);
            startWaypoint = waypointArray[curIndex];
            destinationWaypoint = waypointArray[targetIndex];

            transform.position = startWaypoint.position;

            lastUpdateTime = data.lastUpdateTime.ToDateTime();
            startTime = lastUpdateTime;
            journeyDuration = data.travelTime;


            Debug.Log($"Army loaded from Firestore. Start Time: {startTime}, Duration: {journeyDuration} sec");
        }

        #endregion

        #region Private Methods

        private void ChooseNewDestination()
        {
            startWaypoint = destinationWaypoint;
            int newTargetIndex = UnityEngine.Random.Range(0, waypoints.Length);
            while (waypoints[newTargetIndex] == startWaypoint)
            {
                newTargetIndex = UnityEngine.Random.Range(0, waypoints.Length);
            }
            destinationWaypoint = waypoints[newTargetIndex];

            startTime = DateTime.UtcNow;
            lastUpdateTime = startTime;
            progress = 0f;
            
            float distance = Vector3.Distance(startWaypoint.position, destinationWaypoint.position);
            journeyDuration = distance * distanceTimeMultiplier;
            Debug.Log($"New target selected. Distance: {distance:F2}, Duration: {journeyDuration:F2} sec");

            UpdateMovementInFirestore(newTargetIndex);
        }

        private void UpdateProgressInFirestore()
        {
            if (firestoreDocRef == null) return;

            firestoreDocRef.UpdateAsync("progress", progress)
                .ContinueWithOnMainThread(task =>
                {
                    if (task.IsCompleted)
                        Debug.Log("Progress updated: " + progress);
                    else
                        Debug.LogError("Progress update error: " + task.Exception);
                });
        }

        private void UpdateMovementInFirestore(int newTargetIndex)
        {
            if (firestoreDocRef == null) return;

            int currentIndex = Array.IndexOf(waypoints, startWaypoint);
            var updates = new Dictionary<string, object>
            {
                { "currentWaypoint", currentIndex },
                { "targetWaypoint", newTargetIndex },
                { "progress", progress },
                { "lastUpdateTime", Timestamp.FromDateTime(DateTime.UtcNow) },
                { "travelTime", journeyDuration }
            };

            firestoreDocRef.UpdateAsync(updates)
                .ContinueWithOnMainThread(task =>
                {
                    if (task.IsCompleted)
                        Debug.Log("Movement data updated.");
                    else
                        Debug.LogError("Movement data update error: " + task.Exception);
                });
        }

        #endregion
    }

    [Serializable]
    public class ArmyData
    {
        public string armyName;
        public string armyLeader;
        public int armyLevel;
    }

    [FirestoreData]
    public class ArmyFirestoreData
    {
        [FirestoreProperty] public string armyName { get; set; }
        [FirestoreProperty] public string armyLeader { get; set; }
        [FirestoreProperty] public int armyLevel { get; set; }
        [FirestoreProperty] public int soldierCount { get; set; }
        [FirestoreProperty] public float progress { get; set; }
        [FirestoreProperty] public int currentWaypoint { get; set; }
        [FirestoreProperty] public int targetWaypoint { get; set; }
        [FirestoreProperty] public Timestamp lastUpdateTime { get; set; }
        [FirestoreProperty] public float travelTime { get; set; }
    }
}
