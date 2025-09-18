using UnityEngine;

public interface IDamageable
{
    public void TakeDamage(float amount, GameObject damageSource);
    public void Die();
}