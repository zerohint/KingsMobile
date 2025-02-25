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
        ViewManager.Instance.ChangeView(ViewManager.View.Village);
    }

    private void OnDestroy()
    {
        if (closeButton != null)
        {
            closeButton.onClick.RemoveListener(CloseCanvas);
        }
    }
}
