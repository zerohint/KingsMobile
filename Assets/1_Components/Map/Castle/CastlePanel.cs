using Map;
using UnityEngine;
using TMPro;

namespace Game.Map
{ 
    public class CastlePanel : MapObjectPanelBase
    {
        [SerializeField] private TMP_Text castleName;

        public override void InitializePanel()
        {
        }

        public void UpdateCastleName(CastleData data)
        {
            castleName.text = data.castleName;
        }
    }
}