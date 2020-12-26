﻿using System.Collections;
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
    public Player _player;

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
    }
    public void RegisterScreen() // Regester button
    {
        loginUI.SetActive(false);
        registerUI.SetActive(true);
    }
    public void UserScreen() // LogIn button
    {
        loginUI.SetActive(false);
        ALoginUI.SetActive(true);
    }
    public void MenuToAccountScreen() // Account button
    {
        if (_player.Email != "")
        {
            ALoginUI.SetActive(true);
        }
        else{
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
}