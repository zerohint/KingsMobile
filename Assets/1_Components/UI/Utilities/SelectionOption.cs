using System;
using UnityEngine.EventSystems;

namespace UnityEngine.UI
{
    public class SelectionOption : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] private TMPro.TMP_Text text;
        [SerializeField] private Image image;

        public int Value => value;

        private Action<SelectionOption> selectCallback;
        private int value;

        public virtual void Init(int value, Action<SelectionOption> selectCallback)
        {
            this.value = value;
            this.selectCallback = selectCallback;
        }

        public void SetData(string text, Sprite sprite = null)
        {
            if(this.text != null)
                this.text.text = text;
            
            if(image != null)
                image.sprite = sprite;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            selectCallback?.Invoke(this);
        }
    }
}
