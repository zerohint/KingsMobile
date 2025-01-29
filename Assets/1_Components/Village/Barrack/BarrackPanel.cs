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
        [SerializeField] private Transform soldierListContainer;
        [SerializeField] private Button closeButton;

        private Barrack currentBarrack;
        private List<GameObject> spawnedSoldierEntries = new List<GameObject>();

        private void Start()
        {
            closeButton.onClick.AddListener(() => SetActive(false));
        }

        public override void UpdatePanel(BuildingBase building)
        {
            currentBarrack = building as Barrack;
            if (currentBarrack == null) return;

            buildingNameText.text = currentBarrack.name;
            RefreshSoldierList();
        }

        private void RefreshSoldierList()
        {
            // Önce eski askerleri temizle
            foreach (var entry in spawnedSoldierEntries)
            {
                Destroy(entry);
            }
            spawnedSoldierEntries.Clear();

            // Yeni askerleri oluþtur
            foreach (var soldier in currentBarrack.AvailableSoldiers)
            {
                GameObject entry = Instantiate(soldierEntryPrefab, soldierListContainer);
                SoldierEntryUI entryUI = entry.GetComponent<SoldierEntryUI>();
                entryUI.Setup(soldier, OnRecruitSoldier);
                spawnedSoldierEntries.Add(entry);
            }
        }

        private void OnRecruitSoldier(SoldierType soldier)
        {
            soldier.Count += 1;
            RefreshSoldierList();
        }
    }
}
