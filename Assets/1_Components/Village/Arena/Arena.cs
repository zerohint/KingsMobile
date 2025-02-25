using System.Collections.Generic;
using System;
using UnityEngine;

namespace Game.Village
{
    public class Arena : BuildingBase
    {
        public override BuildingType BuildingType => BuildingType.Arena;
        public override void OnPress()
        {
            ViewManager.Instance.ChangeView(ViewManager.View.Champion);
            //ViewManager.Instance.SetActive(ViewManager.View.Champion);
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
