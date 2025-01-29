using UnityEngine;
using UnityEngine.UI;

namespace Game.Village
{
    public class CouncilPanel : BuildingPanelBase
    {
        public override void UpdatePanel(BuildingBase building)
        {
            var council = building as Council;
            if (council != null)
            {

            }
        }
    }
}
