using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float runSpeed = 1.5f;
    [SerializeField] private float m_JumpForce = 20.0f;
    [SerializeField] private LayerMask platformLayerMask;
    [SerializeField] private MeshRenderer playerMesh;
    private float horizontalMove = 0f;
    private PlayerConfiguration playerConfig;
    private Vector2 horizontalMoveInput;
    private UnityEngine.Rigidbody2D rB2D;
    private BoxCollider2D bC2D;
    private Vector3 playerPosition;
    private PlayerControls controls;
    private bool m_FacingRight = true;
    private bool canDoubleJump;

    // Todo should use the bool to determine whether or not the player has a PowerUp
    private bool hasPowerUp;

    public GameObject FirePoint; 

    public void InitializePlayer(PlayerConfiguration pc)
    {
        playerConfig = pc;
        playerMesh.material = pc.playerMaterial;
        playerConfig.input.onActionTriggered += Input_onActionTriggered;
    }

    private void Input_onActionTriggered(InputAction.CallbackContext obj)
    {
        if (obj.action.name == controls.Player.Movement.name)
        {
            OnHorizontalMove(obj);
        }
    }

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

    public void OnHorizontalMove(InputAction.CallbackContext context)
    {
        horizontalMoveInput = context.ReadValue<Vector2>();
        if (horizontalMoveInput != new Vector2(0f, 0f))
        {
            RotateFirePoint();
        }
    }

    private void RotateFirePoint()
    {
        FirePoint.transform.position = new Vector2(transform.position.x + horizontalMoveInput.x,
        transform.position.y + horizontalMoveInput.y);
    }

    public void UsePowerUp(InputAction.CallbackContext context)
    {
        if (context.action.triggered)
        {
            Debug.Log("Used PowerUp");
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        Awake();
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

    private bool IsGrounded() 
    {
        RaycastHit2D raycastHit2D = Physics2D.BoxCast(bC2D.bounds.center, bC2D.bounds.size, 0f, Vector2.down, .1f, platformLayerMask);
        return raycastHit2D.collider != null;
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

		//Turns the player 180 degrees
        transform.Rotate(0f, 180f, 0f);
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
            canDoubleJump = true;
        }
        HandleMovement();
    }
}
