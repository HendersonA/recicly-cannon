using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public static Action<float> OnCountDown;
    public enum SpawnState
    {
        SPAWNING,
        WAITING,
        COUNTING
    }

    [Serializable]
    public class EnemyWave
    {
        public Transform enemy;
        public int count;
    }

    [Serializable]
    public class Wave
    {
        public string name;
        public List<EnemyWave> enemyList;
        public float rate;
    }

    [SerializeField] private Wave[] waves;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private float timeBetweenWaves = 5f;

    private int nextWave = 0;
    private float waveCountdown;
    private float searchCountdown = 1f;
    private SpawnState state = SpawnState.COUNTING;

    void Start()
    {
        if (spawnPoints.Length == 0)
        {
            Debug.Log("Spawn não referenciado");
        }
        waveCountdown = timeBetweenWaves;
    }

    void Update()
    {
        if (state == SpawnState.WAITING)
        {
            if (!EnemyIsAlive())
            {
                WaveCompleted();
            }
            else
            {
                return;
            }
        }
        if (waveCountdown <= 0)
        {
            if (state != SpawnState.SPAWNING)
            {
                StartCoroutine(SpawnWave(waves[nextWave]));
            }
        }
        else
        {
            waveCountdown -= Time.deltaTime;
            if (OnCountDown != null)
                OnCountDown(waveCountdown);
        }
    }

    private void WaveCompleted()
    {

        state = SpawnState.COUNTING;

        waveCountdown = timeBetweenWaves;

        if (nextWave + 1 > waves.Length - 1)
        {
            nextWave = 0;
        }
        else
        {
            nextWave++;
        }
    }

    private bool EnemyIsAlive()
    {
        searchCountdown -= Time.deltaTime;

        if (searchCountdown <= 0f)
        {
            searchCountdown = 1f;

            if (GameObject.FindGameObjectWithTag("Enemy") == null)
            {
                return false;
            }
        }

        return true;
    }

    private IEnumerator SpawnWave(Wave _wave)
    {
        state = SpawnState.SPAWNING;

        foreach (var item in _wave.enemyList)
        {
            SpawnEnemy(item);
            yield return new WaitForSeconds(1f / _wave.rate);
        }

        state = SpawnState.WAITING;

        yield break;
    }

    private void SpawnEnemy(EnemyWave _enemy)
    {
        for (int i = 0; i < _enemy.count; i++)
        {
            Transform _sp = spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Length)];
            Instantiate(_enemy.enemy, _sp.position, _sp.rotation);
        }
    }
}
