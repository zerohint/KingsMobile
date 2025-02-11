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
                string message = $"Level {nextStage.stageLevel} i�in y�kseltme:\n" +
                                 $"Gerekli bina seviyesi: {nextStage.requiredBuildingLevel}\n" +
                                 $"Z�mr�t: {nextStage.gemCost}\n" +
                                 $"Tah�l: {nextStage.grainCost}\n" +
                                 $"Sikke: {nextStage.coinCost}";

                PopupManager.Instance.ShowPopup(
                    message,
                    () =>
                    {
                        Debug.Log($"Bina level {nextStage.stageLevel}'ye y�kseltildi!");
                    },
                    () =>
                    {
                        Debug.Log("Upgrade iptal edildi.");
                    }
                );
            }
            else
            {
                Debug.Log("Daha fazla upgrade a�amas� bulunmuyor.");
            }
        }
    }
}
