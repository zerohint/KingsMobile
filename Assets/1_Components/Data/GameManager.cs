using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public PlayerData playerData = new PlayerData();

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
            Debug.LogError("FirebaseManager is not on scene");
        }
    }

    private void OnFirebaseReady()
    {
        FirebaseManager.Instance.LoadPlayerData(playerData.playerName, (data) =>
        {
            playerData = data;
        });
    }

    public void SaveGame()
    {
        FirebaseManager.Instance.SavePlayerData(playerData);
    }
}

