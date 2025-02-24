using UnityEngine;
using UnityEngine.UI;

namespace Game.Village
{
    public class CenterPanel : BuildingPanelBase
    {
        public Center CurrentCenter => Building as Center;

        private void Awake()
        {
            Initialize(BuildingType.Center);
        }

        public override void SetBuilding(BuildingBase building)
        {
            base.SetBuilding(building);
        }
    }
}
