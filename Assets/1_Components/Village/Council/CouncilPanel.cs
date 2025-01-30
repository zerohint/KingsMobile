using UnityEngine;
using TMPro;

namespace Game.Village
{
    public class CouncilPanel : BuildingPanelBase
    {
        [SerializeField] private TextMeshProUGUI buildingNameText;
        public override void UpdatePanel(BuildingBase building)
        {
            var council = building as Council;
            if (council != null) return;

            buildingNameText.text = "Divan";
        }
    }
}
