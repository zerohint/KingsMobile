using UnityEngine;
using TMPro;

namespace Game.Village
{
    public class CouncilPanel : BuildingPanelBase
    {
        [SerializeField] private TextMeshProUGUI buildingNameText;
        private void OnEnable()
        {
            UpdateView();
        }

        public void UpdateView()
        {
            buildingNameText.text = "Council";
        }
    }
}
