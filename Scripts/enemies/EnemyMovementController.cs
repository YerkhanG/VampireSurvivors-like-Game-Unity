using System.Runtime.CompilerServices;
using UnityEngine;

public class EnemyMovementController : MonoBehaviour
{

    private float moveSpeed = 3f;
    private Rigidbody2D rb;
    private GameObject player;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void Update()
    {
        MoveTowardsPLayer();
    }
    public void SetMoveSpeed(float speed)
    {
        moveSpeed = speed;
    }

    private void MoveTowardsPLayer()
    {
        Vector2 direction = (player.transform.position - transform.position).normalized;
        rb.linearVelocity = direction * moveSpeed;
    }
}