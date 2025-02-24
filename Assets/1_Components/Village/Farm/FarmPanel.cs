using UnityEngine;
using UnityEngine.UI;

namespace Game.Village
{
    public class FarmPanel : BuildingPanelBase
    {
        public Farm CurrenFarm => Building as Farm;

        private void Awake()
        {
            Initialize(BuildingType.Farm);
        }

        public override void SetBuilding(BuildingBase building)
        {
            base.SetBuilding(building);
        }
    }
}
