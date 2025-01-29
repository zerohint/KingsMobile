using UnityEngine;
using UnityEngine.UI;

namespace Game.Village
{
    public class StablePanel : BuildingPanelBase
    {
        public override void UpdatePanel(BuildingBase building)
        {
            var stable = building as Stable;
            if (stable != null)
            {

            }
        }
    }
}
