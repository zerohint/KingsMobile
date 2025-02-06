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

        public Barrack CurrentBarrack => Building as Barrack;

        private Barrack currentBarrack;
        private List<GameObject> spawnedSoldierEntries = new List<GameObject>();

        private void Awake()
        {
            Initialize(BuildingType.Barrack);
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
