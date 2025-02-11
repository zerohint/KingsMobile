using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

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

    // Army panelindeki UI öðeleri (örneðin Image alanlarý)
    [SerializeField] private Image armyStartLocationImage;
    [SerializeField] private Image armyDestinationLocationImage;
    [SerializeField] private Slider progressSlider;
    [SerializeField] private Text progressText;

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

    /// <summary>
    /// Belirtilen paneli açar, diðer panelleri kapatýr.
    /// </summary>
    public void ShowPanel(PanelType panelToShow)
    {
        foreach (var panel in panels)
            panel.Value.SetActive(panel.Key == panelToShow);
    }

    /// <summary>
    /// Army panelindeki baþlangýç ve varýþ Feudatory verilerinin Icon'larýný UI'ya atar.
    /// </summary>
    public void UpdateArmyPanelFeudatories(FeudatoryDataSC startFeudatory, FeudatoryDataSC destinationFeudatory, float progress)
    {
        UpdateProgress(progress);

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
            progressText.text = $"Tamamlanma: {(progress * 100f):F1}%";
    }
}
