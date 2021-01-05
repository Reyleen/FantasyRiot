using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreElement : MonoBehaviour
{
    public TMP_Text usernameText;
    public TMP_Text ScoreText;
    public void NewScoreElement(string _username, int _score)
    {
        usernameText.text = _username;
        ScoreText.text = _score.ToString();
    }
}
