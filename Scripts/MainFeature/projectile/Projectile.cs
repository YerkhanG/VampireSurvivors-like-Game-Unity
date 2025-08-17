using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float Damage { get; set; } = 20f;
    public float Speed { get; set; } = 5f;
    public float Pierce { get; set; } = 2f;
    public Vector2 Direction { get; set; }

    private Rigidbody2D rb;
    void Awake() => rb = GetComponent<Rigidbody2D>();
    void Start() => rb.linearVelocity = Direction * Speed;
    void Update()
{
    if (rb.linearVelocity != Vector2.zero)
    {
        float angle = Mathf.Atan2(rb.linearVelocity.y, rb.linearVelocity.x) * Mathf.Rad2Deg;
        angle -= 45;
        Debug.Log($"Projectile Angle: {angle}");
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<BaseEnemy>(out var enemy))
        {
            enemy.TakeDamage(Damage, gameObject);
            Pierce--;
            if (Pierce <= 0) Destroy(gameObject);
        }   
    }
}