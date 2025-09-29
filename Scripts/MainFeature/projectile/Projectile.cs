using System;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Projectile properties")]
    public float Damage { get; set; } = 20f;
    public float Speed { get; set; } = 5f;
    public float Pierce { get; set; } = 2f;
    public Vector2 Direction { get; set; }
    public float TTL { get; set; } = 3f;
    private BaseSpell sourceSpell;

    public event Action<BaseEnemy, GameObject> OnEnemyHit;
    private Rigidbody2D rb;
    void Awake() => rb = GetComponent<Rigidbody2D>();
    void Start() => rb.linearVelocity = Direction * Speed;
    void Update()
    {
        if (TTL > 0)
        {
            TTL -= Time.deltaTime;
        }
        else if (TTL <= 0)
        {
            Destroy(this.gameObject);
        }
        if (rb.linearVelocity != Vector2.zero)
        {
            float angle = Mathf.Atan2(rb.linearVelocity.y, rb.linearVelocity.x) * Mathf.Rad2Deg;
            angle -= 45;
            Debug.Log($"Projectile Angle: {angle}");
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }
    public void SetSpell(BaseSpell spell)
    {
        sourceSpell = spell;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<BaseEnemy>(out var enemy))
        {
            enemy.TakeDamage(Damage, gameObject);
            if (sourceSpell != null)
            {
                sourceSpell.PlayHitEffect(transform.position, -Direction);
            }
            Debug.Log("Trying to debug forking: " + enemy.name + gameObject.name);
            OnEnemyHit?.Invoke(enemy, gameObject);
            Pierce--;
            if (Pierce <= 0) Destroy(gameObject);
        }   
    }
}