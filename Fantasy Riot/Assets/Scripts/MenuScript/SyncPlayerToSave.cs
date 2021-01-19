using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/* Sync the player with the RealTime Database*/

public class SyncPlayerToSave : MonoBehaviour
{
    [SerializeField] private SaveSystem _playerSaveManager;
    [SerializeField] private Player _player;
    // Start is called before the first frame update
    private void Reset()
    {
        _playerSaveManager = FindObjectOfType<SaveSystem>();
    }
    public void SDB()//Sync data
    {
        _playerSaveManager.OnPlayerUpdated.AddListener(HandlePlayerSaveUpdated);
        _player.OnPlayerUpdate.AddListener(HandlePlayerUpdate);
        _playerSaveManager.OnPlayerUpdated2.AddListener(HandlePlayerSaveUpdated2);
        _player.OnPlayerUpdate2.AddListener(HandlePlayerUpdate2);
        _player.UpdateThings(_playerSaveManager.LastPlayerData);
        _player.UpdateScore(_playerSaveManager.LastPlayerScore);
    }

    private void HandlePlayerSaveUpdated(PlayerData playerData)
    {
        _player.UpdateThings(playerData);
    }
    private void HandlePlayerSaveUpdated2(PlayerScore playerData)
    {
        _player.UpdateScore(playerData);
    }
    private void HandlePlayerUpdate()
    {
        _playerSaveManager.SaveScore(_player.PlayerScore,false);
    }
    private void HandlePlayerUpdate2()
    {
        _playerSaveManager.SaveScore(_player.PlayerScore, false);
    }
}
