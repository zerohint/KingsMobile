using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public string playerName;
    public int playerLevel;
    public int gold;
    public int food;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            // FirebaseManager haz�r oldu�unda veriyi y�kle
            if (FirebaseManager.Instance != null)
            {
                FirebaseManager.Instance.OnFirebaseInitialized += OnFirebaseReady;
            }
            else
            {
                Debug.LogError("FirebaseManager sahnede yok!");
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnFirebaseReady()
    {
        FirebaseManager.Instance.LoadGameData(OnGameDataLoaded);
    }

    private void OnGameDataLoaded(string json)
    {
        if (!string.IsNullOrEmpty(json))
        {
            JsonUtility.FromJsonOverwrite(json, this);
            Debug.Log("Oyun verileri y�klendi.");
        }
        else
        {
            Debug.Log("Firestore'da kay�tl� oyun verisi bulunamad�. Varsay�lan de�erler kullan�lacak.");
            InitializeDefaultValues();
        }
    }

    private void InitializeDefaultValues()
    {
        playerName = "Player1";
        playerLevel = 1;
        gold = 1000;
        food = 500;
        SaveGame();
    }

    public void SaveGame()
    {
        string json = JsonUtility.ToJson(this, true);
        FirebaseManager.Instance.SaveGameData(json);
    }
}
