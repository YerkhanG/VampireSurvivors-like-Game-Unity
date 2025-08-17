using NUnit.Framework.Constraints;
using UnityEngine;

public class RegularSkeletonEnemy : BaseEnemy
{
    [Header("Stats")]
    public float skeletonHealth = 100f;
    public float damage = 10f;
    public float moveSpeed = 3f;

    private EnemyMovementController _movement;

    protected override float LastSpawnTime { get; set; }
    protected override bool IsSpawningEnabled {  get; set; } = true ;
    protected override float SpawnRateMultiplier { get; set; } = 1f;

    public void Initialize()
    {
        health = skeletonHealth;
        _movement = GetComponent<EnemyMovementController>();
        if (_movement != null)
        {
            _movement.SetMoveSpeed(moveSpeed);
        }
    }

    protected override void InitializeEnemy()
    {
        throw new System.NotImplementedException();
    }
}