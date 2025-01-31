using System.Collections.Generic;
using System;
using UnityEngine;

namespace Game.Village
{
    public class Arena : BuildingBase
    {
        public override void OnPress()
        {
            ShowPanel();
        }
        public override System.Type GetPanelType()
        {
            return typeof(ArenaPanel);
        }

        public override string GetData()
        {
            return JsonUtility.ToJson(new Data()
            {    
            });
        }

        public override void SetData(string dataString)
        {
            Data data = JsonUtility.FromJson<Data>(dataString);
        }
        [Serializable]
        private struct Data
        {
        }
    }
}
