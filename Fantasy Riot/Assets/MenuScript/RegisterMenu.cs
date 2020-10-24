using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RegisterMenu : MonoBehaviour
{
    public InputField insertname;                 //User name
    public InputField insertpassword;         //user password
    public InputField insertpassword2;      //for password check

    public Button submitButton;             //Submit button

    public void CallRegister()              //function that call the function Register
    {
        StartCoroutine(Register());              //StarCoroutine make sure that the user cant change page when the botton is clicked
    }
    IEnumerator Register()                       //this function Register the account in the DB
    {
        //WWWForm create an object "form" that can be used to interact whit DB
        WWWForm form = new WWWForm();
        form.AddField("name", insertname.text);                               //adding the name field
        form.AddField("password", insertpassword.text);                           //adding the password field
        WWW www = new WWW("http://localhost/sqlconnect/register.php",form);                      //WWW create a connection with the page wher the php page is
        yield return www;                                               //return the www info page but continue the program
        if (www.text == "0")                                        //if the contact with the page was succesfull
        {
            Debug.Log("User created.");
            //log the user
            DBManager.username = insertname.text;
            DBManager.score = 0;
            Debug.Log("User logged");
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }
        else //if it was not
        {
            Debug.Log("User cration failed. Error #" + www.text); // show the error in the debug
        }

    }
    public void VerifyInputs() //check if the information are accceptable
    {
        submitButton.interactable = ((insertname.text.Length >= 8 && insertpassword.text.Length >= 8 && insertpassword.text.Length >=8) 
                                    && insertpassword.text == insertpassword2.text);

    }

}
