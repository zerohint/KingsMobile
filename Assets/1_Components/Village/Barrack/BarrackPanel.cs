using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


namespace Game.Village
{
    public class BarrackPanel : BuildingPanelBase
    {
        [SerializeField] private GameObject soldierEntryPrefab;
        [SerializeField] private GameObject RightPanelObject;
        [SerializeField] private Transform soldierListContainer;

        [SerializeField] private BuildingUpgradeData upgradeData;
        public Barrack CurrentBarrack => Building as Barrack;

        private Barrack currentBarrack;
        private List<GameObject> spawnedSoldierEntries = new List<GameObject>();

        private void Awake()
        {
            Initialize(BuildingType.Barrack);
        }
        public void UpgradeBuilding()
        {
            int currentStageLevel = CurrentBarrack.CurrentUpgradeStage;

            UpgradeStage nextStage = upgradeData.upgradeStages.Find(stage => stage.stageLevel == currentStageLevel + 1);

            if (nextStage != null)
            {
                string message = $"Level {nextStage.stageLevel} Upgrade:\n" +
                                 $"Required building level: {nextStage.requiredBuildingLevel}\n" +
                                 $"Emerald: {nextStage.gemCost}\n" +
                                 $"Grain: {nextStage.grainCost}\n" +
                                 $"Coin: {nextStage.coinCost}";

                PopupManager.Instance.ShowPopup(
                    message,
                    () =>
                    {
                        Debug.Log($"Building upgraded to level {nextStage.stageLevel}");
                    },
                    () =>
                    {
                        Debug.Log("Upgrade canceled.");
                    }
                );
            }
            else
            {
                Debug.Log("There are no more upgrade stages.");
            }
        }
    }
}
