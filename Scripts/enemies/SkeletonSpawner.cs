using UnityEngine;

public class SkeletonSpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    public RegularSkeletonEnemy skeletonPrefab;
    public float baseSpawnRate = 1f;
    public float spawnRadius = 5f;
    
    // Non-static now since each spawner should manage its own state
    private bool _spawningEnabled = true;
    private float _spawnRateMultiplier = 1f;
    private float _lastSpawnTime = 0f;
    
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
        Vector2 spawnPos = (Vector2)transform.position + Random.insideUnitCircle * spawnRadius;
        var newEnemy = Instantiate(skeletonPrefab, spawnPos, Quaternion.identity);
        newEnemy.Initialize();
    }
}