using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct GameData
{
    public string playerName;
    public int playerLevel;
    public Dictionary<string, int> buildingLevels; // Bina seviyeleri
    public Dictionary<string, int> unitCounts; // Asker sayýlarý
    public int gold;
    public int food;

    public GameData(string playerName)
    {
        this.playerName = playerName;
        this.playerLevel = 1;
        this.buildingLevels = new Dictionary<string, int>();
        this.unitCounts = new Dictionary<string, int>();
        this.gold = 1000;
        this.food = 500;
    }

    public void UpdateBuildingLevel(string building, int level)
    {
        if (buildingLevels.ContainsKey(building))
            buildingLevels[building] = level;
        else
            buildingLevels.Add(building, level);
    }

    public void UpdateUnitCount(string unit, int count)
    {
        if (unitCounts.ContainsKey(unit))
            unitCounts[unit] = count;
        else
            unitCounts.Add(unit, count);
    }
}
