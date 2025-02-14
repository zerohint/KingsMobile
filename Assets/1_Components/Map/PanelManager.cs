using System.Collections.Generic;
using Game.Map;
using Map;
using UnityEngine;

public enum PanelType
{
    Castle,
    Army
}

public class PanelManager : MonoBehaviour
{
    public static PanelManager Instance { get; private set; }
    private Dictionary<PanelType, MapObjectPanelBase> panels = new Dictionary<PanelType, MapObjectPanelBase>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        
        MapObjectPanelBase[] panelComponents = GetComponentsInChildren<MapObjectPanelBase>(true);
        foreach (var panel in panelComponents)
        {
            if (panel is ArmyPanel)
                panels[PanelType.Army] = panel;
            else if (panel is CastlePanel)
                panels[PanelType.Castle] = panel;

            panel.HidePanel();
        }
    }

    public void ShowPanel(PanelType panelType)
    {
        foreach (var kvp in panels)
        {
            if (kvp.Key == panelType)
                kvp.Value.ShowPanel();
            else
                kvp.Value.HidePanel();
        }
    }

    public T GetPanel<T>(PanelType panelType) where T : MapObjectPanelBase
    {
        if (panels.TryGetValue(panelType, out MapObjectPanelBase panel))
        {
            return panel as T;
        }
        return null;
    }
}