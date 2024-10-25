using UnityEngine;

namespace Game.Village
{
    public abstract class BuildingBase : MonoBehaviour, IPressObject
    {
        public abstract void OnPress();
    }
}