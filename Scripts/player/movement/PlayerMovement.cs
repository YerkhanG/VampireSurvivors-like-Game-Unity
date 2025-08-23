using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private NewActions inputActions;
    
    [HideInInspector]
    public Vector2 moveInput;
    private Rigidbody2D rb;
    public float speed = 5f;

    //for direction sprite changes 
    [HideInInspector]
    public float lastHorizontalVector;
    [HideInInspector]
    public float lastVerticalVector;

    void Awake()
    {
        inputActions = new NewActions();
    }

    void OnEnable()
    {
        inputActions.Player.Move.Enable();
    }

    void OnDisable()
    {
        inputActions.Player.Move.Disable();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        moveInput = inputActions.Player.Move.ReadValue<Vector2>();
        if (moveInput.x != 0)
        {
            lastHorizontalVector = moveInput.x;
        }
        if(moveInput.y != 0)
        {
            lastVerticalVector = moveInput.y;
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = moveInput * speed;
    }
}
