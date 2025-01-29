using UnityEngine;
using UnityEngine.UI;

namespace Map
{
    public class Army : MapObjectBase
    {
        [SerializeField] private LayoutGroup starNest;
        [SerializeField] private ArmyStar armyStarPrefab;
        private ArmyData armyData;
        public override void OnPress()
        {
            Debug.Log("army pressed");
        }

        public void Visualize(int soldierCount)
        {

            for (int i = 0; i < soldierCount; i++)
            {
                var star = Instantiate(armyStarPrefab, starNest.transform);
            }
        }



        private static int GetStarCount(int asker)
        {
            return asker switch
            {
                <= 50 => 0,
                <= 200 => 1,
                <= 500 => 2,
                <= 1000 => 3,
                <= 5000 => 4,
                <= 20000 => 5,
                _ => -1
            };
        }
        public override PanelData GetPanelData()
        {
            return new PanelData
            {
                name = armyData.armyName,
                owner = armyData.armyLeader,
                level = armyData.armyLevel
            };
        }
        public ArmyData GetArmyData()
        {
            return armyData;
        }


        public class Data
        {
            public int soldierCount;


        }
    }

    [System.Serializable]
    public class ArmyData
    {
        public string armyName;
        public string armyLeader;
        public int armyLevel;
    }
}