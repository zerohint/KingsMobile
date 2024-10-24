using UnityEngine;
using System.Text.RegularExpressions;

[CreateAssetMenu(fileName = "LoginManager", menuName = "Game/Managers/Login Manager")]
public class LoginManager : SingletonSC<LoginManager>
{
    public Data LoginData { get; private set; }

    [SerializeField] private LoginPanel loginPanel;


    public override void Initialize()
    {
        base.Initialize();

        LoginData = Data.LoadLocal();
    }

    public void SetUserData(Data data)
    {
        LoginData = data;
        Data.SaveLocal(data);
    }


    public static bool IsUsernameValid(string username)
    {
        return !string.IsNullOrEmpty(username)
               && username.Length <= 16
               && Regex.IsMatch(username, @"^[a-zA-Z]+$");
    }
    public static string GetRandomUsername()
    {
        return "User" + Random.Range(1000, 9999);
    }


    public struct Data
    {
        public string username;
        public string email;
        public string password;

        private const string LoginDataKey = "LoginData";

        /// <summary>
        /// Did user login or using as guest
        /// </summary>
        /// <returns></returns>
        public readonly bool IsLogin() => !string.IsNullOrEmpty(email);

        public Data(string username) : this(username, string.Empty, string.Empty) { }

        public Data(string username, string email, string password)
        {
            this.username = username;
            this.email = email;
            this.password = password;
        }

        /// <summary>
        /// Save data to player prefs
        /// </summary>
        /// <param name="data"></param>
        public static void SaveLocal(Data data)
        {
            PlayerPrefs.SetString(LoginDataKey, JsonUtility.ToJson(data));
        }

        /// <summary>
        /// Load data from player prefs
        /// </summary>
        /// <returns></returns>
        public static Data LoadLocal()
        {
            return JsonUtility.FromJson<Data>(PlayerPrefs.GetString(LoginDataKey, "{}"));
        }
    }
}
