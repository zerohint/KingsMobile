using UnityEngine;
using UnityEngine.UI;
using Game.Map;
using DG.Tweening;
using TMPro;

public class MapPanel : MonoBehaviour
{
    [SerializeField] private TMP_Text mapObjectNameText;
    [SerializeField] private GameObject panelGO;
    [SerializeField] private Canvas canvas;

    [SerializeField] private RectTransform tab;
    [SerializeField] private Button closeButton;
    public Vector2 openPosition;
    public Vector2 closedPosition;
    public Transform arrowButton;
    private bool isOpen = false;

    [Header("Animasyon Ayarlarý")]
    public float moveDuration = 0.5f;
    public float rotateDuration = 0.3f;
    private void Start()
    {
        isOpen = false;
        if (closeButton != null)
        {
            closeButton.onClick.AddListener(ToggleTab);
        }

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
    public void UpdatePanel(PanelData data)
    {
        mapObjectNameText.text = data.name;
    }
    public void SetActive(bool active)
    {
        canvas.enabled = active;
        panelGO.SetActive(active);
    }
}
public class PanelData
{
    public string name;
    public string owner;
    public int level;
}
