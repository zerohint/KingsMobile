using Game.Village;
using UnityEngine;

public abstract class MapObjectBase : MonoBehaviour, IPressObject
{
    public abstract PanelData GetPanelData();
    public abstract void OnPress();
    protected void ShowPanel()
    {
        MapManager.Instance.ShowBuildingPanel(this);
    }
}
