using UnityEngine;
using TMPro;

namespace Game.Village
{
    public class CouncilPanel : BuildingPanelBase
    {
        public Council CurrentBarrack => Building as Council;

        private Council currentCouncil;
        private void Awake()
        {
            Initialize(BuildingType.Council);
        }
    }
}
