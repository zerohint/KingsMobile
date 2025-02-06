using UnityEngine;
using TMPro;
using System.Collections.Generic;

namespace Game.Village
{
    public class ClanBuildingPanel : BuildingPanelBase
    {
        [SerializeField] private TextMeshProUGUI buildingNameText;
        [SerializeField] private TextMeshProUGUI clanStatusText;
        [SerializeField] private GameObject clanListContainer;
        [SerializeField] private GameObject clanEntryPrefab;

        private void OnEnable()
        {
            UpdateView();
        }
        private void Awake()
        {
            Initialize(BuildingType.ClanBuilding);
        }

        public void UpdateView()
        {
            buildingNameText.text = "Clan";
        }
    }
}
