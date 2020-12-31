using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/*This script detect touch and login the player if already entered in old session*/
public class DetectTouchIntro : MonoBehaviour
{
    public bool clicked;
    private GameObject wrap;
    public GameObject load;
    public AuthManagerIntro au;
    public bool enter;
    public bool una=true;
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
                if (PlayerPrefs.HasKey("Email") && PlayerPrefs.HasKey("Password"))
                {
                    au.emailLoginField.text = PlayerPrefs.GetString("Email");
                    au.passwordLoginField.text = PlayerPrefs.GetString("Password");
                    au.LoginButton();
                }
                else
                {
                    PanelManager2.instance.LoginScreen();
                    clicked = false;
                }
            }
            else if (!clicked)
            {
                if (una)
                {
                    PanelManager2.instance.LoginScreen();
                    una = false;
                }
                if (load.activeSelf == false)
                { 
                PlayerPrefs.DeleteKey("Email");
                PlayerPrefs.DeleteKey("Password");
                }
            }
        }
    }
    public void changeLevel()//change the scene to the menu scene
    {
        Debug.Log("entrato");
        wrap.GetComponent<LoadLevel>().LoadScreen("Menu");
    }
           
}
