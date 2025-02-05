using UnityEngine;
using Firebase;
using Firebase.Firestore;
using Firebase.Extensions;
using System;
using System.Threading.Tasks;
using Google.MiniJSON;

public class FirebaseManager : MonoBehaviour
{
    public static FirebaseManager Instance;
    public FirebaseFirestore firestore;

    // Firebase hazır olduğunda tetiklenecek event
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
        //LoadArmiesFromFirestore();
    }

    void LoadArmiesFromFirestore()
    {
        FirebaseFirestore db = FirebaseFirestore.DefaultInstance;
        CollectionReference armiesRef = db.Collection("Armies");

        armiesRef.GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted || task.IsCanceled)
            {
                Debug.LogError("Firestore Query Error: " + task.Exception);
                return;
            }

            QuerySnapshot snapshot = task.Result;
            Debug.Log("Found " + snapshot.Count + " armies in Firestore.");

            foreach (DocumentSnapshot doc in snapshot.Documents)
            {
                Debug.Log($"Army ID: {doc.Id}");

                foreach (var field in doc.ToDictionary())
                {
                    Debug.Log($"{field.Key}: {field.Value}");
                }
            }
        });
    }

    private void InitializeFirebase()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            if (task.Result == DependencyStatus.Available)
            {
                firestore = FirebaseFirestore.DefaultInstance;
                Debug.Log("Firebase Firestore başarıyla başlatıldı.");
                OnFirebaseInitialized?.Invoke();
            }
            else
            {
                Debug.LogError("Firebase bağımlılık hatası: " + task.Result);
            }
        });
    }

    public void SavePlayerData(PlayerData playerData, Action onComplete = null)
    {
        if (firestore == null)
        {
            Debug.LogError("Firestore henüz initialize edilmedi!");
            return;
        }

        // TODO: kalkacak
        var data = new { json = JsonUtility.ToJson(playerData, true) };

        firestore.Collection(FKeys.GAME_DATA).Document(FKeys.SAVE_DATA).SetAsync(data)
            .ContinueWithOnMainThread(task =>
            {
                if (task.IsCompleted)
                {
                    Debug.Log("Game data Firestore'a kaydedildi.");
                    onComplete?.Invoke();
                }
                else
                {
                    Debug.LogError("Veri kaydetme hatası: " + task.Exception);
                }
            });
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="onDataLoaded"></param>
    public void LoadPlayerData(Action<PlayerData> onDataLoaded)
    {
        if (firestore == null)
        {
            Debug.LogError("Firestore henüz initialize edilmedi!");
            return;
        }

        firestore.Collection(FKeys.GAME_DATA).Document(FKeys.SAVE_DATA).GetSnapshotAsync()
            .ContinueWithOnMainThread(task =>
            {
                PlayerData ret = new();
                if (task.IsCompleted)
                {
                    DocumentSnapshot snapshot = task.Result;
                    if (snapshot.Exists)
                    {
                        string jsonData = snapshot.GetValue<string>(FKeys.JSON);
                        if (!string.IsNullOrEmpty(jsonData)) JsonUtility.FromJsonOverwrite(jsonData, ret);
                    }
                    else
                    {
                        Debug.LogWarning("No data on db, new data created.");
                    }
                    onDataLoaded?.Invoke(ret);
                }
                else
                {
                    AlertPanel.Alert("Task error: " + task.Exception);
                }
            });
    }

   

    public static class FKeys
    {
        // Tables
        public const string GAME_DATA = "GameData";

        // Documents
        public const string SAVE_DATA = "SaveData";

        // Keys
        public const string JSON = "json";
    }
}


