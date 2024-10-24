using UnityEngine;

/// <summary>
/// Multiplayer data that is shared between all players
/// </summary>
public class PlayerPublicData
{
    public string username => LoginManager.Instance.LoginData.username; // for now
    public int feudetoryId = -1;
    public int avatarId = -1;

    /// <summary>
    /// Is landing scene done
    /// </summary>
    public bool IsLanded => feudetoryId != -1 && avatarId != -1;

    private const string PlayerPublicDataKey = "PlayerPublicData";

    public PlayerPublicData()
    {
        
    }

    /// <summary>
    /// Save data to player prefs
    /// </summary>
    /// <param name="data"></param>
    public static void SaveLocal(PlayerPublicData data)
    {
        PlayerPrefs.SetString(PlayerPublicDataKey, JsonUtility.ToJson(data));
    }

    /// <summary>
    /// Load data from player prefs
    /// </summary>
    /// <returns></returns>
    public static PlayerPublicData LoadLocal()
    {
        return JsonUtility.FromJson<PlayerPublicData>(PlayerPrefs.GetString(PlayerPublicDataKey, "{}"));
    }
}