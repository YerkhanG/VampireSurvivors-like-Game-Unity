using System.Collections.Generic;
using Mono.Cecil.Cil;
using Unity.Multiplayer.Center.Common;
using UnityEngine;
using UnityEngine.Rendering;


public abstract class BaseEnemy : MonoBehaviour, IDamageable
{
    [Header("Enemy Properties")]
    [SerializeField] protected float health;
    public GameObject enemyPrefab;
    protected abstract float LastSpawnTime { get; set; }
    protected abstract bool IsSpawningEnabled { get; set; }
    protected abstract float SpawnRateMultiplier { get; set; }
    public float individualSpawnRate;

    protected virtual float FinalSpawnRate => SpawnRateMultiplier * individualSpawnRate;

    protected virtual float TimeBetweenSpawns => 1f / FinalSpawnRate;

    protected abstract void InitializeEnemy();
    protected virtual bool CanEnemySpawn()
    {
        bool isItOkToSpawnByTime = Time.time >= LastSpawnTime + TimeBetweenSpawns;
        return IsSpawningEnabled && isItOkToSpawnByTime;
    }

    protected virtual void Awake()
    {
        // Automatically add movement controller if none exists
        if (!TryGetComponent<EnemyMovementController>(out _))
        {
            gameObject.AddComponent<EnemyMovementController>();
        }
    }
    public virtual void TrySpawning()
    {
        if (!CanEnemySpawn()) return;
        Vector3 spawnPos = GetRandomSpawnPos();
        GameObject newEnemy = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);


        BaseEnemy enemyScript = newEnemy.GetComponent<BaseEnemy>();
        if (enemyScript != null)
        {
            enemyScript.InitializeEnemy();
        }
        LastSpawnTime = Time.time;
    }

    protected Vector3 GetRandomSpawnPos()
    {
        Camera cam = Camera.main;
        Vector3 spawnPoint;

        int edge = Random.Range(0, 4);
        //slightly outside screen
        float offset = 1.1f;


        switch (edge)
        {
            case 0:
                spawnPoint = cam.ViewportToWorldPoint(new Vector3(Random.Range(0f, 1f), offset, cam.nearClipPlane));
                break;
            case 1: // Right
                spawnPoint = cam.ViewportToWorldPoint(new Vector3(offset, Random.Range(0f, 1f), cam.nearClipPlane));
                break;
            case 2: // Bottom
                spawnPoint = cam.ViewportToWorldPoint(new Vector3(Random.Range(0f, 1f), -offset + 1f, cam.nearClipPlane));
                break;
            case 3: // Left
                spawnPoint = cam.ViewportToWorldPoint(new Vector3(-offset + 1f, Random.Range(0f, 1f), cam.nearClipPlane));
                break;
            default:
                spawnPoint = cam.ViewportToWorldPoint(new Vector3(offset, Random.Range(0f, 1f), cam.nearClipPlane));
                break;
        }
        spawnPoint.z = 0f;
        return spawnPoint;

    }


    private void HandleCollision(GameObject other)
    {
        if (other.TryGetComponent<Projectile>(out var projectile))
        {
            TakeDamage(projectile.Damage, other);
            Debug.Log("Enemy takes damage");
            return;
        }

        if (other.CompareTag("Player"))
        {
            if (other.TryGetComponent<IDamageable>(out var playerDamageable))
            {
                playerDamageable.TakeDamage(10f, gameObject);
                Debug.Log("Player takes damage");
            }
            return;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        HandleCollision(collision.gameObject);
    }

    public virtual  void TakeDamage(float amount, GameObject damageSource)
    {
        health -= amount;
        if (health <= 0) Die();
    }

    public virtual void Die()
    {
        Destroy(gameObject);
    }
}