using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using Firebase.Extensions;

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

        private void Start()
        {
            LoadAndResumeProductionOrders();
        }

        private void LoadAndResumeProductionOrders()
        {
            FirebaseManager.Instance.LoadProductionOrders((orders) =>
            {
                foreach (var order in orders)
                {
                    ResumeProductionOrder(order);
                }
            });
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

        public void StartProduction(SoldierData soldier, SoldierEntryUI entryUI)
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
                string orderId = Guid.NewGuid().ToString();
                double startTimestamp = (DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds;
                ProductionOrder order = new ProductionOrder(orderId, soldier.soldierName, startTimestamp, soldier.productionTime);

                FirebaseManager.Instance.SaveProductionOrder(order);

                Coroutine productionRoutine = StartCoroutine(ProductionCoroutine(soldier, entryUI, productionIcon, radialProgress, order, soldier.productionTime));
                activeProductions[productionIcon] = productionRoutine;
            }
        }

        private IEnumerator ProductionCoroutine(SoldierData soldier, SoldierEntryUI entryUI, GameObject productionIcon, RadialProgress radialProgress, ProductionOrder order, float remainingTime)
        {
            float totalTime = remainingTime;
            float timeLeft = remainingTime;
            while (timeLeft > 0)
            {
                timeLeft -= Time.deltaTime;
                radialProgress.UpdateProgress(1 - (timeLeft / totalTime), timeLeft);
                yield return null;
            }

            FirebaseManager.Instance.RemoveProductionOrder(order.orderId);

            if (productionIcon != null)
            {
                Destroy(productionIcon);
                activeProductions.Remove(productionIcon);
            }

            soldier.initialCount++;
            entryUI.UpdateCount(soldier.initialCount);
        }

        public void ResumeProductionOrder(ProductionOrder order)
        {
            SoldierData soldier = null;
            SoldierEntryUI matchingEntry = null;
            foreach (var child in soldierListContainer.GetComponentsInChildren<SoldierEntryUI>())
            {
                if (child.soldierData.soldierName == order.soldierName)
                {
                    soldier = child.soldierData;
                    matchingEntry = child;
                    break;
                }
            }

            if (soldier == null)
            {
                Debug.LogWarning("No matching soldier found for resumed order: " + order.orderId);
                FirebaseManager.Instance.RemoveProductionOrder(order.orderId);
                return;
            }

            double currentTimestamp = (DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds;
            float elapsedTime = (float)(currentTimestamp - order.startTimestamp);
            float remainingTime = order.productionTime - elapsedTime;

            if (remainingTime <= 0)
            {
                Debug.Log("Resumed production order already completed: " + order.orderId);
                soldier.initialCount++;
                matchingEntry.UpdateCount(soldier.initialCount);
                FirebaseManager.Instance.RemoveProductionOrder(order.orderId);
                return;
            }

            GameObject productionIcon = Instantiate(productionQueueIconPrefab, productionQueueContainer);
            Image productionQueueImage = productionIcon.GetComponent<Image>();
            RadialProgress radialProgress = productionIcon.GetComponent<RadialProgress>();

            if (productionQueueImage != null)
            {
                productionQueueImage.sprite = matchingEntry.GetSoldierIcon();
            }

            Coroutine productionRoutine = StartCoroutine(ProductionCoroutine(soldier, matchingEntry, productionIcon, radialProgress, order, remainingTime));
            activeProductions[productionIcon] = productionRoutine;
        }
    }
}
