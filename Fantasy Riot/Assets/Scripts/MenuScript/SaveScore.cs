﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
//this script save manage the info to show in the death menu and talk with the save manager for data save
public class SaveScore : MonoBehaviour
{
    public Player _p;
    public SaveSystem _Save;
    public Infinitewaves infinity;
    public WaveSpawner normal;
    public GemsManager g;
    public TMP_Text testoGems;
    public TMP_Text changeWave;
    public TMP_Text chaneGems;

    public void SavingInfinite()
    {
        Debug.Log("SavingScore");
        SetPlayerAndSave();
        infinity = GameObject.Find("WaveSpawner").GetComponent<Infinitewaves>();
        chaneGems.text = ((infinity.currentWave-1) * 50).ToString();
        changeWave.text = (infinity.currentWave-1).ToString();
        _p.AddGems((infinity.currentWave-1) * 50);
        g.AddGems((infinity.currentWave-1) * 50);
        if (_p.PlayerScore.UserScore < infinity.currentWave)
        {
            _p.SetScore(infinity.currentWave-1);
            _Save.SaveScore(_p.PlayerScore, true);
        }
       _Save.SavePlayer(_p.PlayerData, true);
    }

    public void PlayerLoseStory()
    {
        normal = GameObject.Find("GameSpawner").GetComponent<WaveSpawner>();
        testoGems.text = "";
        chaneGems.text = "";
        changeWave.text = (normal.nextWave-1).ToString();
    }
    public void SavingNormal()
    {
        normal = GameObject.Find("GameSpawner").GetComponent<WaveSpawner>();
        SetPlayerAndSave();
        switch (SceneManager.GetActiveScene().name)
        {
            case "Story":
                if (!PlayerPrefs.HasKey("1"))
                {
                    PlayerPrefs.SetInt("1", 1);
                    GiveGems();
                }
                break;
            case "OutsideTheWall":
                if (!PlayerPrefs.HasKey("2"))
                {
                    PlayerPrefs.SetInt("2", 1);
                    GiveGems();
                }
                break;
            case "CaveTutorial":
                if (!PlayerPrefs.HasKey("3"))
                {
                    Debug.Log("cave");
                    PlayerPrefs.SetInt("3", 1);
                    GiveGems();
                }
                break;
            case "Forest":
                if (!PlayerPrefs.HasKey("4"))
                {
                    PlayerPrefs.SetInt("4", 1);
                    GiveGems();
                }
                break;
            case "Dungeon":
                if (!PlayerPrefs.HasKey("5"))
                {
                    PlayerPrefs.SetInt("5", 1);
                    GiveGems();
                }
                break;
        }
    }
    public void SetPlayerAndSave()
    {
        _p = GameObject.Find("PlayerThings/Player").GetComponent<Player>();
        _Save = FindObjectOfType<SaveSystem>();
    }
    public void GiveGems()
    {
        Debug.Log("gem");
        g.AddGems(250);
        _p.AddGems(250);
        _Save.SavePlayer(_p.PlayerData, true);
    }
}
