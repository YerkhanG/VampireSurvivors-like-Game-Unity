using UnityEngine;

public class PlayerController : MonoBehaviour, IDamageable
{
    public System.Action<float, float> HPAction;
    PlayerMovement pl;
    [SerializeField] private float health = 100f;
    [SerializeField] private float maxHealth = 100f;
     public float CurrentHealth => health;
    public float MaxHealth => maxHealth;
    public void IncreaseMaxHealth(float amount)
    {
        maxHealth += amount;
        health += amount; 
        HPAction?.Invoke(health, maxHealth);
        
        Debug.Log($"Max health increased by {amount}. New max: {maxHealth}, Current: {health}");
    }

    public void Awake()
    {
        pl = GetComponent<PlayerMovement>();
    }

    public void Die()
    {
        pl.speed = 0f;
    }

    public void TakeDamage(float amount, GameObject damageSource)
    {
        health -= amount;
        HPAction?.Invoke(health, MaxHealth);
        FlashRed();
        if (health <= 0) Die();
    }
    private void FlashRed()
    {
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        if (sprite != null)
        {
            StartCoroutine(FlashCoroutine(sprite));
        }
    }
    
    private System.Collections.IEnumerator FlashCoroutine(SpriteRenderer sprite)
    {
        Color originalColor = sprite.color;
        sprite.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        sprite.color = originalColor;
    }
}