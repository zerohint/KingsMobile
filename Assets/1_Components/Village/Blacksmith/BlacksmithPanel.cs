using UnityEngine;
using UnityEngine.UI;

namespace Game.Village
{
    public class BlacksmithPanel : BuildingPanelBase
    {
        public Blacksmith CurrentBlacksmith => Building as Blacksmith;

        private void Awake()
        {
            Initialize(BuildingType.Blacksmith);
        }

        public override void SetBuilding(BuildingBase building)
        {
            base.SetBuilding(building);
        }
    }
}
