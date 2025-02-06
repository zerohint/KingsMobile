using UnityEngine;

namespace Game.Village
{
    public abstract class BuildingPanelBase : MonoBehaviour
    {
        [SerializeField]
        private BuildingType buildingType; // Inspector �zerinden ayarlans�n

        public BuildingType BuildingType => buildingType;

        // �stersen runtime'da atamak i�in
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
