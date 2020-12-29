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
        if ((PlayerPrefs.HasKey("Joined")) && (SceneManager.GetActiveScene().name != "Intro"))
        {
            if (PlayerPrefs.GetInt("Joined") == 1)
            {
            _playerSaveManager.ChangePLAYER_KEY(PlayerPrefs.GetString("Email").Replace(".", ","));
            _playerSaveManager.DB();
            SDB();
            PlayerPrefs.SetInt("Joined", 0);
            }
        }
    }
    public void SDB()//Sync data
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
        _playerSaveManager.SavePlayer(_player.PlayerData,false);
    }
}
