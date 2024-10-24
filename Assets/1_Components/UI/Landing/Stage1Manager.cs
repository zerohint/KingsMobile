using TMPro;

namespace UnityEngine.UI.Junk
{
    public class Stage1Manager : MonoBehaviour
    {
        [SerializeField] private Selection avatarSelect;
        [SerializeField] private TMP_InputField userName;
        //[SerializeField] private Selection symbolSelect;

        [SerializeField] private Button nextButton;

        private void Start()
        {
            userName.text = LoginManager.GetRandomUsername();
        }

        // TODO: lazyupdate
        private void Update()
        {
            nextButton.interactable = avatarSelect.IsSelected && LoginManager.IsUsernameValid(userName.text);
        }
    }
}
