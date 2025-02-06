using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using System.Collections.Generic;
using System;


namespace Game.Village
{
    public class LeftPanel : MonoBehaviour
    {
        public static LeftPanel Instance; // todo yap

        [SerializeField] private Transform panelContainer;

        [SerializeField] private RectTransform tab;
        public Vector2 openPosition;
        public Vector2 closedPosition;
        public Transform arrowButton;
        [SerializeField] private Button closeButton;

        [SerializeField] private Button upgradeButton;
        [SerializeField] private TMP_Text buildingNameText;
        [SerializeField] private GameObject panelGO;
        [SerializeField] private Canvas canvas;

        [Header("Animasyon Ayarlar�")]
        public float moveDuration = 0.5f;
        public float rotateDuration = 0.3f;

        private bool isOpen = false;

        [SerializeField] private BuildingPanelBase[] panelPrefabs;
        private List<BuildingPanelBase> panelsInstantiated = new();
        private BuildingPanelBase currentPanel = null;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void OpenPanel(BuildingType btype)
        {
            var instantiatedPanel = panelsInstantiated.Find(panel => panel.BuildingType == btype);
            if(instantiatedPanel == null)
            {
                var prefab = Array.Find(panelPrefabs, panel => panel.BuildingType == btype);
                instantiatedPanel = Instantiate(prefab, panelContainer);
            }

            if(currentPanel != null)
                currentPanel.SetActive(false);
            instantiatedPanel.SetActive(true);
            currentPanel = instantiatedPanel;
        }




        private BuildingBase currentBuilding;
        public void UpdatePanel(BuildingBase building)
        {
            currentBuilding = building;
            buildingNameText.text = building.name;

        }

        public void UpgradeBuilding()
        {
            PopupManager.Instance.ShowPopup(
                "Bu binay� y�kseltmek istedi�ine emin misin?",
                () => {
                    Debug.Log("Bina upgrade edildi!");
                    // Buraya upgrade i�lemi eklenecek
                },
                () => {
                    Debug.Log("Upgrade iptal edildi.");
                }
            );
        }
        private void Start()
        {
            isOpen = true;
            if (closeButton != null)
            {
                closeButton.onClick.AddListener(ToggleTab);
            }
            upgradeButton.onClick.AddListener(UpgradeBuilding);
        }

        public void ToggleTab()
        {
            isOpen = !isOpen;

            tab.DOMoveX(isOpen ? openPosition.x : closedPosition.x, moveDuration)
               .SetEase(Ease.InOutQuad);

            // Ok butonu d�nd�rme
            arrowButton.DORotate(new Vector3(0, 0, !isOpen ? 180 : 0), 0, RotateMode.FastBeyond360)
                       .SetEase(Ease.InOutQuad);
        }

        private void OnDestroy()
        {
            if (closeButton != null)
            {
                closeButton.onClick.RemoveListener(ToggleTab);
            }
        }

        public BuildingBase GetCurrentBuilding()
        {
            return currentBuilding;
        }
    }
}
