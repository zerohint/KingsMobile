using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class LoginPanel : MonoBehaviour
{
    public UnityEvent OnLoginDone;

    [SerializeField] private TMP_InputField emailInput;
    [SerializeField] private TMP_InputField passwordInput;

    /// <summary>
    /// Invoked by button click event
    /// </summary>
    public void OnLoginButtonClicked()
    {
        string userName = TheSingleton.GetManager<LoginManager>().LoginData.username;

        TheSingleton.GetManager<LoginManager>().SetUserData(new LoginManager.Data(userName, emailInput.text, passwordInput.text));
        OnLoginDone.Invoke();
    }

    public void OnGoogleLoginClicked()
    {
        throw new NotImplementedException();
    }

    public void OnSkipButtonClicked()
    {
        OnLoginDone.Invoke();
    }
}
