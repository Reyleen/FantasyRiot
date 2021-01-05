﻿using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WaveSpawner : MonoBehaviour
{
    public enum SpawnState { SPAWNING, WAITING, COUNTING };

    [System.Serializable]
    public class Wave
    {
        public string name;
        public Transform enemy;
        public Transform enemy1;
        public Transform enemy2;
        public int count;
        public int count1;
        public int count2;
        public float rate; 
    }
    //public GameObject TowerUI;
    public Wave[] waves;

    public int nextWave = 0;

    public Transform[] spawnPoints;

    public float timeBetweenWaves = 60f;
    public float waveCountdown = 0;
    private float searchCountdown = 1f;

    private SpawnState state = SpawnState.COUNTING;
    public bool StarWaveSpowner;
    public GameObject bottoneGo;
    public GameObject bottoneNext;
    public TMP_Text timer;

    private WinnerPanel winner;

    void Start()
    {
        //TowerUI = GameObject.FindGameObjectWithTag("TowerManager");
        StarWaveSpowner = false;
        state = SpawnState.WAITING;
        winner = GameObject.Find("Canvas").transform.Find("WinnerPanel").GetComponent<WinnerPanel>();
    }

    void Update() 
    {
        if (!StarWaveSpowner)
            return;
        if (state == SpawnState.WAITING)
        {
            if (!EnemyIsAlive())
            {
                WaveCompleted();
                return;
            }
            else
            {
                return;
            }
        }


            if (waveCountdown <= 0)
            {
                //TowerUI.SetActive(false);
                if (state != SpawnState.SPAWNING)
                {
                    StartCoroutine(SpawnWave(waves[nextWave]));
                }
                
            }
            else
            {
                waveCountdown -= Time.deltaTime;
                string minutes = ((int)waveCountdown / 60).ToString();
                string seconds = (waveCountdown % 60).ToString("f0");
                timer.text = minutes + ":" + seconds;
            }

    }
    public void startWave()
    {
        StarWaveSpowner = true;
        waveCountdown = timeBetweenWaves;
        bottoneGo.SetActive(false);
    }
    public void NextWave()
    {
        bottoneNext.SetActive(false);
        waveCountdown = 0;
    }
    void WaveCompleted()
    {
        state = SpawnState.COUNTING;
        waveCountdown = timeBetweenWaves;
        StarWaveSpowner = true;
        //TowerUI.SetActive(true);
        bottoneGo.SetActive(true);
        if (nextWave < waves.Length -1)
        {
            nextWave++;
        } 

        else if (nextWave == waves.Length)
        {
            winner.ToggleWinPan();
            Time.timeScale = 0f;
        }
    }

    bool EnemyIsAlive()
    {
        searchCountdown -= Time.deltaTime;

        if (searchCountdown <= 0f)
        {
            searchCountdown = 2f;
            if (GameObject.FindGameObjectWithTag("Enemy") == null)
            {
                return false;
            }
        }
        return true;
    }

    IEnumerator SpawnWave(Wave _wave)
    {
        state = SpawnState.SPAWNING;

        for(int i = 0; i < _wave.count; i++)
        {
            SpawnEnemy(_wave.enemy);
            yield return new WaitForSeconds(1f /_wave.rate);
        }

        for (int i = 0; i < _wave.count1; i++)
        {
            SpawnEnemy1(_wave.enemy1);
            yield return new WaitForSeconds(1f / _wave.rate);
        }

        for (int i = 0; i < _wave.count2; i++)
        {
            SpawnEnemy2(_wave.enemy2);
            yield return new WaitForSeconds(1f / _wave.rate);
        }


        state = SpawnState.WAITING;

        yield break;
    }

    void SpawnEnemy (Transform _enemy)
    {
        Transform _sp = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(_enemy, _sp.transform.position, _sp.transform.rotation);
    }

    void SpawnEnemy1(Transform _enemy1)
    {
        Transform _sp = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(_enemy1, _sp.transform.position, _sp.transform.rotation);
    }

    void SpawnEnemy2(Transform _enemy2)
    {
        Transform _sp = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(_enemy2, _sp.transform.position, _sp.transform.rotation);
    }
}
