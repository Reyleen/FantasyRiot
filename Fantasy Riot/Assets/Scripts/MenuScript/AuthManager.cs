using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Auth;
using Firebase.Database;
using TMPro;
using System.Linq;
using UnityEngine.SceneManagement;

/* this is the authentication manager. It works with firebase Auth.*/
public class AuthManager : MonoBehaviour
{
    //Firebase variables
    [Header("Firebase")]
    public DependencyStatus dependencyStatus;
    public FirebaseAuth auth;
    public FirebaseUser User;

    //Login variable
    [Header("Login")]
    public TMP_InputField emailLoginField;
    public TMP_InputField passwordLoginField;
    public TMP_Text warningLoginText;
    public TMP_Text confirmLoginText;

    //Register variables
    [Header("Register")]
    public TMP_InputField usernameRegisterField;
    public TMP_InputField emailRegisterField;
    public TMP_InputField passwordRegisterField;
    public TMP_InputField passwordRegisterVerifyField;
    public TMP_Text warningRegisterText;

    [Header("Recover")]
    public TMP_InputField EmailRecoverField;
    public TMP_Text warningRecoverText;

    //AfterLogged
    [Header("UserData")]
    public TMP_Text Username;
    public Player _player;
    public SaveSystem a;
    public SyncPlayerToSave a1;
    public TMP_Text UserScore;
    public TMP_Text Score;
    public GameObject scoreElement;
    public Transform scoreboardContnent;

    //Prefab
    [Header("Other")]
    public PlayerStatus arc;
    public PlayerStatus fig;
    public PlayerStatus mag;
    public PlayerStatus lan;
    public GemsManager gemm;
    public GemsUI g;

