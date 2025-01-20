using UnityEngine;


public class LoadingPanel : MonoSingletonSerializable
{
    [SerializeField] private GameObject loadingPanel;

    public static void SetLoading(bool stat)
    {
        //Instance.loadingPanel.SetActive(stat);
    }
}
