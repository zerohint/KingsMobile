using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewBuildingUpgradeData", menuName = "Building/Upgrade Data")]
public class BuildingUpgradeData : ScriptableObject
{
    public List<UpgradeStage> upgradeStages;
}
