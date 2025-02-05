using System;
using UnityEngine;

namespace Game.Village
{
    public class ClanBuilding : BuildingBase
    {
        public Clan ClanData { get; private set; }

        private void Start()
        {
            // Varsayýlan deðerler (isteðe baðlý olarak deðiþtirilebilir)
            ClanData = new Clan(1, 10);
        }

        public override string GetData()
        {
            return JsonUtility.ToJson(ClanData);
        }

        public override void SetData(string dataString)
        {
            ClanData = JsonUtility.FromJson<Clan>(dataString);
        }

        public override void OnPress()
        {
            ShowPanel();
        }

        public override Type GetPanelType => typeof(ClanBuildingPanel);
    }

    [Serializable]
    public class Clan
    {
        public int ClanLevel;
        public int PlayerLimit;

        public Clan(int clanLevel, int playerLimit)
        {
            ClanLevel = clanLevel;
            PlayerLimit = playerLimit;
        }
    }
}
