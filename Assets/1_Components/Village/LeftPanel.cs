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
        [SerializeField] private TMP_Text upgradeLevelText;
        [SerializeField] private TMP_Text gemText;
        [SerializeField] private TMP_Text grainText;
        [SerializeField] private TMP_Text coinText;

        [SerializeField] private Image gemIcon;
        [SerializeField] private Image grainIcon;
        [SerializeField] private Image coinIcon;

        [SerializeField] private GameObject panelGO;
        [SerializeField] private Canvas canvas;

        [Header("Animation Settings")]
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

            var upgradeStage = building.GetNextUpgradeStage();
            if (upgradeStage != null)
            {
                upgradeLevelText.text = $"Level {upgradeStage.stageLevel}";
                gemText.text = upgradeStage.gemCost.ToString();
                grainText.text = upgradeStage.grainCost.ToString();
                coinText.text = upgradeStage.coinCost.ToString();

                upgradeButton.interactable = HasSufficientResources(upgradeStage);
            }
            else
            {
                upgradeLevelText.text = "Max Level";
                upgradeButton.interactable = false;
            }
        }

        private bool HasSufficientResources(UpgradeStage upgradeStage)
        {
            var playerData = PlayersManager.Instance.playerData;
            return playerData.coin >= upgradeStage.coinCost &&
                   playerData.gem >= upgradeStage.gemCost &&
                   playerData.grain >= upgradeStage.grainCost;
        }

        public void UpgradeBuilding()
        {
            if (currentBuilding != null)
            {
                PopupManager.Instance.ShowPopup(
                    "Upgrade this building?",
                    () =>
                    {
                        var upgradeStage = currentBuilding.GetNextUpgradeStage();
                        if (upgradeStage != null && HasSufficientResources(upgradeStage))
                        {
                            var playerData = PlayersManager.Instance.playerData;
                            playerData.coin -= upgradeStage.coinCost;
                            playerData.gem -= upgradeStage.gemCost;
                            playerData.grain -= upgradeStage.grainCost;

                            FirebaseManager.Instance.SavePlayerData(playerData, () =>
                            {
                                Debug.Log("Resources deducted and saved to Firebase.");
                                PlayerUIManager uiManager = FindObjectOfType<PlayerUIManager>();
                                if (uiManager != null)
                                    uiManager.UpdateUI();
                            });

                            currentBuilding.Upgrade();
                            Debug.Log("Building upgraded!");
                        }
                        else
                        {
                            Debug.LogWarning("Not enough resources to upgrade the building.");
                        }
                        UpdatePanel(currentBuilding);
                    },
                    () =>
                    {
                        Debug.Log("Upgrade canceled.");
                    }
                );
            }
            else
            {
                Debug.LogWarning("No active building selected!");
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
