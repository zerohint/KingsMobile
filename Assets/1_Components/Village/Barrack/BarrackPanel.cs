using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


namespace Game.Village
{
    public class BarrackPanel : BuildingPanelBase
    {
        [SerializeField] private TMP_Text buildingNameText;
        [SerializeField] private GameObject soldierEntryPrefab;
        [SerializeField] private GameObject RightPanelObject;
        [SerializeField] private Transform soldierListContainer;

        
        private Barrack currentBarrack;
        private List<GameObject> spawnedSoldierEntries = new List<GameObject>();

        
        public override void UpdatePanel(BuildingBase building)
        {
            currentBarrack = building as Barrack;
            if (currentBarrack == null) return;

            buildingNameText.text = "K��la";
        }

        public void UpgradeBuilding()
        {
            PopupManager.Instance.ShowPopup(
                "Bu binay� y�kseltmek istedi�ine emin misin?",
                () => {
                    Debug.Log("Bina upgrade edildi!");
                    // Upgrade i�lemini burada yap
                },
                () => {
                    Debug.Log("Upgrade iptal edildi.");
                }
            );
        }
    }
}
