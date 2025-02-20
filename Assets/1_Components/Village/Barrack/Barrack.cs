using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Village
{
    public class Barrack : BuildingBase
    {
        public override BuildingType BuildingType => BuildingType.Barrack;
        [SerializeField]
        private BuildingUpgradeData upgradeData;
        public BuildingUpgradeData UpgradeData => upgradeData;
        public List<SoldierInfo> AvailableSoldiers { get; private set; }
        [SerializeField] private List<SoldierIconData> soldierIcons;

        private void Start()
        {
            AvailableSoldiers = new List<SoldierInfo>
            {
                new SoldierInfo(SoldierType.Suvari, 35, GetSoldierIcon(SoldierType.Suvari)),
                new SoldierInfo(SoldierType.Yaya, 35, GetSoldierIcon(SoldierType.Yaya))
            };
        }
        private Sprite GetSoldierIcon(SoldierType type)
        {
            SoldierIconData data = soldierIcons.Find(s => s.type == type);
            return data.icon != null ? data.icon : default;
        }


        public override string GetData()
        {
            return JsonUtility.ToJson(new Data()
            {
                AvailableSoldiers = AvailableSoldiers,
                currentUpgradeStage = currentUpgradeStage
            });
        }

        public override void SetData(string dataString)
        {
            Data data = JsonUtility.FromJson<Data>(dataString);
            AvailableSoldiers = data.AvailableSoldiers;
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
            public List<SoldierInfo> AvailableSoldiers;
            public int currentUpgradeStage;
        }

        [Serializable]
        public class SoldierInfo
        {
            public SoldierType Type;
            public int Count;
            public Sprite Icon;

            public SoldierInfo(SoldierType type, int count, Sprite icon)
            {
                Type = type;
                Count = count;
                Icon = icon;
            }
        }
        [Serializable]
        public struct SoldierIconData
        {
            public SoldierType type;
            public Sprite icon;
        }
    }

    public enum SoldierType
    {
        Suvari,
        Yaya
    }
}
