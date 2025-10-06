using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerManager : MonoBehaviour
{
    [Header("Spawners")] public SkeletonSpawner skeletonSpawner;
    [Header("Teleport Settings")] public Transform teleportPoint;
    public float teleportTimer;
    public float maxDistanceFromPlayer;
    private float _teleportCheckTimer = 0f;

    public void Update()
    {
        _teleportCheckTimer += Time.deltaTime;
        if (_teleportCheckTimer >= teleportTimer)
        {
            _teleportCheckTimer = 0;
            CheckAndTeleportEnemies(skeletonSpawner, teleportPoint, maxDistanceFromPlayer);
        }
    }

    private void CheckAndTeleportEnemies(BaseEnemySpawner spawner, Transform teleportPos, float maxDistance)
    {
        List<BaseEnemy> enemiesToRemove = new List<BaseEnemy>();
        foreach (BaseEnemy enemy in spawner.enemies)
        {
            if (!enemy)
            {
                enemiesToRemove.Add(enemy);
                continue;
            }
            
            // ✅ Check if enemy GameObject is destroyed
            if (!enemy.gameObject)
            {
                enemiesToRemove.Add(enemy);
                continue;
            }
            
            // ✅ Check if enemy is active and alive
            if (!enemy.gameObject.activeInHierarchy)
            {
                enemiesToRemove.Add(enemy);
                continue;
            }

            var distance = (enemy.transform.position - teleportPos.position).sqrMagnitude;
            if (distance > maxDistance)
            {
                TeleportEnemyToPlayer(teleportPos, enemy, spawner);
            }
            
        }
        foreach (BaseEnemy deadEnemy in enemiesToRemove)
        {
            spawner.enemies.Remove(deadEnemy);
        }
    }

    private void TeleportEnemyToPlayer(Transform teleportPos, BaseEnemy enemy, BaseEnemySpawner spawner)
    {
        var spawnPoint = spawner.GetRandomSpawnPos();
        enemy.transform.position = spawnPoint;
    }

    public void SetWave(int waveNumber)
    {
        switch (waveNumber)
        {
            case 1:
                skeletonSpawner.SpawningEnabled = true;
                skeletonSpawner.SpawnRateMultiplier = 1f;
                break;
            case 5:
                skeletonSpawner.SpawnRateMultiplier = 2f;
                break;
            case 10:
                skeletonSpawner.SpawningEnabled = false;
                break;
        }
    }
}