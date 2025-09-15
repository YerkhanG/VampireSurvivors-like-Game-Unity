using System.Collections.Generic;
using Mono.Cecil.Cil;
using Unity.Multiplayer.Center.Common;
using UnityEngine;
using UnityEngine.Rendering;


public abstract class BaseEnemy : MonoBehaviour, IDamageable
{
    [Header("XP Drop Settings")]
    [SerializeField] private GameObject xpGemPrefab;
    [SerializeField] private float xpDropAmount = 20f;

    [Header("Enemy Properties")]
    [SerializeField] protected float health;

    protected virtual void Awake()
    {
        if (!TryGetComponent<EnemyMovementController>(out _))
        {
            gameObject.AddComponent<EnemyMovementController>();
        }
    }
    public abstract void Initialize();
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
    private void DropXPGems()
    {
        if (xpGemPrefab == null)
        {
            Debug.Log("There needs to be a xpGemPrefab");
            return;
        }

        Vector3 dropPosition = transform.position;

        GameObject gem = Instantiate(xpGemPrefab, dropPosition, Quaternion.identity);
        XpGem xpGem = gem.GetComponent<XpGem>();

        if (xpGem != null)
        {
            xpGem.SetXpValue(xpDropAmount);
        }
    }

    public virtual void Die()
    {
        DropXPGems();
        Destroy(gameObject);
    }
}