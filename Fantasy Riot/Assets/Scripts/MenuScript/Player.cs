﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using System;

public class Player : MonoBehaviour
{
    [SerializeField]
    private PlayerData _playerData;

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
    public UnityEvent OnPlayerUpdate = new UnityEvent();

    [Header("Info Chamge")]
    public TMP_Text Users;
    public TMP_Text Gems;
    public TMP_Text other;

    public void SetThings(int val)
    {
        if (!val.Equals(Gemms))
        {
            _playerData.Gemms = val;
        }
    }
    public void UpdateThings(PlayerData playerData)
    {
        if (!playerData.Equals(_playerData))
        {
            _playerData = playerData;
            Users.text = playerData.User;
            Gems.text = playerData.Gemms.ToString();
        }
    }
    public void Switch(string stringa)
    {
        _playerData.User = stringa;
    }
}
