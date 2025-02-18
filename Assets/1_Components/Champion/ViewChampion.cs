using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewChampion : MonoBehaviour
{
    [SerializeField]
    public Button closeButton;
    [SerializeField]
    public Canvas ChampionViewCanvas;

    void Awake()
    {
        if (closeButton != null)
        {
            closeButton.onClick.AddListener(CloseCanvas);
        }
    }

    void CloseCanvas()
    {
        if (ChampionViewCanvas != null)
        {
            ChampionViewCanvas.enabled = false;
        }
    }

    private void OnDestroy()
    {
        if (closeButton != null)
        {
            closeButton.onClick.RemoveListener(CloseCanvas);
        }
    }
}
