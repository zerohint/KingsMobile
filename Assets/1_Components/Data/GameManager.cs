using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public PlayerData playerData = new();


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
        if (FirebaseManager.Instance != null)
        {
            FirebaseManager.Instance.OnFirebaseInitialized += OnFirebaseReady;
        }
        else
        {
            Debug.LogError("FirebaseManager sahnede yok!");
        }
    }


    private void OnFirebaseReady()
    {
        FirebaseManager.Instance.LoadPlayerData((data) =>
        {
            playerData = data;
        });
    }

    

    public void SaveGame()
    {
        FirebaseManager.Instance.SavePlayerData(playerData);
    }
}


public class PlayerData
{
    public string playerName;
    public int playerLevel = 0;
    public int gold = 0;
    public int food = 0;

    public PlayerData()
    {
        Random();
    }

    public PlayerData(string playerName)
    {
        this.playerName = playerName;
    }

    public void Random()
    {
        playerName = "Player#" + 123/*UnityEngine.Random.Range(111, 999)*/;
        playerLevel = 1;
        gold = 1000;
        food = 500;
    }
}