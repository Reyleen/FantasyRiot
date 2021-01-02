using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Infinitewaves : MonoBehaviour
{
    public enum SpawnState { SPAWNING, WAITING, COUNTING };

    [System.Serializable]
    public class Enemies
    {
        public GameObject[] enemies;
        public int count;
        public float rate;
    }

    public GameObject TowerUI;
    public Enemies e;

    public int currentWave = 0;

    public Transform[] spawnPoints;

    public float timeBetweenWaves = 45f;
    public float waveCountdown = 0;
    private float searchCountdown = 1f;

    private SpawnState state = SpawnState.COUNTING;
    public bool StarWaveSpowner;
    public GameObject bottoneGo;
    public GameObject bottoneNext;
    public TMP_Text timer;

    void Start()
    {
        TowerUI = GameObject.FindGameObjectWithTag("TowerManager");
        StarWaveSpowner = false;
        state = SpawnState.WAITING;
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
            TowerUI.SetActive(false);
            if (state != SpawnState.SPAWNING)
            {
                StartCoroutine(SpawnWave());
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
        TowerUI.SetActive(true);
        bottoneGo.SetActive(true);

    }

    bool EnemyIsAlive()
    {
        searchCountdown -= Time.deltaTime;

        if (searchCountdown <= 0f)
        {
            searchCountdown = 2f;
            if (GameObject.FindGameObjectWithTag("Enemy") == null)
            {
                e.count += 2;
                for(int i = 0; i < 2; i++)
                {
                    e.enemies[i].GetComponent<EnemyHealthManager>().MaxHealth += 10;
                }
                currentWave++;
                return false;
            }
        }
        return true;
    }

    IEnumerator SpawnWave()
    {
        state = SpawnState.SPAWNING;
        for (int i = 0; i < e.count; i++)
        {
            SpawnEnemy(e.enemies[Random.Range(0,6)]);
            yield return new WaitForSeconds(1f / e.rate);
        }
        state = SpawnState.WAITING;
        yield break;
    }

    void SpawnEnemy(GameObject enemy)
    {
        Transform _sp = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(enemy, _sp.transform.position, _sp.transform.rotation);
    }
}