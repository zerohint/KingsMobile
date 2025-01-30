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

            buildingNameText.text = "Teþkilat";  // Sabit isim, istersen deðiþtirilebilir.
            clanStatusText.text = clanBuilding.IsInClan ? $"Bulunduðun Teþkilat: {clanBuilding.ClanName}" : "Henüz bir teþkilatta deðilsin";

            // Önce eski giriþleri temizle
            foreach (Transform child in clanListContainer.transform)
            {
                Destroy(child.gameObject);
            }

            // Mevcut teþkilatlarý listele
            foreach (var clan in clanBuilding.AvailableClans)
            {
                GameObject entry = Instantiate(clanEntryPrefab, clanListContainer.transform);
            }
        }
    }
}
