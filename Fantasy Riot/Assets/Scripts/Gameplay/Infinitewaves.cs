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

    [System.Serializable]
    public class Bosses
    {
        public GameObject[] boss;
        public int count;
        public float rate;
    }

    public GameObject TowerUI;
    public Enemies e;
    public Bosses b;

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
    public TMP_Text waveCounter;
    public GameObject abilityButton;
    public bool spawningEnemies;
    public bool road;

    void Start()
    {
        StarWaveSpowner = false;
        state = SpawnState.WAITING;
        abilityButton.SetActive(false);
    }

    void Update()
    {
        waveCounter.text = "Wave: " + (currentWave);
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
            bottoneNext.SetActive(false);
            TowerUI.SetActive(false);
            abilityButton.SetActive(true);
            spawningEnemies = true;
            if (state != SpawnState.SPAWNING)
            {
                StartCoroutine(SpawnWave());
            }
        }
        else
        {
            bottoneNext.SetActive(true);
            waveCountdown -= Time.deltaTime;
            string minutes = ((int)waveCountdown / 60).ToString();
            string seconds = (waveCountdown % 60).ToString("f0");
            timer.text = minutes + ":" + seconds;
        }
    }
    public void startWave()
    {
        StarWaveSpowner = true;
        waveCountdown = 0;
        bottoneGo.SetActive(false);
        timer.text = 0 + ":" + 0;
    }
    public void NextWave()
    {
        bottoneNext.SetActive(false);
        abilityButton.SetActive(true);
        waveCountdown = 0;
        timer.text = 0 + ":" + 0;
    }
    void WaveCompleted()
    {
        state = SpawnState.COUNTING;
        if (currentWave != 1)
            waveCountdown = timeBetweenWaves;
        StarWaveSpowner = true;
        TowerUI.SetActive(true);
        abilityButton.SetActive(false);
        spawningEnemies = false;
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
                    e.enemies[i].GetComponent<EnemyHealthManager>().MaxHealth += (currentWave*2);
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
        if (currentWave % 10 == 0)
        {
            for (int i = 0; i < 1; i++)
            {
                int index = Random.Range(0, spawnPoints.Length);
                SpawnEnemy(b.boss[Random.Range(0, 5)], index);
                Foll(index);
                yield return new WaitForSeconds(1f / e.rate);
            }
            for (int i = 0; i < e.count; i++)
            {
                int index = Random.Range(0, spawnPoints.Length);
                SpawnEnemy(e.enemies[Random.Range(0, 12)], index);
                Foll(index);
                yield return new WaitForSeconds(1f / e.rate);
            }
        }
        else
        {
            for (int i = 0; i < e.count; i++)
            {
                int index = Random.Range(0, spawnPoints.Length);
                SpawnEnemy(e.enemies[Random.Range(0, 12)], index);
                Foll(index);
                yield return new WaitForSeconds(1f / e.rate);
            }
        }
        state = SpawnState.WAITING;
        yield break;
    }

    void SpawnEnemy(GameObject enemy, int index)
    {
        Transform _sp = spawnPoints[index];
        Instantiate(enemy, _sp.transform.position, _sp.transform.rotation);
    }

    void SpawnBoss(GameObject boss, int index)
    {
        Transform _sp = spawnPoints[index];
        Instantiate(boss, _sp.transform.position, _sp.transform.rotation);
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