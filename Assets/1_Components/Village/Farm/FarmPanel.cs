using UnityEngine;
using UnityEngine.UI;

namespace Game.Village
{
    public class FarmPanel : BuildingPanelBase
    {
        public override void UpdatePanel(BuildingBase building)
        {
            var farm = building as Farm;
            if (farm != null)
            {

            }
        }
    }
}
