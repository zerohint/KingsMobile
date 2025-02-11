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
    [SerializeField] private Image armyDestinationLocationImage;
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
            panel.Value.SetActive(panel.Key == panelToShow);
    }

    #region Army Panel Codes
    public void UpdateArmyPanelFeudatories(FeudatoryDataSC startFeudatory, FeudatoryDataSC destinationFeudatory, float progress,ArmyData data)
    {
        UpdateProgress(progress);
        SoldierName.text=data.armyName;

        if (armyStartLocationImage != null)
            armyStartLocationImage.sprite = startFeudatory.Icon;
        if (armyDestinationLocationImage != null)
            armyDestinationLocationImage.sprite = destinationFeudatory.Icon;
    }

    public void UpdateProgress(float progress)
    {
        if (progressSlider != null)
            progressSlider.value = progress;

        if (progressText != null)
            progressText.text = $"{(progress * 100f):F1}%";
    }
    #endregion

    #region Castle Panel Codes
    public void UpdateCastleName(CastleData data)
    {
        CastleName.text=data.castleName;
    }
    #endregion
}
