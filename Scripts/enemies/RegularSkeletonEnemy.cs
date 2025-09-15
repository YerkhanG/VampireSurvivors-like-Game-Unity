using NUnit.Framework.Constraints;
using UnityEngine;

public class RegularSkeletonEnemy : BaseEnemy
{
    [Header("Stats")]
    public float skeletonHealth = 100f;
    public float damage = 10f;
    public float moveSpeed = 3f;

    private EnemyMovementController _movement;

    public override void Initialize()
    {
        health = skeletonHealth;
        _movement = GetComponent<EnemyMovementController>();
        if (_movement != null)
        {
            _movement.SetMoveSpeed(moveSpeed);
        }
    }

}