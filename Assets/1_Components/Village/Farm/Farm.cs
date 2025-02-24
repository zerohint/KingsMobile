using System;
using UnityEngine;

namespace Game.Village
{
    public class Farm : BuildingBase
    {
        public override BuildingType BuildingType => BuildingType.Farm;
        [SerializeField] private BuildingUpgradeData upgradeData;
        public BuildingUpgradeData UpgradeData => upgradeData;

        public override void OnPress()
        {
            ShowPanel();
        }

        public override string GetData()
        {
            return JsonUtility.ToJson(new Data()
            {
                currentUpgradeStage = currentUpgradeStage
            });
        }

        public override void SetData(string dataString)
        {
            Data data = JsonUtility.FromJson<Data>(dataString);
            currentUpgradeStage = data.currentUpgradeStage;
        }

        public override UpgradeStage GetNextUpgradeStage()
        {
            int nextStageLevel = currentUpgradeStage + 1;
            return upgradeData.upgradeStages.Find(s => s.stageLevel == nextStageLevel);
        }
        public override void Upgrade()
        {
            currentUpgradeStage++;
            Debug.Log("Farm upgraded to stage " + currentUpgradeStage);
        }

        [Serializable]
        private struct Data
        {
            public int currentUpgradeStage;
        }
    }
}