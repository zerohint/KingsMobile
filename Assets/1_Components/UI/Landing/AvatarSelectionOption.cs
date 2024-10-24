namespace UnityEngine.UI
{
    public class AvatarSelectionOption : SelectionOption
    {
        [SerializeField] private GameObject outline;

        public void Select() => outline.SetActive(true);
        public void Deselect() => outline.SetActive(false);
    }
}