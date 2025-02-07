using UnityEngine;
using Firebase;
using Firebase.Firestore;
using Firebase.Extensions;
using System;
using System.Collections.Generic;

public class FirebaseManager : MonoBehaviour
{
    public static FirebaseManager Instance;
    public FirebaseFirestore firestore;

    public event Action OnFirebaseInitialized;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        InitializeFirebase();
    }

    private void InitializeFirebase()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            if (task.Result == DependencyStatus.Available)
            {
                firestore = FirebaseFirestore.DefaultInstance;
                Debug.Log("Firebase Firestore has been started successfully.");
                OnFirebaseInitialized?.Invoke();
            }
            else
            {
                Debug.LogError("Firebase dependency error:" + task.Result);
            }
        });
    }

    public void SavePlayerData(PlayerData playerData, Action onComplete = null)
    {
        if (firestore == null)
        {
            Debug.LogError("Firestore is not initialized yet!");
            return;
        }

        Dictionary<string, object> data = new Dictionary<string, object>
        {
            { FKeys.PLAYER_NAME, playerData.playerName },
            { FKeys.PLAYER_LEVEL, playerData.playerLevel },
            { FKeys.GOLD, playerData.gold },
            { FKeys.FOOD, playerData.food }
        };

        firestore.Collection(FKeys.PLAYER_COLLECTION)
            .Document(playerData.playerName)
            .SetAsync(data)
            .ContinueWithOnMainThread(task =>
            {
                if (task.IsCompleted)
                {
                    Debug.Log("Player data saved to Firestore.");
                    onComplete?.Invoke();
                }
                else
                {
                    Debug.LogError("Data saving error: " + task.Exception);
                }
            });
    }

    public void LoadPlayerData(string playerName, Action<PlayerData> onDataLoaded)
    {
        if (firestore == null)
        {
            Debug.LogError("Firestore is not initialized yet!");
            return;
        }
        if (playerName == null)
        {
            playerName = "Player#" + UnityEngine.Random.Range(111, 999);
        }
        firestore.Collection(FKeys.PLAYER_COLLECTION)
            .Document(playerName)
            .GetSnapshotAsync()
            .ContinueWithOnMainThread(task =>
            {
                if (task.IsCompleted)
                {
                    DocumentSnapshot snapshot = task.Result;
                    if (snapshot.Exists)
                    {
                        PlayerData ret = new PlayerData();
                        ret.playerName = snapshot.GetValue<string>(FKeys.PLAYER_NAME);
                        ret.playerLevel = snapshot.GetValue<int>(FKeys.PLAYER_LEVEL);
                        ret.gold = snapshot.GetValue<int>(FKeys.GOLD);
                        ret.food = snapshot.GetValue<int>(FKeys.FOOD);
                        Debug.Log("Player data loaded.");
                        onDataLoaded?.Invoke(ret);
                    }
                    else
                    {
                        Debug.LogWarning("No data found in the database, new data is being created.");

                        PlayerData newData = new PlayerData(playerName);
                        SavePlayerData(newData, () =>
                        {
                            Debug.Log("New player data has been created and saved.");
                            onDataLoaded?.Invoke(newData);
                        });
                    }
                }
                else
                {
                    Debug.LogError("Task error: " + task.Exception);
                }
            });
    }

    public static class FKeys
    {
        public const string PLAYER_COLLECTION = "PlayerData";

        public const string PLAYER_NAME = "playerName";

        public const string PLAYER_LEVEL = "playerLevel";

        public const string GOLD = "gold";

        public const string FOOD = "food";


    }
}
