using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelManager2 : MonoBehaviour
{
    public static PanelManager2 instance;

    //Screen object variables
    public GameObject loginUI;
    public GameObject registerUI;
    public GameObject FirstPanel;
    public GameObject ResetPanel;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Debug.Log("Instance already exists, destroying object!");
            Destroy(this);
        }
    }

    //Functions to change the login screen UI
    public void LoginScreen() //Back button1
    {
        loginUI.SetActive(true);
        registerUI.SetActive(false);
        FirstPanel.SetActive(false);
    }
    public void RegisterScreen() // Regester button
    {
        loginUI.SetActive(false);
        registerUI.SetActive(true);
    }
    public void GuestScreen() // Account button
    {
        loginUI.SetActive(false);
        FirstPanel.SetActive(true);
    }
    public void CloseLogin()
    {
        loginUI.SetActive(false);
    }
    public void CloseRegister()
    {
        ResetPanel.SetActive(false);
    }
    public void toResetPass()
    {
        loginUI.SetActive(false);
        ResetPanel.SetActive(true);
    }
    public void FromResetPass()
    {
        loginUI.SetActive(true);
        ResetPanel.SetActive(false);
    }
}
