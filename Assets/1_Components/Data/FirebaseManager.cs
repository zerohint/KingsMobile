using UnityEngine;
using Firebase;
using Firebase.Firestore;
using Firebase.Extensions;
using System;
using System.Collections.Generic;
using Game.Village;

[CreateAssetMenu(fileName = "FirebaseManager", menuName = "Game/Managers/Firebase Manager")]
public class FirebaseManager : SingletonSC<FirebaseManager>
{
    public FirebaseFirestore firestore;

    public event Action OnFirebaseInitialized;

    private void Start()
    {
        InitializeFirebase();
    }

    public void InitializeFirebase()
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
            Debug.LogError("Firestore henüz başlatılmadı!");
            return;
        }

        Dictionary<string, object> data = new Dictionary<string, object>
        {
            { FKeys.PLAYER_NAME, playerData.playerName },
            { FKeys.PLAYER_LEVEL, playerData.playerLevel },
            { FKeys.GOLD, playerData.gold },
            { FKeys.FOOD, playerData.food },
            { FKeys.VILLAGE_DATA, playerData.villageData.buildingsData }
        };

        firestore.Collection(FKeys.PLAYER_COLLECTION)
            .Document(playerData.playerName)
            .SetAsync(data)
            .ContinueWithOnMainThread(task =>
            {
                if (task.IsCompleted)
                {
                    Debug.Log("Player data Firestore'a kaydedildi.");
                    onComplete?.Invoke();
                }
                else
                {
                    Debug.LogError("Veri kaydetme hatası: " + task.Exception);
                }
            });
    }

    public void LoadPlayerData(string playerName, Action<PlayerData> onDataLoaded)
    {
        if (firestore == null)
        {
            Debug.LogError("Firestore henüz başlatılmadı!");
            return;
        }
        if (string.IsNullOrEmpty(playerName))
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

                        if (snapshot.ContainsField(FKeys.VILLAGE_DATA))
                        {
                            ret.villageData = new Village.Data
                            {
                                buildingsData = snapshot.GetValue<string[]>(FKeys.VILLAGE_DATA)
                            };
                        }
                        else
                        {
                            ret.villageData = new Village.Data();
                        }

                        Debug.Log("Player data yüklendi.");
                        onDataLoaded?.Invoke(ret);
                    }
                    else
                    {
                        Debug.LogWarning("Veritabanında veri bulunamadı, yeni veri oluşturuluyor.");
                        PlayerData newData = new PlayerData(playerName);
                        SavePlayerData(newData, () =>
                        {
                            Debug.Log("Yeni player data oluşturuldu ve kaydedildi.");
                            onDataLoaded?.Invoke(newData);
                        });
                    }
                }
                else
                {
                    Debug.LogError("Görev hatası: " + task.Exception);
                }
            });
    }

    public void SaveProductionOrder(ProductionOrder order, Action onComplete = null)
    {
        if (firestore == null)
        {
            Debug.LogError("Firestore not initialized yet!");
            return;
        }
        firestore.Collection("ProductionOrders")
            .Document(order.orderId)
            .SetAsync(order)
            .ContinueWithOnMainThread(task =>
            {
                if (task.IsCompleted)
                {
                    Debug.Log("Production order saved: " + order.orderId);
                    onComplete?.Invoke();
                }
                else
                {
                    Debug.LogError("Error saving production order: " + task.Exception);
                }
            });
    }


    public void RemoveProductionOrder(string orderId)
    {
        firestore.Collection("ProductionOrders")
            .Document(orderId)
            .DeleteAsync()
            .ContinueWithOnMainThread(task =>
            {
                if (task.IsCompleted)
                    Debug.Log("Production order removed: " + orderId);
                else
                    Debug.LogError("Error removing production order: " + task.Exception);
            });
    }

    public void LoadProductionOrders(Action<List<ProductionOrder>> onOrdersLoaded)
    {
        firestore.Collection("ProductionOrders")
            .GetSnapshotAsync()
            .ContinueWithOnMainThread(task =>
            {
                if (task.IsCompleted)
                {
                    var snapshot = task.Result;
                    List<ProductionOrder> orders = new List<ProductionOrder>();
                    foreach (var doc in snapshot.Documents)
                    {
                        ProductionOrder order = doc.ConvertTo<ProductionOrder>();
                        orders.Add(order);
                    }
                    onOrdersLoaded?.Invoke(orders);
                }
                else
                {
                    Debug.LogError("Error loading production orders: " + task.Exception);
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
        public const string VILLAGE_DATA = "villageData";
    }
}
