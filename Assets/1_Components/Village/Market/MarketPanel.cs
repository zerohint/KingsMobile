using UnityEngine;
using UnityEngine.UI;

namespace Game.Village
{
    public class MarketPanel : BuildingPanelBase
    {
        public Market CurrentMarket => Building as Market;

        private void Awake()
        {
            Initialize(BuildingType.Market);
        }

        public override void SetBuilding(BuildingBase building)
        {
            base.SetBuilding(building);
        }

    }
}
