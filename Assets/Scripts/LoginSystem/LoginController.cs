using Firebase.Auth;
using Firebase.Database;
using Firebase.Extensions;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoginController : MonoBehaviour
{
    [Header("Interaction References")]
    [SerializeField]
    private Button loginButton = null;
    [SerializeField]
    private Button logoutButton = null;
    [SerializeField]
    private TMP_InputField nickInput = null;
    [SerializeField]
    private TextMeshProUGUI nickText = null;

    [Header("Panels")]
    [SerializeField]
    private GameObject AuthPanel = null;
    [SerializeField]
    private GameObject GamePanel = null;

    private string nick = string.Empty;
    private string Nick
    {
        get => nick;
        set
        {
            nick = value;
            nickText.text = value;
        }
    }

    private FirebaseAuth auth;
    private DatabaseReference referance;

    public static event Action<Exception> error;
    public static event Action loggedOut;

    private void Awake()
    {
        auth = FirebaseAuth.DefaultInstance;
        referance = FirebaseDatabase.DefaultInstance.RootReference;
    }

    private void Start()
    {
        loginButton.onClick.AddListener(Login);
        logoutButton.onClick.AddListener(Logout);
    }

    private void OnEnable()
    {
        auth.StateChanged += AuthStateChanged;
    }

    private void OnDisable()
    {
        auth.StateChanged -= AuthStateChanged;
    }

    private void AuthStateChanged(object sender, EventArgs e)
    {
        NextPanel(auth.CurrentUser != null);
    }

    private void Login()
    {
        if (nickInput.text != null && nickInput.text != "")
            QuestLogin();

        else
        {
            ArgumentException ae = new ArgumentException();
            error?.Invoke(ae);
            return;
        }
    }

    private void QuestLogin()
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            Exception e = new Exception("No internet connection!");
            error?.Invoke(e);
            return;
        }

        FirebaseAuth.DefaultInstance.SignInAnonymouslyAsync().ContinueWithOnMainThread(task =>
        {
            FirebaseUser newUser = task.Result;

            if (task.IsCompleted)
                CreateUserDatas(nickInput.text);

            else
                error?.Invoke(task.Exception);
        });
    }

    private void CreateUserDatas(string _nick)
    {
        UserData userData = new UserData
        {
            nick = _nick,
            id = auth.CurrentUser.UserId
        };

        string emptyJson = JsonUtility.ToJson(userData);
        referance.Child("Users").Child(auth.CurrentUser.UserId).SetRawJsonValueAsync(emptyJson);

        SetNick(userData);
    }

    private void NextPanel(bool loggedIn)
    {
        if (Nick == string.Empty && loggedIn)
            GetUserDatas();

        AuthPanel.SetActive(!loggedIn);
        GamePanel.SetActive(loggedIn);
    }

    private void GetUserDatas()
    {
        referance.Child("Users").Child(auth.CurrentUser.UserId).GetValueAsync().ContinueWith(task =>
        {
            DataSnapshot snapshot = task.Result;
            if (snapshot.GetRawJsonValue() != null)
            {
                UserData userData = JsonUtility.FromJson<UserData>(snapshot.GetRawJsonValue());

                SetNick(userData);
            }
        });
    }

    private void SetNick(UserData userData)
    {
        Nick = userData.nick;
    }

    private void Logout()
    {
        auth.SignOut();
        loggedOut?.Invoke();
    }
}

public class UserData
{
    public string id = string.Empty;
    public string nick = string.Empty;
    public string message = string.Empty;
}