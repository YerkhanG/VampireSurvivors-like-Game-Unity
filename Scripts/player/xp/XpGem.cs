using UnityEngine;

public class XpGem : MonoBehaviour
{
    private float magnetRadius = 3.5f;
    private float xpValue = 10f;
    private float acceleration = 15f;
    private Transform player;
    private bool isAttracted = false;
    private float currentSpeed = 0f;
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }
    private void Update()
    {
        if (player == null) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= magnetRadius)
        {
            isAttracted = true;
        }

        //Accelerated movement
        if (isAttracted)
        {
            currentSpeed += acceleration * Time.deltaTime;
            Vector3 direction = (player.position - transform.position).normalized;
            transform.position += direction * currentSpeed * Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerLevelSystem levelSystem = other.GetComponent<PlayerLevelSystem>();
            if (levelSystem != null)
            {
                levelSystem.GainXP(xpValue);
            }
            Destroy(gameObject);
        }
    }
    public void SetXpValue(float value) => xpValue = value;
    public void SetMagnetRadius(float radius) => magnetRadius = radius;
}