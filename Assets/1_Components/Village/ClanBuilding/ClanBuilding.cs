using System;
using UnityEngine;

namespace Game.Village
{
    public class ClanBuilding : BuildingBase
    {
        public override BuildingType BuildingType => BuildingType.ClanBuilding;

        [SerializeField] private BuildingUpgradeData upgradeData;
        public BuildingUpgradeData UpgradeData => upgradeData;
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
        public override void Upgrade()
        {
            currentUpgradeStage++;
            Debug.Log("Barrack upgraded to stage " + currentUpgradeStage);
        }

        public override void OnPress()
        {
            ShowPanel();
        }

        [Serializable]
        private struct Data
        {
            public int currentUpgradeStage;
        }
    }

    [Serializable]
    public class Clan
    {
        public int ClanLevel;
        public int PlayerLimit;

        public Clan(int clanLevel, int playerLimit)
        {
            ClanLevel = clanLevel;
            PlayerLimit = playerLimit;
        }
    }


}
