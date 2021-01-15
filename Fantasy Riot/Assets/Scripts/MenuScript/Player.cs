using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using System;
using UnityEngine.SceneManagement;
/*player class thet store information in game and change them if needed*/
public class Player : MonoBehaviour
{
    [SerializeField]
    private PlayerData _playerData;
    [SerializeField]
    private PlayerScore _playerScore;

    public PlayerData PlayerData => _playerData;
    public string User => _playerData.User;
    public int Gemms => _playerData.Gemms;
    public int LvlA => _playerData.lvlA;
    public int HPA => _playerData.HPA;
    public int AtkA => _playerData.atkA;
    public int LvlF => _playerData.lvlF;
    public int HPF => _playerData.HPF;
    public int AtkF => _playerData.atkF;
    public int LvlM => _playerData.lvlM;
    public int HPM => _playerData.HPM;
    public int AtkM => _playerData.atkM;
    public int LvlL => _playerData.lvlL;
    public int HPL => _playerData.HPL;
    public int AtkL => _playerData.atkL;
    public bool L1 => _playerData.L1;
    public bool L2 => _playerData.L2;
    public bool L3 => _playerData.L3;
    public bool L4 => _playerData.L4;

    public PlayerScore PlayerScore => _playerScore;
    public string Username => _playerScore.Username;
    public int UserScore => _playerScore.UserScore;

    public UnityEvent OnPlayerUpdate = new UnityEvent();
    public UnityEvent OnPlayerUpdate2 = new UnityEvent();

    [Header("Info Chamge")]
    public TMP_Text Users;
    public TMP_Text Gems;
    public TMP_Text other;

    public void UpdateThings(PlayerData playerData)
    {
        Debug.Log("Updating Player");
        if (!playerData.Equals(_playerData))
        {
            Debug.Log("Update Player");
            _playerData = playerData;
        }
    }
    public void UpdateScore(PlayerScore playerScore)
    {
        Debug.Log("Updating Score");
        if (!playerScore.Equals(_playerScore))
        {
            Debug.Log("Update score");
            _playerScore = playerScore;
        }
    }
    public void Switch(string stringa)
    {
        _playerData.User = stringa;
        _playerScore.Username = stringa;
    }
    public void UpdateScreen()
    {
        Users = GameObject.Find("MenuCanvas/AccountMenuSignedIn/UserChange").GetComponent<TMP_Text>();
        Gems = GameObject.Find("MenuCanvas/AccountMenuSignedIn/GemsChange").GetComponent<TMP_Text>();
        Users.text = _playerData.User;
        Gems.text = _playerData.Gemms.ToString();
    }
    public void changeGem(int gems)
    {
        Debug.Log(gems);
        Debug.Log(_playerData.Gemms);
        _playerData.Gemms = gems;
    }
    public void AddGems(int gems)
    {
        Debug.Log(gems);
        Debug.Log(_playerData.Gemms);
        _playerData.Gemms += gems;
    }
    public void ChangeArc(int att, int HP, int lvl)
    {
        _playerData.atkA = att;
        _playerData.HPA = HP;
        _playerData.lvlA = lvl;
    }
    public void ChangeFig(int att, int HP, int lvl)
    {
        _playerData.atkF = att;
        _playerData.HPF = HP;
        _playerData.lvlF = lvl;
    }
    public void ChangeWit(int att, int HP, int lvl)
    {
        _playerData.atkM = att;
        _playerData.HPM = HP;
        _playerData.lvlM = lvl;
    }
    public void ChangeLan(int att, int HP, int lvl)
    {
        _playerData.atkL = att;
        _playerData.HPL = HP;
        _playerData.lvlL = lvl;
    }
    public void SetFirstPlayer()
    {
        _playerData.atkA = 5;
        _playerData.HPA = 35;
        _playerData.lvlA = 0;
        _playerData.atkF = 10;
        _playerData.HPF = 45;
        _playerData.lvlF = 0;
        _playerData.atkM = 7;
        _playerData.HPM = 33;
        _playerData.lvlM = 0;
        _playerData.atkL = 12;
        _playerData.HPL = 70;
        _playerData.lvlL = 0;
    }
    public void SetFirstScore()
    {
        _playerScore.Username = "";
        _playerScore.UserScore = 0;
    }
    public void SetScore(int score)
    {
        _playerScore.UserScore = score;
    }
}
