using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField]
    private PlayerData _playerData;

    public PlayerData PlayerData =>_playerData;

    public string User => _playerData.User;
    public string Scritta => _playerData.Scritta;
    public UnityEvent OnPlayerUpdate = new UnityEvent();

    public void SetThings(string val)
    {
        if (!val.Equals(Scritta))
        {
            _playerData.Scritta = val;
        }
    }
    public void UpdateThings(PlayerData playerData)
    {
        if (!playerData.Equals(_playerData))
            {
            _playerData = playerData;
        }
    }
}
