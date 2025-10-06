using System.Collections.Generic;
using UnityEngine;

public class BaseEnemySpawner : MonoBehaviour
{
    public BaseEnemy baseEnemyPrefab;
    public float baseSpawnRate = 1f;
    public float spawnRadius = 5f;
    private bool _spawningEnabled = true;
    private float _spawnRateMultiplier = 1f;
    private float _lastSpawnTime = 0f;
    public Camera _mainCamera;
    public List<BaseEnemy> enemies = new List<BaseEnemy>();

    public bool SpawningEnabled
    {
        get => _spawningEnabled;
        set => _spawningEnabled = value;
    }

    public float SpawnRateMultiplier
    {
        get => _spawnRateMultiplier;
        set => _spawnRateMultiplier = value;
    }

    private void Start()
    {
        _mainCamera = Camera.main;
    }
    private void Update()
    {
        TrySpawning();
    }

    private void TrySpawning()
    {
        if (!_spawningEnabled) return;

        float spawnInterval = 1f / (baseSpawnRate * _spawnRateMultiplier);
        if (Time.time >= _lastSpawnTime + spawnInterval)
        {
            SpawnEnemy();
            _lastSpawnTime = Time.time;
        }
    }

    private void SpawnEnemy()
    {
        Vector3 spawnPos = GetRandomSpawnPos();
        var newEnemy = Instantiate(baseEnemyPrefab, spawnPos, Quaternion.identity);
        enemies.Add(newEnemy);
        newEnemy.Initialize();
    }

    public Vector3 GetRandomSpawnPos()
    {
        if (!_mainCamera)
        {
            _mainCamera = Camera.main;
            if (!_mainCamera) return transform.position;
        }

        Vector3 spawnPoint;
        int edge = Random.Range(0, 4);
        float offset = 0.1f;

        switch (edge)
        {
            case 0: // Top
                spawnPoint = _mainCamera.ViewportToWorldPoint(new Vector3(Random.Range(0f, 1f), 1 + offset, _mainCamera.nearClipPlane));
                break;
            case 1: // Right
                spawnPoint = _mainCamera.ViewportToWorldPoint(new Vector3(1 + offset, Random.Range(0f, 1f), _mainCamera.nearClipPlane));
                break;
            case 2: // Bottom
                spawnPoint = _mainCamera.ViewportToWorldPoint(new Vector3(Random.Range(0f, 1f), -offset, _mainCamera.nearClipPlane));
                break;
            case 3: // Left
                spawnPoint = _mainCamera.ViewportToWorldPoint(new Vector3(-offset, Random.Range(0f, 1f), _mainCamera.nearClipPlane));
                break;
            default:
                spawnPoint = _mainCamera.ViewportToWorldPoint(new Vector3(1 + offset, Random.Range(0f, 1f), _mainCamera.nearClipPlane));
                break;
        }

        spawnPoint.z = 0f;
        return spawnPoint;
    }

}