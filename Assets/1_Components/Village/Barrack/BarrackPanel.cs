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
        private Dictionary<GameObject, Coroutine> activeProductions = new Dictionary<GameObject, Coroutine>();

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
            foreach (Transform child in soldierListContainer)
            {
                Destroy(child.gameObject);
            }
            spawnedSoldierEntries.Clear();

            foreach (var soldierData in CurrentBarrack.AvailableSoldiers)
            {
                GameObject entryObj = Instantiate(soldierEntryPrefab, soldierListContainer);
                SoldierEntryUI entryUI = entryObj.GetComponent<SoldierEntryUI>();
                if (entryUI != null)
                {
                    entryUI.Setup(soldierData, StartProduction);
                }
                spawnedSoldierEntries.Add(entryObj);
            }
        }

        private void StartProduction(SoldierData soldier, SoldierEntryUI entryUI)
        {
            GameObject productionIcon = Instantiate(productionQueueIconPrefab, productionQueueContainer);
            Image productionQueueImage = productionIcon.GetComponent<Image>();
            RadialProgress radialProgress = productionIcon.GetComponent<RadialProgress>();

            if (productionQueueImage != null)
            {
                productionQueueImage.sprite = entryUI.GetSoldierIcon();
            }

            if (radialProgress != null)
            {
                Coroutine productionRoutine = StartCoroutine(ProductionCoroutine(soldier, entryUI, productionIcon, radialProgress));
                activeProductions[productionIcon] = productionRoutine;
            }
        }

        private IEnumerator ProductionCoroutine(SoldierData soldier, SoldierEntryUI entryUI, GameObject productionIcon, RadialProgress radialProgress)
        {
            float timeLeft = soldier.productionTime;
            while (timeLeft > 0)
            {
                timeLeft -= Time.deltaTime;
                radialProgress.UpdateProgress(1 - (timeLeft / soldier.productionTime), timeLeft);
                yield return null;
            }

            if (productionIcon != null)
            {
                Destroy(productionIcon);
                activeProductions.Remove(productionIcon);
            }

            soldier.initialCount++;
            entryUI.UpdateCount(soldier.initialCount);
        }

    }
}
