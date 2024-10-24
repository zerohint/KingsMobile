using System;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Junk
{
    [RequireComponent(typeof(Button))]
    public class ViewButton : MonoBehaviour
    {
        [SerializeField] private ViewManager.View view;

        private void Start()
        {
            GetComponent<Button>().onClick.AddListener(OnClick);
        }

        private void OnClick()
        {
            ViewManager.Instance.ChangeView(view);
        }
    }
}