using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Landing : MonoBehaviour
{
    [SerializeField] private LoginPanel loginManager;
    [Space]
    [SerializeField] private Selection feudatorySelection;
    [SerializeField] private Selection avatarSelection;
    [SerializeField] private TMP_InputField userName;
    //[SerializeField] private Selection emblemSelection;


    private void Start()
    {
        loginManager.OnLoginDone.AddListener(OnLoginDone);
    }

    private void OnLoginDone()
    {
        PlayersManager.Instance.playerPublicData.feudetoryId = (feudatorySelection as FeudatorySelection).feudatories[feudatorySelection.Value].Id;
        PlayersManager.Instance.playerPublicData.avatarId = (avatarSelection as AvatarSelection).avatars[avatarSelection.Value].Id;
        //PlayersManager.Instance.playerPublicData.emblemId = emblemSelection.Value;

        LoginManager.Instance.SetUserData(new LoginManager.Data(userName.text));

        PlayerPublicData.SaveLocal(PlayersManager.Instance.playerPublicData);

        ScenesManager.Instance.LoadScene(ScenesManager.Scene.Game);
    }
}
