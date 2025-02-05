using UnityEngine;
using Firebase.Firestore;
using Firebase.Extensions;
using System.Collections.Generic;
using System;
using UnityEngine.UI;

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
        private float journeyDuration;   // Hareket s�resi (ger�ek d�nya zaman� olarak)

        private float progress;
        [SerializeField] private float speedMultiplier = 0.1f; // Ekstra h�z �arpan�

        private DocumentReference firestoreDocRef;
        private float lastFirestoreUpdateTime = 0f; // Son Firestore g�ncelleme zaman�
        private const float firestoreUpdateInterval = 1200f; // 20 dakika (saniye cinsinden)

        public override void OnPress()
        {
            Debug.Log("Army pressed");
        }

        public void Visualize(int soldierCount)
        {
            for (int i = 0; i < soldierCount; i++)
            {
                Instantiate(armyStarPrefab, starNest.transform);
            }
        }

        public override PanelData GetPanelData()
        {
            return new PanelData
            {
                name = armyData.armyName,
                owner = armyData.armyLeader,
                level = armyData.armyLevel
            };
        }

        public ArmyData GetArmyData()
        {
            return armyData;
        }

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

            // Firestore'dan gelen son g�ncelleme zaman�n� al
            lastUpdateTime = data.lastUpdateTime.ToDateTime();
            startTime = lastUpdateTime;
            journeyDuration = data.travelTime; // Ger�ek d�nya s�resi (saniye olarak)

            Debug.Log($"Army loaded from Firestore. Start Time: {startTime}, Duration: {journeyDuration} sec");
        }

        private void Update()
        {
            if (startWaypoint == null || destinationWaypoint == null) return;

            // �u anki zaman� al
            DateTime currentTime = DateTime.UtcNow;
            float elapsedTime = (float)(currentTime - startTime).TotalSeconds;

            // Progress hesapla
            progress = Mathf.Clamp01(elapsedTime / journeyDuration);

            // Unity i�indeki pozisyonu g�ncelle
            transform.position = Vector3.Lerp(startWaypoint.position, destinationWaypoint.position, progress);

            // 20 dakikada bir Firestore'a g�ncelleme g�nder
            if (Time.time - lastFirestoreUpdateTime > firestoreUpdateInterval)
            {
                lastFirestoreUpdateTime = Time.time;
                UpdateProgressInFirestore();
            }

            // E�er yolculuk tamamland�ysa yeni hedef se�
            if (progress >= 1f)
            {
                ChooseNewDestination();
            }
        }

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

            // Yeni yolculuk s�resini belirleyelim (g�nler s�recek hareket i�in)
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
    }

    [System.Serializable]
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
