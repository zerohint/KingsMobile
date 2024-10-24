using TMPro;

namespace UnityEngine.UI
{
    public class FeudatoryInfoPanel : MonoBehaviour
    {
        [SerializeField] private Image iconImage;
        [SerializeField] private TMP_Text nameText;
        [SerializeField] private TMP_Text sloganText;
        [SerializeField] private TMP_Text descriptionText;
        // ...

        private FeudatoryDataSC data;

        public void SetData(FeudatoryDataSC data)
        {
            this.data = data;

            iconImage.sprite = data.Icon;
            nameText.text = data.Name;
            sloganText.text = data.Slogan;
            descriptionText.text = data.Description;
            // ...
        }
    }
}
