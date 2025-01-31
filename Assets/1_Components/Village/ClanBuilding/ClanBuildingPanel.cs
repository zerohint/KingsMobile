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

        public override void UpdatePanel(BuildingBase building)
        {
            ClanBuilding clanBuilding = building as ClanBuilding;
            if (clanBuilding == null) return;

            buildingNameText.text = "Te�kilat";  // Sabit isim, istersen de�i�tirilebilir.

            // �nce eski giri�leri temizle
            foreach (Transform child in clanListContainer.transform)
            {
                Destroy(child.gameObject);
            }
        }
    }
}
