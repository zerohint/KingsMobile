using System;

[Serializable]
public class UpgradeStage
{
    // Bu a�aman�n s�ra numaras� veya a�ama seviyesi (�r. 1, 2, 3, ...)
    public int stageLevel;

    // Bina y�kseltmesi i�in gereken bina seviyesi (�r. 10, 15, 20, ...)
    public int requiredBuildingLevel;

    // Maliyet bilgileri
    public int gemCost;   // Z�mr�t maliyeti
    public int grainCost; // Tah�l maliyeti
    public int coinCost;  // Sikke maliyeti
}
