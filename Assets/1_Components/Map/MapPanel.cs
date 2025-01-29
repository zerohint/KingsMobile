using UnityEngine;
using UnityEngine.UI;
using Game.Map;

public class MapPanel : MonoBehaviour
{
    [SerializeField] private Text mapObjectNameText;
    [SerializeField] private GameObject panelGO;
    [SerializeField] private Canvas canvas;


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
