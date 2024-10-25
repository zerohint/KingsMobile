using System;
using UnityEngine;

public class ViewManager : MonoBehaviour
{
    public static ViewManager Instance;

    public Action<View> OnViewChanged;
    public View CurrentView { get; private set; }

    [SerializeField] private ViewData[] views;

    private void Start()
    {
        Instance = this;
        foreach (var viewData in views)
        {
            viewData.SetActive(false);
        }
        //ChangeView(View.Village);
    }


    public void ChangeView(View view)
    {
        foreach (var viewData in views)
            viewData.SetActive(viewData.view == view);
        CurrentView = view;
        OnViewChanged?.Invoke(view);
    }


    public enum View
    {
        Village,
        Map,
        Champion
    }

    [Serializable]
    private class ViewData
    {
        public View view;
        public Canvas canvas;
        public GameObject viewGO;

        [NonSerialized] public bool isActive = false;

        public void SetActive(bool active)
        {
            isActive = active;
            canvas.enabled = active;
            viewGO.SetActive(active);
        }
    }
}
