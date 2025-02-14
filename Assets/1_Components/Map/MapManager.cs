using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;
using Game.Map;
using Map;

public enum PanelType
{
    Castle,
    Army
}

public class MapManager : MonoBehaviour
{
    public static MapManager Instance { get; private set; }

    [SerializeField] private GameObject castlePanel;
    [SerializeField] private GameObject armyPanel;

    // Armypanel UI
    [SerializeField] private TMP_Text SoldierName;
    [SerializeField] private Image armyStartLocationImage;
    [SerializeField] private TMP_Text armyStartLocationText;
    [SerializeField] private Image armyDestinationLocationImage;
    [SerializeField] private TMP_Text armyDestinationLocationText;
    [SerializeField] private Slider progressSlider;
    [SerializeField] private TMP_Text progressText;

    // CastlePanel UI
    [SerializeField] private TMP_Text CastleName;


    private Dictionary<PanelType, GameObject> panels;

    private void Awake()
    {
        Instance = this;
        panels = new Dictionary<PanelType, GameObject>
        {
            { PanelType.Castle, castlePanel },
            { PanelType.Army, armyPanel }
        };
    }

    public void ShowPanel(PanelType panelToShow)
    {
        foreach (var panel in panels)
        {
            panel.Value.SetActive(panel.Key == panelToShow);
            if (panel.Key==panelToShow)
            {
                MapPanel.Instance.ToggleTab();
            }
        }
 
    }

    #region Army Panel Codes
    public void UpdateArmyPanelFeudatories(FeudatoryDataSC startFeudatory, FeudatoryDataSC destinationFeudatory, float progress, float journeyDuration, ArmyData data)
    {
        UpdateProgress(progress, journeyDuration);
        SoldierName.text = data.armyName;

        if (armyStartLocationImage != null)
        {
            armyStartLocationImage.sprite = startFeudatory.Icon;
            armyStartLocationText.text = startFeudatory.Name;
        }
        if (armyDestinationLocationImage != null)
        {
            armyDestinationLocationImage.sprite = destinationFeudatory.Icon;
            armyDestinationLocationText.text = destinationFeudatory.Name;
        }
    }

    public void UpdateProgress(float progress, float journeyDuration)
    {
        if (progressSlider != null)
            progressSlider.value = progress;
        
        float remainingTime = journeyDuration * (1f - progress);
        TimeSpan timeSpan = TimeSpan.FromSeconds(remainingTime);

        if (progressText != null)
            progressText.text = string.Format("{0:D2}:{1:D2}:{2:D2}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
    }
    #endregion

    #region Castle Panel Codes
    public void UpdateCastleName(CastleData data)
    {
        CastleName.text=data.castleName;
    }
    #endregion
}
