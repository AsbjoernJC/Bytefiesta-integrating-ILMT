using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]


// Todo create a limit to the velocity of the players
// This might have an answer: https://answers.unity.com/questions/9985/limiting-rigidbody-velocity.html 
// or https://answers.unity.com/questions/1458194/proper-way-to-set-a-rigidbodys-maximum-velocity.html

public class Player : MonoBehaviour
{
    public float runSpeed = 1.5f;
    private Rigidbody2D rB2D;
    private BoxCollider2D bC2D;
    private float horizontalMove = 0f;
    private Vector2 horizontalMoveInput; 
    private bool m_FacingRight = true;  // For determining which way the player is currently facing.
    private bool canDoubleJump;
    private Vector3 playerPosition;
    [SerializeField] private float m_JumpForce = 400f;
    [SerializeField] private LayerMask platformLayerMask;


    private void Start() 
    {
        rB2D = GetComponent<Rigidbody2D>();
        bC2D = transform.GetComponent<BoxCollider2D>();
    }

    private void Awake()
    {

    }

    public void OnJump(InputAction.CallbackContext context)
    {

        if (context.action.triggered)
        {
            if (IsGrounded())
            {
                rB2D.velocity = Vector2.up * m_JumpForce;
            }
            else if (canDoubleJump)
            {
                rB2D.velocity = Vector2.up * m_JumpForce;
                canDoubleJump = false;
            }
        }       
    }

    public void OnHorizontalMove(InputAction.CallbackContext context)
    {
        horizontalMoveInput = context.ReadValue<Vector2>();
    }

    private bool IsGrounded() 
    {
        return true;
    }

    private void HandleMovement() 
    {
        // horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        horizontalMove = horizontalMoveInput.x * runSpeed;
        rB2D.velocity = new Vector2(horizontalMove * 10f, rB2D.velocity.y);

        if (horizontalMove > 0 && !m_FacingRight)
        {
            // ... flip the player.
            Flip();
        }
        // Otherwise if the input is moving the player left and the player is facing right...
        else if (horizontalMove < 0 && m_FacingRight)
        {
            // ... flip the player.
            Flip();
        }        
    }

    private void Flip()
    {
        // Switch the way the player is labelled as facing.
		m_FacingRight = !m_FacingRight;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
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

    private void Update()
    {
        if (IsGrounded())
        {
            canDoubleJump = true;
        }
        // transform.position = new Vector3(OutOfBoundsX(), OutOfBoundsY());
        if (OutOfBounds() != new Vector3(0f, 0f))
        {
            transform.position = OutOfBounds();
        }
        HandleMovement();
    }

}
