using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Auth;
using TMPro;
using Firebase.Database;

public class AuthManagerIntro : MonoBehaviour
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

    //AfterLogged
    [Header("Other")]
    public Player _player;
    public SaveSystem a;
    public SyncPlayerToSave a1;
    public DetectTouchIntro b;

    private void Awake()
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
    public void LogOut()
    {
        auth.SignOut();
        Debug.Log("User SignOut");
    }
    private void InitializeFirebase()
    {
        Debug.Log("Seting up Firebase Auth");
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
            PlayerPrefs.SetString("Email", _email);
            PlayerPrefs.SetString("Password", _password);
            warningLoginText.text = "";;
            a.DB();
            a1.SDB();
            b.changeLevel();
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
                        //Now return to login screeen
                        PanelManager2.instance.LoginScreen();
                        warningRegisterText.text = "";
                    }
                }
            }
        }
    }

}
