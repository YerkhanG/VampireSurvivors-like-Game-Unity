using UnityEngine;

public class PlayerController : MonoBehaviour, IDamageable
{
    PlayerMovement pl;
    [SerializeField]private float health = 100f;
    public float CurrentHealth => health;
    public float MaxHealth => 100f;
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