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
        private void Awake()
        {
            Initialize(BuildingType.Council);
        }
        public void UpdateView()
        {
            buildingNameText.text = "Council";
        }
    }
}
