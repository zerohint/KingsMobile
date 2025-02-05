using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase.Firestore;
using Firebase.Extensions;

namespace Map
{
    public class Army : MapObjectBase
    {
        [SerializeField] private LayoutGroup starNest;
        [SerializeField] private ArmyStar armyStarPrefab;

        private ArmyData armyData;
        private Transform[] waypoints;
        private Transform startWaypoint;
        private Transform destinationWaypoint;

        private DateTime lastUpdateTime; // Firestore'dan �ekilen son g�ncelleme zaman�
        private DateTime startTime;      // Hareketin ba�lang�� zaman�
        private float journeyDuration;   // Ger�ek d�nya s�resi (saniye cinsinden)

        private float progress;
        [SerializeField] private float speedMultiplier = 0.1f; // Ekstra h�z �arpan�

        private DocumentReference firestoreDocRef;
        private float lastFirestoreUpdateTime = 0f; // Son Firestore g�ncelleme zaman�
        private const float firestoreUpdateInterval = 1200f; // 20 dakika (saniye)

        #region Unity Lifecycle

        public override void OnPress() => Debug.Log("Army pressed");

        private void Update()
        {
            if (startWaypoint == null || destinationWaypoint == null) return;

            DateTime currentTime = DateTime.UtcNow;
            float elapsedTime = (float)(currentTime - startTime).TotalSeconds;
            progress = Mathf.Clamp01(elapsedTime / journeyDuration);
            transform.position = Vector3.Lerp(startWaypoint.position, destinationWaypoint.position, progress);

            // 20 dakikada bir Firestore g�ncellemesi g�nder
            if (Time.time - lastFirestoreUpdateTime > firestoreUpdateInterval)
            {
                lastFirestoreUpdateTime = Time.time;
                UpdateProgressInFirestore();
            }

            // Hareket tamamland�ysa yeni hedef belirle
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
        /// Firestore'dan gelen verilerle Army'yi initialize eder.
        /// </summary>
        /// <param name="data">Firestore verileri</param>
        /// <param name="waypointArray">Waypoint dizisi</param>
        /// <param name="docRef">Firestore Document referans�</param>
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
                Debug.LogError("Waypoint dizisi bo�!");
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
            journeyDuration = UnityEngine.Random.Range(86400f, 259200f); // 1 ila 3 g�n aras�
            progress = 0f;

            UpdateMovementInFirestore(newTargetIndex);
        }

        private void UpdateProgressInFirestore()
        {
            if (firestoreDocRef == null) return;

            firestoreDocRef.UpdateAsync("progress", progress)
                .ContinueWithOnMainThread(task =>
                {
                    if (task.IsCompleted)
                        Debug.Log("Progress g�ncellendi: " + progress);
                    else
                        Debug.LogError("Progress g�ncelleme hatas�: " + task.Exception);
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
                        Debug.Log("Hareket verileri g�ncellendi.");
                    else
                        Debug.LogError("Hareket verisi g�ncelleme hatas�: " + task.Exception);
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
