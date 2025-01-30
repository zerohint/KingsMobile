using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Village
{
    public class ClanBuilding : BuildingBase
    {
        public bool IsInClan { get; private set; }
        public string ClanName { get; private set; }
        public List<ClanData> AvailableClans { get; private set; } = new List<ClanData>();

        public override void OnPress()
        {
            ShowPanel();
        }

        public override Type GetPanelType()
        {
            return typeof(ClanBuildingPanel);
        }

        public void JoinClan(ClanData clan)
        {
            IsInClan = true;
            ClanName = clan.Name;
        }

        public void LeaveClan()
        {
            IsInClan = false;
            ClanName = "";
        }

        public void SetAvailableClans(List<ClanData> clans)
        {
            AvailableClans = clans;
        }
    }
}
