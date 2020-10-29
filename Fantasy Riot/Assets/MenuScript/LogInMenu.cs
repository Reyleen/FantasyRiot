using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogInMenu : MonoBehaviour
{
    public InputField insertname;                 //User name
    public InputField insertpassword;         //user password

    public Button submitButton;             //Submit button

    public void CallLogin()
    {
        StartCoroutine(LoginPlayer());
    }
    IEnumerator LoginPlayer()                       //this function Login the account in the DB
    {
        //WWWForm create an object "form" that can be used to interact whit DB
        WWWForm form = new WWWForm();
        form.AddField("name", insertname.text);                               //adding the name field
        form.AddField("password", insertpassword.text);                           //adding the password field
        WWW www = new WWW("http://localhost/sqlconnect/login.php", form);     //WWW create a connection with the page wher the php page is
        Debug.Log("insert name =" + insertname.text);
        Debug.Log("insert password =" + insertpassword.text);
        yield return www;                                               //return the www info page but continue the program
        Debug.Log("error =" + www.text[0]);
        if (www.text[0] == '0')                                        //if the contact with the page was succesfull
        {
            DBManager.username = insertname.text;                    //in the file DVManager insert the username and the score
            DBManager.score = int.Parse(www.text.Split('\t')[1]);
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }
        else //if it was not
        {
            Debug.Log("User login failed. Error #" + www.text); // show the error in the debug
        }
    }
    public void VerifyInputs() //check if the information are accceptable
    {
        submitButton.interactable = (insertname.text.Length >= 8 && insertpassword.text.Length >= 8);
    }
}
