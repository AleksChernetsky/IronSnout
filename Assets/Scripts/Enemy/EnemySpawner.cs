using System;
using System.Collections.Generic;

using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private int _amountToPool;
    [SerializeField] private float _spawnSpeedMin;
    [SerializeField] private float _spawnSpeedMax;
    [SerializeField] private EnemyActions[] _enemyPrefab;
    [SerializeField] private Transform[] _spawnPoints;

    private List<GameObject> pooledEnemies = new List<GameObject>();
    private float _randomSpawnTime;
    private float _timer;

    private void Start()
    {
        CreatePool();
        GlobalEvents.OnDieEvent.AddListener(DecreaseSpawnTime);
    }

    private void Update()
    {
        _timer += Time.deltaTime;
        if (_timer >= _randomSpawnTime)
        {
            GetEnemy().SetActive(true);
            _randomSpawnTime = UnityEngine.Random.Range(_spawnSpeedMin, _spawnSpeedMax);
            _timer = 0;
        }
    }
    private void CreatePool()
    {
        for (int i = 0; i < _amountToPool; i++)
        {
            CreateEnemy();
        }
    }

    private GameObject CreateEnemy()
    {
        int random = UnityEngine.Random.Range(0, 3);
        GameObject enemy = Instantiate(_enemyPrefab[random].gameObject, transform);
        enemy.SetActive(false);
        pooledEnemies.Add(enemy);
        return enemy;
    }
    private GameObject GetEnemy()
    {
        int randomEnemy = UnityEngine.Random.Range(0, pooledEnemies.Count);
        int randomSpawnPoint = UnityEngine.Random.Range(0, _spawnPoints.Length);

        if (!pooledEnemies[randomEnemy].activeInHierarchy)
        {
            pooledEnemies[randomEnemy].transform.position = _spawnPoints[randomSpawnPoint].position;
            return pooledEnemies[randomEnemy];
        }
        else
        {
            return GetFreeEnemy(randomSpawnPoint);
        }
    }
    private GameObject GetFreeEnemy(int randomSpawnPoint)
    {
        for (int i = 0; i < pooledEnemies.Count; i++)
        {
            if (!pooledEnemies[i].activeInHierarchy)
            {
                pooledEnemies[i].transform.position = _spawnPoints[randomSpawnPoint].position;
                return pooledEnemies[i];
            }
        }
        throw new Exception("There is no free element in pool");
    }
    private void DecreaseSpawnTime()
    {
        if (_spawnSpeedMin > 1)
        {
            _spawnSpeedMin -= 0.1f;
        }
        if (_spawnSpeedMax > 3)
        {
            _spawnSpeedMax -= 0.1f;
        }
    }
}
