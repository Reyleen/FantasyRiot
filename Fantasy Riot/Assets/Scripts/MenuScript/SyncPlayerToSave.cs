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
    private void Awake()//control if it's the first time the player joined the game
    {
        
    }
    public void SDB()//Sync data
    {
        _playerSaveManager.OnPlayerUpdated.AddListener(HandlePlayerSaveUpdated);
        _player.OnPlayerUpdate.AddListener(HandlePlayerUpdate);
        _playerSaveManager.OnPlayerUpdated2.AddListener(HandlePlayerSaveUpdated2);
        _player.OnPlayerUpdate2.AddListener(HandlePlayerUpdate2);
        Debug.Log("Entering Update()");
        _player.UpdateThings(_playerSaveManager.LastPlayerData);
        Debug.Log("Entering Update2()");
        _player.UpdateScore(_playerSaveManager.LastPlayerScore);
        Debug.Log("Exiting Update2()");
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
