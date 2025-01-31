using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
namespace Game.Village
{
    public class BuildingPanel : MonoBehaviour
    {
        [SerializeField] private RectTransform tab;
        public Vector2 openPosition;
        public Vector2 closedPosition;
        public Transform arrowButton;
        [SerializeField] private Button closeButton;

        [SerializeField] private Button upgradeButton;
        [SerializeField] private TMP_Text buildingNameText;
        [SerializeField] private GameObject panelGO;
        [SerializeField] private Canvas canvas;

        [Header("Animasyon Ayarlarý")]
        public float moveDuration = 0.5f;
        public float rotateDuration = 0.3f;

        private bool isOpen = false;


        private BuildingBase currentBuilding;
        public void UpdatePanel(BuildingBase building)
        {
            currentBuilding = building;
            buildingNameText.text = building.name;

        }

        public void UpgradeBuilding()
        {
            PopupManager.Instance.ShowPopup(
                "Bu binayý yükseltmek istediðine emin misin?",
                () => {
                    Debug.Log("Bina upgrade edildi!");
                    // Buraya upgrade iþlemi eklenecek
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

            tab.DOAnchorPos(isOpen ? openPosition : closedPosition, moveDuration)
               .SetEase(Ease.InOutQuad);

            // Ok butonu döndürme
            arrowButton.DORotate(new Vector3(0, 0, isOpen ? 180 : 0), 0, RotateMode.FastBeyond360)
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
