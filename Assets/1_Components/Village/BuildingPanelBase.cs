using UnityEngine;

namespace Game.Village
{
    public abstract class BuildingPanelBase : MonoBehaviour
    {
        public abstract void UpdatePanel(BuildingBase building);
        public virtual void SetActive(bool active)
        {
            gameObject.SetActive(active);
        }
    }
}
