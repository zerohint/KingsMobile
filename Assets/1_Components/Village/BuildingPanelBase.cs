using UnityEngine;

namespace Game.Village
{
    public abstract class BuildingPanelBase : MonoBehaviour
    {
        public BuildingType BuildingType { get; }
        public virtual void SetActive(bool active)
        {
            gameObject.SetActive(active);
        }
    }
}
