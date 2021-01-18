using System.Collections;
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
    public TMP_Text waveCounter;
    public GameObject abilityButton;
    public GameObject TowerUI;
    public bool road;

    public AudioSource begin, end;

    void Start()
    {
        StarWaveSpowner = false;
        state = SpawnState.WAITING;
        winner = GameObject.Find("Canvas").transform.Find("WinnerPanel").GetComponent<WinnerPanel>();
        abilityButton.SetActive(false);
    }

    void Update() 
    {
        waveCounter.text = "Wave: " + (nextWave);
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
            abilityButton.SetActive(true);
            TowerUI.SetActive(false);
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
        begin.Play();
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
        abilityButton.SetActive(false);
        TowerUI.SetActive(true);

        if (nextWave < waves.Length -1)
        {
            nextWave++;
        } 

        else if (nextWave == waves.Length - 1)
        {
            end.Play();
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
            int index = Random.Range(0, spawnPoints.Length);
            SpawnEnemy(_wave.enemy, index);
            Foll(index);
            yield return new WaitForSeconds(1f /_wave.rate);
        }

        for (int i = 0; i < _wave.count1; i++)
        {
            int index = Random.Range(0, spawnPoints.Length);
            SpawnEnemy1(_wave.enemy1, index);
            Foll(index);
            yield return new WaitForSeconds(1f / _wave.rate);
        }

        for (int i = 0; i < _wave.count2; i++)
        {
            int index = Random.Range(0, spawnPoints.Length);
            SpawnEnemy2(_wave.enemy2, index);
            Foll(index);
            yield return new WaitForSeconds(1f / _wave.rate);
        }


        state = SpawnState.WAITING;

        yield break;
    }

    void SpawnEnemy (Transform _enemy, int index)
    {
        Transform _sp = spawnPoints[index];
        Instantiate(_enemy, _sp.transform.position, _sp.transform.rotation);
    }

    void SpawnEnemy1(Transform _enemy1, int index)
    {
        Transform _sp = spawnPoints[index];
        Instantiate(_enemy1, _sp.transform.position, _sp.transform.rotation);
    }

    void SpawnEnemy2(Transform _enemy2, int index)
    {
        Transform _sp = spawnPoints[index];
        Instantiate(_enemy2, _sp.transform.position, _sp.transform.rotation);
    }

    bool Foll(int index)
    {
        if (index == 1)
        {
            road = true;
        }
        else
        {
            road = false;
        }
        return road;
    }
        
}
