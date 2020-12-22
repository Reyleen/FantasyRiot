using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyncPlayerToSave : MonoBehaviour
{
    [SerializeField] private SaveSystem _playerSaveManager;
    [SerializeField] private Player _player;
    // Start is called before the first frame update
    private void Reset()
    {
        _playerSaveManager = FindObjectOfType<SaveSystem>();
    }
    public void SDB()
    {
        _playerSaveManager.OnPlayerUpdated.AddListener(HandlePlayerSaveUpdated);
        _player.OnPlayerUpdate.AddListener(HandlePlayerUpdate);
        _player.UpdateThings(_playerSaveManager.LastPlayerData);
    }

    private void HandlePlayerSaveUpdated(PlayerData playerData)
    {
        _player.UpdateThings(playerData);
    }

    private void HandlePlayerUpdate()
    {
        _playerSaveManager.SavePlayer(_player.PlayerData);
    }
}
