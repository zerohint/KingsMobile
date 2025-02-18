using UnityEngine;
using System;
using Game.Village;

[Serializable]
public class PlayerData
{
    public string playerName;
    public int playerLevel = 0;
    public int gold = 0;
    public int food = 0;
    public Village.Data villageData;
    public int feudetoryId = -1;
    public int avatarId = -1;

    public bool IsLanded => PlayerPrefs.GetString(PlayerPublicDataKey, "{}") != "{}";
    private const string PlayerPublicDataKey = "PlayerPublicData";

    public PlayerData()
    {
        villageData = new Village.Data();
    }

    public PlayerData(string playerName)
    {
        this.playerName = playerName;
        playerLevel = 1;
        gold = 1000;
        food = 500;
        villageData = new Village.Data();
    }
}
