using UnityEngine;

public class PlayerData
{
    public string playerName;
    public int playerLevel = 0;

    public int gold = 0;
    public int food = 0; // tahil
    public Village.Data villageData;
    public int feudetoryId = -1;
    public int avatarId = -1;

    /// <summary>
    /// Is landing scene done
    /// </summary>
    public bool IsLanded => PlayerPrefs.GetString(PlayerPublicDataKey, "{}") != "{}";

    private const string PlayerPublicDataKey = "PlayerPublicData";

    public PlayerData() { }

    public PlayerData(string playerName)
    {
        this.playerName = playerName;
        // Yeni oyuncu için varsayýlan deðerleri atayabilirsiniz
        playerLevel = 1;
        gold = 1000;
        food = 500;
    }
}