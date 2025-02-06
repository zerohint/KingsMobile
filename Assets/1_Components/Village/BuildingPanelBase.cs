using UnityEngine;

namespace Game.Village
{
    public abstract class BuildingPanelBase : MonoBehaviour
    {
        [SerializeField]
        private BuildingType buildingType; // Inspector üzerinden ayarlansýn

        public BuildingType BuildingType => buildingType;

        // Ýstersen runtime'da atamak için
        public virtual void Initialize(BuildingType type)
        {
            buildingType = type;
        }

        public virtual void SetActive(bool active)
        {
            gameObject.SetActive(active);
        }
    }
}
