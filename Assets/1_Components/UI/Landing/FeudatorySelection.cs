using ZeroGame;
using System.Linq;

namespace UnityEngine.UI
{
    public class FeudatorySelection : Selection
    {
        [SerializeField] private FeudatoryInfoPanel infoPanel;
        [SerializeField] private FeudatorySelectionMap map;
        [SerializeField] private Button nextButton;

        public FeudatoryDataSC[] feudatories;

        private void Start()
        {
            nextButton.enabled = false;
            feudatories = SCDB.GetAll<FeudatoryDataSC>().ToArray();
            CreateOptions(feudatories.Length);
        }


        protected override void OnOptionCreated(SelectionOption option)
        {
            option.SetData(feudatories[option.Value].Name);
        }

        protected override void OnValueChanged(int oldValue, int newValue)
        {
            infoPanel.SetData(feudatories[newValue]);
            map.SetData(feudatories[newValue]);
            if (oldValue != -1)
                options[oldValue].transform.localScale = Vector3.one;
            options[newValue].transform.localScale = Vector3.one * 1.1f;

            nextButton.enabled = newValue != -1;
        }

        
    }
}
