using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Village
{
    public class Barrack : BuildingBase
    {
        public override BuildingType BuildingType => BuildingType.Barrack;

        [SerializeField] private BuildingUpgradeData upgradeData;
        public BuildingUpgradeData UpgradeData => upgradeData;

        [SerializeField] private List<SoldierData> availableSoldiers;
        public List<SoldierData> AvailableSoldiers => availableSoldiers;

        private void Start()
        {
            if (availableSoldiers == null || availableSoldiers.Count == 0)
            {
                availableSoldiers = new List<SoldierData>(Resources.LoadAll<SoldierData>("Soldiers"));
            }
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

        public override void OnPress()
        {
            ShowPanel();
            BarrackPanel barrackPanel = FindObjectOfType<BarrackPanel>();
            if (barrackPanel != null)
            {
                barrackPanel.SetBuilding(this);
                barrackPanel.SetActive(true);
            }
        }

        public override string GetUpgradeInfo()
        {
            int nextStageLevel = currentUpgradeStage + 1;
            UpgradeStage nextStage = upgradeData.upgradeStages.Find(s => s.stageLevel == nextStageLevel);
            if (nextStage != null)
            {
                return $"Level {nextStage.stageLevel} Upgrade:\n" +
                       $"Required building level: {nextStage.requiredBuildingLevel}\n" +
                       $"Gem: {nextStage.gemCost}\n" +
                       $"Grain: {nextStage.grainCost}\n" +
                       $"Coin: {nextStage.coinCost}";
            }
            return "No further upgrades available.";
        }

        [Serializable]
        private struct Data
        {
            public int currentUpgradeStage;
        }
    }
}