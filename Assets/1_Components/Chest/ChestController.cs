using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase.Firestore;
using Firebase.Extensions;
using TMPro;

public class ChestController : MonoBehaviour
{
    [Header("UI References")]
    public Button chestButton;
    public TextMeshProUGUI chestButtonText;

    [Header("Chest Settings")]
    public bool hasKey = false;
    private TimeSpan cooldown = TimeSpan.FromHours(12);
    private DateTime lastChestOpenTime;

    public event Action ChestOpenRequested;

    private void Awake()
    {
        chestButton.onClick.AddListener(() =>
        {
            ChestOpenRequested?.Invoke();
        });
    }

    private void Start()
    {
        chestButton.interactable = false;
        FirebaseManager.Instance.OnFirebaseInitialized += OnFirebaseInitialized;
        if (FirebaseManager.Instance.firestore != null)
            OnFirebaseInitialized();

        ChestOpenRequested += OpenChest;
    }

    private void OnDestroy()
    {
        ChestOpenRequested -= OpenChest;
    }

    void OnFirebaseInitialized()
    {
        LoadChestData();
    }

    private void Update()
    {
        TimeSpan elapsed = DateTime.UtcNow - lastChestOpenTime;
        if (hasKey || elapsed >= cooldown)
        {
            chestButton.interactable = true;
            chestButtonText.text = "OPEN";
        }
        else
        {
            chestButton.interactable = false;
            TimeSpan remaining = cooldown - elapsed;
            chestButtonText.text = string.Format("{0:D2}:{1:D2}:{2:D2}", remaining.Hours, remaining.Minutes, remaining.Seconds);
        }
    }

    void OpenChest()
    {
        if (!chestButton.interactable)
            return;

        if (!hasKey)
        {
            lastChestOpenTime = DateTime.UtcNow;
            SaveChestData();
        }
        else
        {
            hasKey = false;
        }

        int itemLevel = GetRandomItem();
        PopupManager.Instance.ShowPopup(
            "Items that come out of the chest:   level " + itemLevel + " item",
            () => { }
        );

        chestButton.interactable = false;
    }

    int GetRandomItem()
    {
        float rand = UnityEngine.Random.Range(0f, 100f);
        if (rand < 70f)
            return 1;
        else if (rand < 70f + 28.91f)
            return 2;
        else if (rand < 70f + 28.91f + 0.99f)
            return 3;
        else if (rand < 70f + 28.91f + 0.99f + 0.09f)
            return 4;
        else
            return 5;
    }

    void LoadChestData()
    {
        string playerName = PlayersManager.Instance.playerData.playerName;
        if (string.IsNullOrEmpty(playerName))
        {
            Debug.LogError("Player name is empty, ChestData cannot be loaded!");
            return;
        }

        DocumentReference docRef = FirebaseManager.Instance.firestore
            .Collection(FirebaseManager.FKeys.PLAYER_COLLECTION)
            .Document(playerName)
            .Collection("ChestData")
            .Document("demo");

        docRef.GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                DocumentSnapshot snapshot = task.Result;
                if (snapshot.Exists && snapshot.ContainsField("lastChestOpenTime"))
                {
                    long ticks = snapshot.GetValue<long>("lastChestOpenTime");
                    lastChestOpenTime = new DateTime(ticks, DateTimeKind.Utc);
                }
                else
                {
                    lastChestOpenTime = DateTime.UtcNow - cooldown;
                }
            }
            else
            {
                Debug.LogError("Unable to load chest data: " + task.Exception);
                lastChestOpenTime = DateTime.UtcNow - cooldown;
            }
        });
    }

    void SaveChestData()
    {
        string playerName = PlayersManager.Instance.playerData.playerName;
        if (string.IsNullOrEmpty(playerName))
        {
            Debug.LogError("Player name is empty, data cannot be saved!");
            return;
        }

        DocumentReference docRef = FirebaseManager.Instance.firestore
            .Collection(FirebaseManager.FKeys.PLAYER_COLLECTION)
            .Document(playerName)
            .Collection("ChestData")
            .Document("demo");

        Dictionary<string, object> data = new Dictionary<string, object>
        {
            { "lastChestOpenTime", lastChestOpenTime.Ticks }
        };

        docRef.SetAsync(data).ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                Debug.Log("Chestdata updated successfully.");
            }
            else
            {
                Debug.LogError("Unable to save Chestdata: " + task.Exception);
            }
        });
    }
}
