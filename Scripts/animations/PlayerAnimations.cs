using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAnimations : MonoBehaviour
{
    Animator animator;
    PlayerMovement playerMovement;
    SpriteRenderer spriteRenderer;
    void Start()
    {
        animator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        if (playerMovement.moveInput.x != 0 || playerMovement.moveInput.y != 0)
        {
            animator.SetBool("Move", true);
            SpriteDirectionTracker();
        }
        else
        {
            animator.SetBool("Move", false);
        }
    }

    void SpriteDirectionTracker()
    {
        if (playerMovement.lastHorizontalVector < 0)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }
    }
}
