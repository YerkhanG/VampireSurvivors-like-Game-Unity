using UnityEngine;

public class OrbitMovement : MonoBehaviour
{
    public Transform center;
    public float radius = 3f;
    public float speed = 2f;
    public float duration = 8f;

    private float angle = 0f;
    private float timeAlive = 0f;
    private Rigidbody2D rb;
    private Vector3 previousPosition;
    private bool isInitialized = false;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
        }
        var projectile = GetComponent<Projectile>();
        if (projectile != null)
        {
            projectile.enabled = false;
        }
        if (center != null)
        {
            Vector3 offset = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * radius;
            transform.position = center.position + offset;
            previousPosition = transform.position;
            isInitialized = true;
        }
    }

   void Update()
    {
        if (!isInitialized) return;
        
        timeAlive += Time.deltaTime;
        if (timeAlive >= duration)
        {
            Destroy(gameObject);
            return;
        }
        
        if (center != null)
        {
            angle += speed * Time.deltaTime;

            Vector3 offset = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * radius;
            Vector3 newPosition = center.position + offset;

            Vector3 tangentDirection = new Vector3(-Mathf.Sin(angle), Mathf.Cos(angle), 0).normalized;

            transform.position = newPosition;

            float rotationAngle = Mathf.Atan2(tangentDirection.y, tangentDirection.x) * Mathf.Rad2Deg;
            rotationAngle -= 45; 
            transform.rotation = Quaternion.AngleAxis(rotationAngle, Vector3.forward);
            
            previousPosition = newPosition;
        }
    }
    
    void OnDestroy()
    {
        var projectile = GetComponent<Projectile>();
        if (projectile != null)
        {
            projectile.enabled = true;
        }
    }
}