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
        public static LeftPanel Instance;

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

        [Header("Animasyon Settings")]
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

            if (instantiatedPanel == null)
            {
                BuildingPanelBase prefab = null;

                foreach (var panel in panelPrefabs)
                {
                    if (panel.BuildingType == btype)
                    {
                        prefab = panel;
                        break;
                    }
                }

                if (prefab == null)
                {
                    return;
                }

                instantiatedPanel = Instantiate(prefab, panelContainer);
                panelsInstantiated.Add(instantiatedPanel);
            }

            if (currentPanel != null)
                currentPanel.SetActive(false);

            instantiatedPanel.SetActive(true);
            ToggleTab();
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
            BuildingBase building = GetCurrentBuilding();
            if (building != null)
            {
                PopupManager.Instance.ShowPopup(
                    "Are you sure you want to upgrade this building?",
                    () => {
                        building.Upgrade();
                        Debug.Log("The building has been upgraded!");
                    },
                    () => {
                        Debug.Log("Upgrade canceled.");
                    }
                );
            }
            else
            {
                Debug.LogWarning("No active buildings found!");
            }
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
