using UnityEngine;
using UnityEngine.UI;

namespace Game.Village
{
    public class BlacksmithPanel : BuildingPanelBase
    {


        public override void UpdatePanel(BuildingBase building)
        {
            var blacksmithPanel = building as Blacksmith;
            if (blacksmithPanel != null)
            {
                
            }
        }
    }
}
