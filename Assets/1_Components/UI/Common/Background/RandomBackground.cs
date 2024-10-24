namespace UnityEngine.UI
{
    public sealed class RandomBackground : MonoBehaviour
    {
        [SerializeField] private Sprite[] backgrounds;

        private void Awake()
        {
            var image = GetComponent<Image>();
            image.sprite = backgrounds[Random.Range(0, backgrounds.Length)];
        }
    }
}