using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPrefabs : MonoBehaviour
{
    public PlayerStatus arc;
    public PlayerStatus fig;
    public PlayerStatus mag;
    public PlayerStatus lan;
    public Player _player;
    // Start is called before the first frame update
    void Awake()
    {
        if (!PlayerPrefs.HasKey("SetPrefab"))
        {
            _player = GameObject.Find("PlayerThings/Player").GetComponent<Player>();
            arc.currentHp = _player.PlayerData.HPA;
            arc.playerLevel = _player.PlayerData.lvlA;
            arc.maxHp = _player.PlayerData.HPA;
            arc.attack = _player.PlayerData.atkA;

            fig.currentHp = _player.PlayerData.HPF;
            fig.playerLevel = _player.PlayerData.lvlF;
            fig.maxHp = _player.PlayerData.HPF;
            fig.attack = _player.PlayerData.atkF;

            mag.currentHp = _player.PlayerData.HPM;
            mag.playerLevel = _player.PlayerData.lvlM;
            mag.maxHp = _player.PlayerData.HPM;
            mag.attack = _player.PlayerData.atkM;

            lan.currentHp = _player.PlayerData.HPL;
            lan.playerLevel = _player.PlayerData.lvlL;
            lan.maxHp = _player.PlayerData.HPL;
            lan.attack = _player.PlayerData.atkL;
            PlayerPrefs.SetInt("SetPrefab", 1);
        }
    }
}