    private void Awake()//start auth
    {
        //check dependencies for Firebase on the system
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            dependencyStatus = task.Result;
            if (dependencyStatus == DependencyStatus.Available)
            {
                InitializeFirebase();
            }
            else
            {
                Debug.LogError("Could not Resolve all Firebase dependencies: " + dependencyStatus);
            }
        });
    }
    private void Start()
    {
        GameObject s = GameObject.Find("PlayerThings/SaveManager");
        _player = GameObject.Find("PlayerThings/Player").GetComponent<Player>();
        a = s.GetComponent<SaveSystem>();
        a1 = s.GetComponent<SyncPlayerToSave>();
    }
    public void ScoreBoardButton()
    {
        StartCoroutine(LoadScoreboardData());
    }
    public void LogOut()//logout function
    {
        auth.SignOut();
        PlayerPrefs.DeleteAll();
        arc.currentHp = 35;
        arc.playerLevel = 0;
        arc.maxHp = 35;
        arc.attack = 5;
        fig.currentHp = 45;
        fig.playerLevel = 0;
        fig.maxHp = 45;
        fig.attack = 5;
        mag.currentHp = 33;
        mag.playerLevel = 0;
        mag.maxHp = 35;
        mag.attack = 5;
        lan.currentHp = 70;
        lan.playerLevel = 0;
        lan.maxHp = 70;
        lan.attack = 5;
        gemm.curGems = 0;
        g.UpdateStats();
        _player.SetFirstPlayer();
        _player.SetFirstScore();
        SceneManager.LoadScene("Intro");
    }
    private void InitializeFirebase()
    {
        //Set the Authentication instance object
        auth = FirebaseAuth.DefaultInstance;
    }
    public void LoginButton()
    {
        //Call the login courotine passing email and password
        StartCoroutine(Login(emailLoginField.text, passwordLoginField.text));
    }
    public void RegisterButton()
    {
        //Call the login courotine passing email and password
        StartCoroutine(Register(emailRegisterField.text, passwordRegisterField.text, usernameRegisterField.text));
    }

    private IEnumerator Login(string _email, string _password)
    {
        //Call the Firebase signin function (in auth)
        var LoginTask = auth.SignInWithEmailAndPasswordAsync(_email, _password);
        //wait till it is finished
        yield return new WaitUntil(predicate: () => LoginTask.IsCompleted);

        if (LoginTask.Exception != null)
        {
            //if an error occourred
            Debug.LogWarning(message: $"Failed to register task with {LoginTask.Exception}");
            FirebaseException firebaseEx = LoginTask.Exception.GetBaseException() as FirebaseException;
            AuthError errorCode = (AuthError)firebaseEx.ErrorCode;

            string message = "Login Failed";
            switch (errorCode)
            {
                case AuthError.MissingEmail:
                    message = "Missing Email";
                    break;
                case AuthError.MissingPassword:
                    message = "Missing Password";
                    break;
                case AuthError.WrongPassword:
                    message = "Wrong Password";
                    break;
                case AuthError.UserNotFound:
                    message = "Account does not exist";
                    break;
            }
            warningLoginText.text = message;
        }
        else
        {
            //user is logged in
            User = LoginTask.Result;
            Debug.LogFormat("User signed in successfully: {0} ({1})", User.DisplayName, User.Email);
            Username.text = User.DisplayName;
            warningLoginText.text = "";
            PlayerPrefs.SetString("ActualUser", User.UserId);
            a.DB();
            a1.SDB();
            PlayerPrefs.SetString("Email", _email);
            PlayerPrefs.SetString("Password", _password);
            PlayerPrefs.SetInt("Joined", 0);
            PlayerPrefs.SetString("User", User.DisplayName);
            Debug.Log(User.UserId);
            PanelManager.instance.UserScreen();
        }
    }
    private IEnumerator Register(string _email, string _password, string _username)
    {
        if (_username == "")
        {
            //if username is blank
            warningRegisterText.text = "Missing Username";
        }
        else if (passwordRegisterField.text != passwordRegisterVerifyField.text)
        {
            warningRegisterText.text = "Password Does Not Match!";
        }
        else
        {
            //Call the Firebase signin function (in auth)
            var RegisterTask = auth.CreateUserWithEmailAndPasswordAsync(_email, _password);
            //wait till it is finished
            yield return new WaitUntil(predicate: () => RegisterTask.IsCompleted);

            if (RegisterTask.Exception != null)
            {
                //if an error occourred
                Debug.LogWarning(message: $"Failed to register task with {RegisterTask.Exception}");
                FirebaseException firebaseEx = RegisterTask.Exception.GetBaseException() as FirebaseException;
                AuthError errorCode = (AuthError)firebaseEx.ErrorCode;
                string message = "Login Failed";
                switch (errorCode)
                {
                    case AuthError.MissingEmail:
                        message = "Missing Email";
                        break;
                    case AuthError.EmailAlreadyInUse:
                        message = "Email already in use";
                        break;
                    case AuthError.InvalidEmail:
                        message = "Invalid Email";
                        break;
                    case AuthError.MissingPassword:
                        message = "Missing Password";
                        break;
                    case AuthError.WrongPassword:
                        message = "Wrong Password";
                        break;
                    case AuthError.WeakPassword:
                        message = "The password is too weak";
                        break;
                }
                warningRegisterText.text = message;
            }
            else
            {
                //user has now been created
                //Get the result
                User = RegisterTask.Result;
                if (User != null)
                {
                    //Create a profile and set username
                    UserProfile profile = new UserProfile { DisplayName = _username };

                    //Call the Firebase auth update user profile funtion passing the profile with the username
                    var ProfileTask = User.UpdateUserProfileAsync(profile);
                    //Wait
                    yield return new WaitUntil(predicate: () => ProfileTask.IsCompleted);
                    if (RegisterTask.Exception != null)
                    {
                        //if there are errors handel them
                        Debug.LogWarning(message: $"failed to register task with {ProfileTask.Exception}");
                        FirebaseException firebaseEx = ProfileTask.Exception.GetBaseException() as FirebaseException;
                        AuthError errorCode = (AuthError)firebaseEx.ErrorCode;
                        warningRegisterText.text = "Username Set Failed!";
                    }
                    else
                    {
                        //Username is now set
                        warningRegisterText.text = "";
                        PlayerPrefs.SetString("ActualUser", User.UserId);
                        string Register = usernameRegisterField.text;
                        _player.Switch(Register);
                        a.DB();
                        a.SavePlayer(_player.PlayerData, true);
                        a.SaveScore(_player.PlayerScore, true);
                        PanelManager.instance.ToAccountScreenFromRegister();
                        PlayerPrefs.SetString("Email", _email);
                        PlayerPrefs.SetString("Password", _password);
                        PlayerPrefs.SetInt("Joined", 0);
                        PanelManager.instance.ToAccountScreenFromRegister();
                    }
                }
            }
        }
    }

    public void RecoverPassword()
    {
        StartCoroutine(Recover(EmailRecoverField.text));
    }
    private IEnumerator Recover(string _email)
    {
        //Call the Firebase recover function (in auth)
        var Task = auth.SendPasswordResetEmailAsync(_email);
        //wait till it is finished
        yield return new WaitUntil(predicate: () => Task.IsCompleted);
        if (Task.Exception != null)
        {
            //if an error occourred
            Debug.LogWarning(message: $"Failed to register task with {Task.Exception}");
            FirebaseException firebaseEx = Task.Exception.GetBaseException() as FirebaseException;
            AuthError errorCode = (AuthError)firebaseEx.ErrorCode;

            warningRecoverText.text = "Wrong Email";
        }
        else
        {
            //email sent
            warningRecoverText.text = "Email sent";
            PanelManager.instance.FromPassRecover();
        }
    }
    private IEnumerator LoadScoreboardData()
    {
        var DBTask = FirebaseDatabase.DefaultInstance.GetReference("score").OrderByChild("UserScore").LimitToLast(11).GetValueAsync();

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else
        {
            DataSnapshot snapshot = DBTask.Result;
            foreach (Transform Child in scoreboardContnent.transform)
            {
                Destroy(Child.gameObject);
            }
            int i = 1;
            bool top10 = false;
            foreach (DataSnapshot childSnapshot in snapshot.Children.Reverse<DataSnapshot>())
            {
                if (i <= 10)
                {
                    string username = childSnapshot.Child("Username").Value.ToString();
                    if (!_player.Username.Equals(""))
                    {
                        if (username == _player.Username)
                        {
                            top10 = true;
                        }
                    }
                    int score = int.Parse(childSnapshot.Child("UserScore").Value.ToString());
                    Debug.Log(int.Parse(childSnapshot.Child("UserScore").Value.ToString()));
                    GameObject scoreboardElement = Instantiate(scoreElement, scoreboardContnent);
                    scoreboardElement.GetComponent<ScoreElement>().NewScoreElement(i, username, score);
                    i++;
                }
                else if ((!top10) && (!_player.Username.Equals("")))
                {
                    GameObject scoreboardElement = Instantiate(scoreElement, scoreboardContnent);
                    scoreboardElement.GetComponent<ScoreElement>().NewScoreElement("---", _player.Username, _player.UserScore);
                }
            }
            PanelManager.instance.Scoreboard();
        }
    }
    public void UpdatePlayerOnServer(int currentGems, int playerLevel, int maxHp, int attack, string prefabName)
    {
        Debug.Log(currentGems);
        _player.changeGem(currentGems);
        switch (prefabName)
        {
            case "arcPlayer":
                _player.ChangeArc(attack, maxHp,playerLevel);
                break;
            case "FighterPlayer":
                _player.ChangeFig(attack, maxHp, playerLevel);
                break;
            case "LancPlayer":
                _player.ChangeLan(attack, maxHp, playerLevel);
                break;
            case "witchPlayer":
                _player.ChangeWit(attack, maxHp, playerLevel);
                break;
        }
        a.SavePlayer(_player.PlayerData, true);
    }
}
