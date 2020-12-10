using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogInStart : MonoBehaviour
{
    private Player player;
    // Start is called before the first frame update
    void Start()
    {
        player = new Player();
        player.LoadPlayer();
        if(!string.IsNullOrEmpty(player.name))
        StartCoroutine(LoginPlayer());
    }
    IEnumerator LoginPlayer()                       //this function Login the account in the DB
    {
        //WWWForm create an object "form" that can be used to interact whit DB
        WWWForm form = new WWWForm();
        form.AddField("name", player.name);                               //adding the name field
        form.AddField("password", player.password);                           //adding the password field
        WWW www = new WWW("http://localhost/sqlconnect/login.php", form);     //WWW create a connection with the page wher the php page is
        Debug.Log("insert name =" + player.name);
        Debug.Log("insert password =" + player.password);
        yield return www;                                               //return the www info page but continue the program
        Debug.Log("error =" + www.text[0]);
        if (www.text[0] == '0')                                        //if the contact with the page was succesfull
        {
            DBManager.username = player.name;                    //in the file DVManager insert the username and the score
            DBManager.score = int.Parse(www.text.Split('\t')[1]);
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }
        else //if it was not
        {
            Debug.Log("User login failed. Error #" + www.text); // show the error in the debug
        }
    }
}
