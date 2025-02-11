using System;

[Serializable]
public class UpgradeStage
{
    // Bu aþamanýn sýra numarasý veya aþama seviyesi (ör. 1, 2, 3, ...)
    public int stageLevel;

    // Bina yükseltmesi için gereken bina seviyesi (ör. 10, 15, 20, ...)
    public int requiredBuildingLevel;

    // Maliyet bilgileri
    public int gemCost;   // Zümrüt maliyeti
    public int grainCost; // Tahýl maliyeti
    public int coinCost;  // Sikke maliyeti
}
