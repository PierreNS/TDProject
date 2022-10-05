using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager instance { get; private set; }

    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private float _spawnTimer;
    [SerializeField] private float _delayBetweenSpawn;
    [SerializeField] private int _spawnAmount;
    [SerializeField] private int _spawnAmountIncrease;
    [SerializeField] private PlayerResources _playerResource;

    private float _currentTime;
    private int _enemyAmount;
    private bool _spawnEnemies = true;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }

    private void Update()
    {
        SpawnEnemy();
    }

    private void SpawnEnemy()
    {
        if (_spawnEnemies == false) return;

        if (_currentTime < Time.time)
        {
            _currentTime = Time.time + _spawnTimer;
            _spawnAmount += _spawnAmountIncrease;
            StartCoroutine(SpawnRoutine());
        }
    }

    IEnumerator SpawnRoutine() 
    {
        for (int i = 0; i < _spawnAmount; i++)
        {
            var enemy = Instantiate(_enemyPrefab, _spawnPoint.position, _spawnPoint.rotation);
            enemy.GetComponent<EnemyHitpoints>().DieEvent += DecreaseEnemyAmount;
            IncreaseEnemyAmount();
            yield return new WaitForSeconds(_delayBetweenSpawn);
        }
    }

    private void IncreaseEnemyAmount()
    {
        _enemyAmount++;
        UpdateUI();
    }

    private void DecreaseEnemyAmount()
    {
        _enemyAmount--;
        UpdateUI();
        _playerResource.AddCredits(150);
    }

    public void DisableSpawning() 
    {
        _spawnEnemies = false;
        StopAllCoroutines();
    }

    private void UpdateUI() 
    {
        UIManager.Instance.SetEnemyAmount(_enemyAmount);
    }
}
