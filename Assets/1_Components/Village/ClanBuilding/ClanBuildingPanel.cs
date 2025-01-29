using UnityEngine;
using UnityEngine.UI;

namespace Game.Village
{
    public class ClanBuildingPanel : BuildingPanelBase
    {
        public override void UpdatePanel(BuildingBase building)
        {
            var clanBuilding = building as ClanBuilding;
            if (clanBuilding != null)
            {

            }
        }
    }
}
