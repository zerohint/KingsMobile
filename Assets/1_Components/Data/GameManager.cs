using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public string playerName;
    public int playerLevel;
    public int gold;
    public int food;

    public Dictionary<string, int> buildingLevels = new Dictionary<string, int>();
    public Dictionary<string, int> unitCounts = new Dictionary<string, int>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            LoadGame();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void UpdateBuildingLevel(string building, int level)
    {
        buildingLevels[building] = level;
        SaveGame();
    }

    public void UpdateUnitCount(string unit, int count)
    {
        unitCounts[unit] = count;
        SaveGame();
    }

    public void AddGold(int amount)
    {
        gold += amount;
        SaveGame();
    }

    public void AddFood(int amount)
    {
        food += amount;
        SaveGame();
    }

    public void SaveGame()
    {
        string json = JsonUtility.ToJson(this, true);
        File.WriteAllText(Application.persistentDataPath + "/gamedata.json", json);
    }

    public void LoadGame()
    {
        string path = Application.persistentDataPath + "/gamedata.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            JsonUtility.FromJsonOverwrite(json, this);
        }
        else
        {
            Debug.Log("No saved game found, initializing default values.");
            InitializeNewGame();
        }
    }

    private void InitializeNewGame()
    {
        playerName = "Player1";
        playerLevel = 1;
        gold = 1000;
        food = 500;

        buildingLevels.Clear();
        unitCounts.Clear();

        buildingLevels.Add("Arena", 1);
        buildingLevels.Add("Barracks", 1);
        buildingLevels.Add("Blacksmith", 1);
        buildingLevels.Add("Center", 1);
        buildingLevels.Add("ClanBuilding", 1);
        buildingLevels.Add("Council", 1);
        buildingLevels.Add("Farm", 1);
        buildingLevels.Add("Market", 1);
        buildingLevels.Add("Stable", 1);

        unitCounts.Add("soldier", 5);

        SaveGame();
    }
}
