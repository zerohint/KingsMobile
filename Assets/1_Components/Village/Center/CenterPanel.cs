using UnityEngine;
using UnityEngine.UI;

namespace Game.Village
{
    public class CenterPanel : BuildingPanelBase
    {
        public override void UpdatePanel(BuildingBase building)
        {
            var centerPanel = building as Center;
            if (centerPanel != null)
            {

            }
        }
    }
}
