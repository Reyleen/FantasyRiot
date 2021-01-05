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

    public PlayerData PlayerData =>_playerData;
    public string User => _playerData.User;
    public int Gemms => _playerData.Gemms;
    public int lvlA => _playerData.lvlA;
    public int HPA => _playerData.HPA;
    public int atkA => _playerData.atkA;
    public int lvlF => _playerData.lvlF;
    public int HPF => _playerData.HPF;
    public int atkF => _playerData.atkF;
    public int lvlM => _playerData.lvlM;
    public int HPM => _playerData.HPM;
    public int atkM => _playerData.atkM;
    public int lvlL => _playerData.lvlL;
    public int HPL => _playerData.HPL;
    public int atkL => _playerData.atkL;
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
        Gems= GameObject.Find("MenuCanvas/AccountMenuSignedIn/GemsChange").GetComponent<TMP_Text>();
        Users.text = _playerData.User;
        Gems.text = _playerData.Gemms.ToString();
    }
}
