using UnityEngine;
using UnityEngine.UI;

namespace Game.Village
{
    public class MarketPanel : BuildingPanelBase
    {
        public override void UpdatePanel(BuildingBase building)
        {
            var market = building as Market;
            if (market != null)
            {

            }
        }
    }
}
