using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SaveScore : MonoBehaviour
{
    public Player _p;
    public SaveSystem _Save;
    public Infinitewaves i;
    public GemsManager g;
    public void SavingInfinite()
    {
        SetPlayerAndSave();
        i = GameObject.Find("WaveSpawner").GetComponent<Infinitewaves>();
        _p.AddGems(i.currentWave * 50);
        g.AddGems(i.currentWave * 50);
        if (_p.PlayerScore.UserScore < i.currentWave)
        {
            Debug.Log("ScoreChanged :)");
            _p.SetScore(i.currentWave);
            _Save.SaveScore(_p.PlayerScore, true);
        }
        else
            Debug.Log("ScoreNotSaved :(");
        _Save.SavePlayer(_p.PlayerData, true);
    }

    public void SavingNormal()
    {
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
        g.AddGems(250);
        _p.AddGems(250);
        _Save.SavePlayer(_p.PlayerData, true);
    }
}
