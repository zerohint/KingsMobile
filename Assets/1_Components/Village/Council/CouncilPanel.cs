using UnityEngine;
using TMPro;

namespace Game.Village
{
    public class CouncilPanel : BuildingPanelBase
    {
        public Council CurrentCouncil => Building as Council;

        private Council currentCouncil;
        private void Awake()
        {
            Initialize(BuildingType.Council);
        }
        public override void SetBuilding(BuildingBase building)
        {
            base.SetBuilding(building);
        }
    }
}
