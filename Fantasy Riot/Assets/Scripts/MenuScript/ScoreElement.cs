using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreElement : MonoBehaviour
{
    public TMP_Text rankText;
    public TMP_Text usernameText;
    public TMP_Text ScoreText;
    public void NewScoreElement(int _rank, string _username, int _score)
    {
        rankText.text = _rank.ToString();
        usernameText.text = _username;
        ScoreText.text = _score.ToString();
    }
    public void NewScoreElement(string _rank, string _username, int _score)
    {
        rankText.text = _rank;
        usernameText.text = _username;
        ScoreText.text = _score.ToString();
    }
}
