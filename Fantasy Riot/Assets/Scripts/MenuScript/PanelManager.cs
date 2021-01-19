using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelManager : MonoBehaviour
{
    public static PanelManager instance;

    //Screen object variables
    public GameObject loginUI;
    public GameObject registerUI;
    public GameObject ALoginUI;
    public GameObject FirstPanel;
    public GameObject Recover;
    public GameObject Rank;
    public Player _player;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Destroy(this);
        }
    }
    private void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
    }

    //Functions to change the login screen UI
    public void LoginScreen() //Back button1
    {
        loginUI.SetActive(true);
        registerUI.SetActive(false);
    }
    public void RegisterScreen() // Regester button
    {
        loginUI.SetActive(false);
        registerUI.SetActive(true);
    }
    public void ToAccountScreenFromRegister() // Regester button
    {
        registerUI.SetActive(false);
        ALoginUI.SetActive(true);
        _player.UpdateScreen();
    }
    public void LoginScreenToMenu()
    {
        loginUI.SetActive(false);
        FirstPanel.SetActive(true);
    }
    public void UserScreen() // LogIn button
    {
        loginUI.SetActive(false);
        ALoginUI.SetActive(true);
        _player.UpdateScreen();
    }
    public void MenuToAccountScreen() // Account button
    {
        if (PlayerPrefs.HasKey("Joined"))
        {
            ALoginUI.SetActive(true);
            _player.UpdateScreen();
        }
        else
        {
            loginUI.SetActive(true);
        }
        FirstPanel.SetActive(false);
    }
    public void AccountScreenToMenu()
    {
        ALoginUI.SetActive(false);
        loginUI.SetActive(false);
        FirstPanel.SetActive(true);
    }
    public void ToPassRecover()
    {
        loginUI.SetActive(false);
        Recover.SetActive(true);
    }
    public void FromPassRecover()
    {
        loginUI.SetActive(true);
        Recover.SetActive(false);
    }
    public void Scoreboard()
    {
        FirstPanel.SetActive(false);
        Rank.SetActive(true);
    }
    public void BackFromScoreboard()
    {
        FirstPanel.SetActive(true);
        Rank.SetActive(false);
    }
}
