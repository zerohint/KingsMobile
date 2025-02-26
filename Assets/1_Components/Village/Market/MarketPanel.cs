using UnityEngine;
using UnityEngine.UI;

namespace Game.Village
{
    public class MarketPanel : BuildingPanelBase
    {
        public Market CurrentMarket => Building as Market;

        [SerializeField] private Button buyPanelButton;
        [SerializeField] private Button sellPanelButton;
        [SerializeField] private GameObject buyPanel;
        [SerializeField] private GameObject sellPanel;

        [SerializeField] private Button buyButton;
        [SerializeField] private Button sellButton;

        public Color defaultColor = Color.white;
        public Color activeColor;

        private void Awake()
        {
            Initialize(BuildingType.Market);

            buyPanelButton.onClick.AddListener(() => TogglePanel(buyPanel, sellPanel, buyPanelButton, sellPanelButton));
            sellPanelButton.onClick.AddListener(() => TogglePanel(sellPanel, buyPanel, sellPanelButton, buyPanelButton));

            TogglePanel(buyPanel, sellPanel, buyPanelButton, sellPanelButton);

            if (buyButton != null)
            {
                buyButton.onClick.AddListener(BuySelectedItems);
            }
            if (sellButton != null)
            {
                sellButton.onClick.AddListener(SellSelectedItems);
            }
        }

        public override void SetBuilding(BuildingBase building)
        {
            base.SetBuilding(building);
        }

        private void TogglePanel(GameObject openPanel, GameObject closePanel, Button activeButton, Button inactiveButton)
        {
            openPanel.SetActive(true);
            closePanel.SetActive(false);

            UpdateButtonColor(activeButton, true);
            UpdateButtonColor(inactiveButton, false);
        }

        private void UpdateButtonColor(Button button, bool isActive)
        {
            ColorBlock colors = button.colors;
            colors.normalColor = isActive ? activeColor : defaultColor;
            colors.selectedColor = isActive ? activeColor : defaultColor;
            colors.highlightedColor = isActive ? activeColor : defaultColor;
            button.colors = colors;
        }
        private void BuySelectedItems()
        {
            Inventory buyInventory = buyPanel.GetComponent<Inventory>();
            Inventory sellInventory = sellPanel.GetComponent<Inventory>();
            if (buyInventory != null && sellInventory != null)
            {
                buyInventory.TransferSelectedItemsTo(sellInventory);
            }
            else
            {
                Debug.LogWarning("Inventory component not found in Buy or Sell panel!");
            }
        }

        private void SellSelectedItems()
        {
            Inventory sellInventory = sellPanel.GetComponent<Inventory>();
            Inventory buyInventory = buyPanel.GetComponent<Inventory>();
            if (sellInventory != null && buyInventory != null)
            {
                sellInventory.TransferSelectedItemsTo(buyInventory);
            }
            else
            {
                Debug.LogWarning("Inventory component not found in Sell or Buy panel!");
            }
        }
    }
}
