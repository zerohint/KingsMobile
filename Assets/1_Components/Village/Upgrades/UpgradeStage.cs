using System;

[Serializable]
public class UpgradeStage
{
    // Sequence number or stage level of this stage (e.g. 1, 2, 3, ...)
    public int stageLevel;

    // Building level required for building upgrade (e.g. 10, 15, 20, ...)
    public int requiredBuildingLevel;

    // Cost information
    public int gemCost;
    public int grainCost;
    public int coinCost;
}
