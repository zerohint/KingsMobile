using Redellion;
using System.Linq;

namespace UnityEngine.UI
{
    public class AvatarSelection : Selection
    {
        public AvatarDataSC[] avatars;

        private void Start()
        {
            avatars = SCDB.GetAll<AvatarDataSC>().ToArray();
            CreateOptions(avatars.Length);
        }

        protected override void OnOptionCreated(SelectionOption option)
        {
            option.SetData(avatars[option.Value].Name, avatars[option.Value].Photo);
        }

        protected override void OnValueChanged(int oldValue, int newValue)
        {
            if (oldValue != -1 && options[oldValue] is AvatarSelectionOption avatarSelection)
                avatarSelection.Deselect();


            if (options[newValue] is AvatarSelectionOption avatarSelectionNew)
                avatarSelectionNew.Select();
            else
                Debug.LogError($"Option is not {nameof(AvatarSelectionOption)}");
        }
    }
}