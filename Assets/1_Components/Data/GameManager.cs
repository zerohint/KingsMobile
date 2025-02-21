using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

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
            //FirebaseManager.Instance.OnFirebaseInitialized += OnFirebaseReady;
        }
        else
        {
            Debug.LogError("FirebaseManager is not on scene");
        }
    }

    private void OnFirebaseReady()
    {
        var pm = PlayersManager.Instance;
        FirebaseManager.Instance.LoadPlayerData(pm.playerData.playerName, (data) =>
        {
            pm.playerData = data;
            Debug.Log("GameManager: Player data loaded from Firebase.");
        });
    }

    public void SaveGame()
    {
        FirebaseManager.Instance.SavePlayerData(PlayersManager.Instance.playerData, () =>
        {
            Debug.Log("GameManager: Player data saved to Firebase.");
        });
    }
}
