using UnityEngine;

// TODO: monosingleton
public class LoadingPanel : MonoBehaviour
{
    [SerializeField] private GameObject loadingPanel;

    public static void SetLoading(bool stat)
    {
        //Instance.loadingPanel.SetActive(stat);
    }
}
