using System;
using UnityEngine;

public class ForkingProjectile : MonoBehaviour
{
    public float forkingRadius = 25f;
    public int forkingCount = 1;
    public LayerMask enemyLayer;
    private Projectile projectile;
    private GameObject projectilePrefab;
    private BaseEnemy lastHitEnemy;
    void Awake()
    {
        Debug.Log("Awake at 14 forking");
        projectile = GetComponent<Projectile>();
        if (projectile == null)
        {
            enabled = false;
            return;
        }
    }
    void OnEnable()
    {
        projectile.OnEnemyHit += HandleEnemyHit;
    }
    void OnDisable()
    {
        projectile.OnEnemyHit -= HandleEnemyHit;
    }

    private void HandleEnemyHit(BaseEnemy enemy, GameObject @object)
    {
        if (forkingCount > 0)
        {
            lastHitEnemy = enemy;
            ForkProjectile(enemy.transform.position);
            forkingCount--;
            lastHitEnemy = null;
        }
    }

    private void ForkProjectile(Vector3 pointOfForking)
    {
        
        Collider2D nearestEnemy = FindNearestEnemy(pointOfForking);
        if (nearestEnemy != null)
        {
            // GameObject newProjPre = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            // Projectile newProj = newProjPre.GetComponent<Projectile>();
            Projectile newProj = Instantiate(projectile, transform.position, Quaternion.identity);
            // if (newProj == null)
            // {
            //     Debug.LogError("Instantiated prefab does not have a Projectile component!", newProjPre);
            //     Destroy(newProjPre);
            //     return;
            // }
            newProj.Damage = projectile.Damage;
            newProj.Pierce = projectile.Pierce;
            newProj.Direction = (nearestEnemy.transform.position - transform.position).normalized;
            newProj.TTL = projectile.TTL;
            newProj.Speed = projectile.Speed;

            var newForkingMod = newProj.gameObject.AddComponent<ForkingProjectile>();
            newForkingMod.forkingRadius = forkingRadius;
            newForkingMod.forkingCount = forkingCount;
            newForkingMod.enemyLayer = enemyLayer;
        }
    }
    private Collider2D FindNearestEnemy(Vector3 centerPosition)
    {
        float minDis = forkingRadius + 1f;
        Collider2D foundNearestEnemy = null;
        Collider2D[] colliders2D = Physics2D.OverlapCircleAll(centerPosition, forkingRadius, enemyLayer);
        foreach (Collider2D c in colliders2D)
        {
            if (lastHitEnemy != null && c.TryGetComponent<BaseEnemy>(out var currentEnemy) && currentEnemy == lastHitEnemy)
            {
                continue; 
            }
            var distance = Vector2.Distance(centerPosition, c.transform.position);
            if (distance < minDis)
            {
                minDis = distance;
                foundNearestEnemy = c;
                Debug.Log("84 nearest enemy: " + c.name);
            }
        }
        return foundNearestEnemy;
    }
} 