using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Map
{
    public class Castle : MapObjectBase
    {
        private CastleData castleData;

        private void Awake()
        {
            castleData = FakeDatabase.GetRandomCastleData();
        }

        public override void OnPress()
        {
            ShowCastleInfoPanel(castleData);
            PanelManager.Instance.ShowPanel(PanelType.Castle);
            CastlePanel castlePanel = PanelManager.Instance.GetPanel<CastlePanel>(PanelType.Castle);
            if (castlePanel != null)
            {
                castlePanel.UpdateCastleName(castleData);
            }

        }

        private void ShowCastleInfoPanel(CastleData data)
        {
            Debug.Log($"Castle Name: {data.castleName}");
            Debug.Log($"Castle Owner: {data.castleOwner}");
            Debug.Log($"Castle Level: {data.castleLevel}");
            Debug.Log($"Resources Available: {data.resourcesAvailable}");
        }
        public override PanelData GetPanelData()
        {
            return new PanelData
            {
                name = castleData.castleName,
                owner = castleData.castleOwner,
                level = castleData.castleLevel
            };
        }

        public CastleData GetCastleData()
        {
            return castleData;
        }
    }

    [System.Serializable]
    public class CastleData
    {
        public string castleName;
        public string castleOwner;
        public int castleLevel;
        public int resourcesAvailable;
    }
}
