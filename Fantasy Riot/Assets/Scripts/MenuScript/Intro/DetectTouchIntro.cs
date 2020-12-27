using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DetectTouchIntro : MonoBehaviour
{
    private bool clicked;
    private GameObject wrap;
    public AuthManagerIntro au;
    // Update is called once per frame
    private void Start()
    {
        wrap = GameObject.Find("LevelLoader");
        clicked = true;
    }
    void Update()
    {

        if (Time.time > 5f)
        {
            if ((Input.touchCount > 0 || Input.GetMouseButtonDown(0)) && clicked)
            {
                clicked = false;
                if (PlayerPrefs.HasKey("Email") && PlayerPrefs.HasKey("Password"))
                {
                    au.emailLoginField.text = PlayerPrefs.GetString("Email");
                    au.passwordLoginField.text = PlayerPrefs.GetString("Password");
                    au.LoginButton();
                }
                else
                {
                    PanelManager2.instance.LoginScreen();
                }
            }  
        }
    }
    public void changeLevel()
    {
        Debug.Log("entrato");
        wrap.GetComponent<LoadLevel>().LoadScreen("Menu");
    }
           
}
