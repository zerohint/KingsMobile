using UnityEngine;
using Firebase;
using Firebase.Firestore;
using Firebase.Extensions;
using System;

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
            InitializeFirebase();
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
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

    public void SaveGameData(string jsonData, Action onComplete = null)
    {
        if (firestore == null)
        {
            Debug.LogError("Firestore henüz initialize edilmedi!");
            return;
        }

        // JSON verisini Firestore için Dictionary olarak parse et
        var data = new { json = jsonData };

        firestore.Collection("GameData").Document("SaveData").SetAsync(data)
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

    public void LoadGameData(Action<string> onDataLoaded)
    {
        if (firestore == null)
        {
            Debug.LogError("Firestore henüz initialize edilmedi!");
            return;
        }

        firestore.Collection("GameData").Document("SaveData").GetSnapshotAsync()
            .ContinueWithOnMainThread(task =>
            {
                if (task.IsCompleted)
                {
                    DocumentSnapshot snapshot = task.Result;
                    if (snapshot.Exists)
                    {
                        string jsonData = snapshot.GetValue<string>("json");
                        Debug.Log("Firestore'dan veriler çekildi: " + jsonData);
                        onDataLoaded?.Invoke(jsonData);
                    }
                    else
                    {
                        Debug.LogWarning("Firestore'da kayıtlı veri bulunamadı.");
                        onDataLoaded?.Invoke(null);
                    }
                }
                else
                {
                    Debug.LogError("Veri çekme hatası: " + task.Exception);
                }
            });
    }
}
