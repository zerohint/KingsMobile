using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Game.Village
{
    public class BarrackPanel : BuildingPanelBase
    {
        [SerializeField] private GameObject soldierEntryPrefab;
        [SerializeField] private Transform soldierListContainer;
        [SerializeField] private Transform productionQueueContainer;
        [SerializeField] private GameObject productionQueueIconPrefab; 

        private List<GameObject> spawnedSoldierEntries = new List<GameObject>();

        public Barrack CurrentBarrack => Building as Barrack;

        private void Awake()
        {
            Initialize(BuildingType.Barrack);
        }

        private void OnEnable()
        {
            if (CurrentBarrack == null)
            {
                Debug.LogError("BarrackPanel: CurrentBarrack null! Building not assigned");
                return;
            }
            PopulateSoldierList();
        }

        public override void SetBuilding(BuildingBase building)
        {
            base.SetBuilding(building);
            if (CurrentBarrack != null)
            {
                PopulateSoldierList();
            }
            else
            {
                Debug.LogError("BarrackPanel: The assigned building is not a Barrack!");
            }
        }

        private void PopulateSoldierList()
        {
            if (CurrentBarrack == null || CurrentBarrack.AvailableSoldiers == null)
            {
                Debug.LogError("BarrackPanel: CurrentBarrack or AvailableSoldiers null!");
                return;
            }

            foreach (Transform child in soldierListContainer)
            {
                Destroy(child.gameObject);
            }
            spawnedSoldierEntries.Clear();

            foreach (var soldier in CurrentBarrack.AvailableSoldiers)
            {
                GameObject entryObj = Instantiate(soldierEntryPrefab, soldierListContainer);
                SoldierEntryUI entryUI = entryObj.GetComponent<SoldierEntryUI>();
                if (entryUI != null)
                {
                    entryUI.Setup(soldier, () => StartProduction(soldier, entryUI));
                }
                spawnedSoldierEntries.Add(entryObj);
            }
        }

        private void StartProduction(Barrack.SoldierInfo soldier, SoldierEntryUI entryUI)
        {
            GameObject productionIcon = Instantiate(productionQueueIconPrefab, productionQueueContainer);

            Image productionQueueImage = productionIcon.GetComponent<Image>();
            if (productionQueueImage != null && entryUI.GetSoldierIcon() != null)
            {
                productionQueueImage.sprite = entryUI.GetSoldierIcon();
            }

            StartCoroutine(ProductionCoroutine(soldier, entryUI, productionIcon));
        }

        private IEnumerator ProductionCoroutine(Barrack.SoldierInfo soldier, SoldierEntryUI entryUI, GameObject productionIcon)
        {
            float productionTime = 5f;
            yield return new WaitForSeconds(productionTime);

            if (productionIcon != null)
            {
                Destroy(productionIcon);
            }

            soldier.Count++;
            entryUI.UpdateCount(soldier.Count);
        }
    }
}
