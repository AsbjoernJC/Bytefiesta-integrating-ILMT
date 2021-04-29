using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController2 : MonoBehaviour
{
    [SerializeField] private float playerSpeed = 50.0f;
    [SerializeField] private float jumpHeight = 300.0f;
    [SerializeField] private LayerMask platformLayerMask;
    private Rigidbody2D rB2D;
    private BoxCollider2D bC2D;
    private Vector3 playerPosition;
    
    private Vector2 movementInput = Vector2.zero;
    private bool jumped = false;

    // private void Start()
    // {
    //     m_Rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
    //     boxCollider2D = transform.GetComponent<BoxCollider2D>();
    // }

    private void Awake() 
    {
        if (rB2D == null)
        {
            rB2D = gameObject.GetComponent<Rigidbody2D>();
        }
        if (bC2D == null)
        {
            bC2D = transform.GetComponent<BoxCollider2D>();
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        jumped = context.action.triggered;
    }

        private bool IsGrounded() 
    {
        RaycastHit2D raycastHit2D = Physics2D.BoxCast(bC2D.bounds.center, bC2D.bounds.size, 0f, Vector2.down, .1f, platformLayerMask);
        return raycastHit2D.collider != null;
    }

        private Vector3 OutOfBounds()
    {
        if(transform.position.x >= 17.36)
        {
            playerPosition = new Vector3(-transform.position.x + 0.1f, transform.position.y);
            return playerPosition;
        }
        else if (transform.position.x <= -17.36)
        {
            playerPosition = new Vector3(-transform.position.x - 0.1f, transform.position.y);
            return playerPosition;
        }
        else if (transform.position.y >= 13.4)
        {
            playerPosition = new Vector3(transform.position.x, -11.3099f);
            return playerPosition;
        }
        else if (transform.position.y <= -11.3199)
        {
            playerPosition = new Vector3(transform.position.x, 13.39f);
            return playerPosition;
        }
        playerPosition = new Vector3(0f, 0f);
        return playerPosition;
    }

    void Update()
    {
        if (OutOfBounds() != new Vector3(0f, 0f))
        {
            transform.position = OutOfBounds();
        }

        if (IsGrounded())
        {
            rB2D.velocity = Vector2.zero;
        }

        Vector2 move = new Vector2(movementInput.x, movementInput.y);
        if (move != Vector2.zero)
        {
            rB2D.velocity = new Vector2(movementInput.x * playerSpeed * 10f, rB2D.velocity.y);
        }

        // Changes the height position of the player..
        if (jumped && IsGrounded())
        {
            rB2D.velocity = Vector2.up * jumpHeight;
        }
    }
}
