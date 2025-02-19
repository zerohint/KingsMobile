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
    }
}
