using UnityEngine;

namespace Map
{
    public abstract class MapObjectPanelBase : MonoBehaviour
    {
        public virtual void ShowPanel() => gameObject.SetActive(true);
        
        public virtual void HidePanel() => gameObject.SetActive(false);
        
        public virtual void InitializePanel() { }
    }
}