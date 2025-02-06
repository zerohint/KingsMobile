using UnityEngine;
using TMPro;
using System.Collections.Generic;

namespace Game.Village
{
    public class ClanBuildingPanel : BuildingPanelBase
    {
        [SerializeField] private TextMeshProUGUI clanStatusText;
        [SerializeField] private GameObject clanListContainer;
        [SerializeField] private GameObject clanEntryPrefab;

        public ClanBuilding CurrentBarrack => Building as ClanBuilding;

        private ClanBuilding currentClanBuilding;
        private void Awake()
        {
            Initialize(BuildingType.ClanBuilding);
        }
    }
}
