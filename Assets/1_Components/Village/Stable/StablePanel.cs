using UnityEngine;
using UnityEngine.UI;

namespace Game.Village
{
    public class StablePanel : BuildingPanelBase
    {
        public Stable CurrentStable => Building as Stable;

        private void Awake()
        {
            Initialize(BuildingType.Stable);
        }

        public override void SetBuilding(BuildingBase building)
        {
            base.SetBuilding(building);
        }
    }
}
