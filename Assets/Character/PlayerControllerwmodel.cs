using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]

public class PlayerControllerwmodel : MonoBehaviour
{
    [SerializeField] private float runSpeed = 1.5f;
    [SerializeField] private float m_JumpForce = 20.0f;
    [SerializeField] private LayerMask platformLayerMask;
    private float horizontalMove = 0f;
    private PlayerConfiguration playerConfig;
    private Vector2 horizontalMoveInput;
    private Rigidbody2D rB2D;
    private BoxCollider2D bC2D;
    private CapsuleCollider2D cC2D;
    private Vector3 playerPosition;
    private PlayerControls controls;
    private bool m_FacingRight = true;
    private bool canDoubleJump;

    private Quaternion shootingAngle;

    // Todo should use the bool to determine whether or not the player has a PowerUp
    private int bulletCounter = 0;
    public Transform firePoint; 
    public GameObject[] powerUp;
    public Animator animator;


    public void InitializePlayer(PlayerConfiguration pc)
    {
        playerConfig = pc;
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
        if (cC2D == null)
        {
            cC2D = transform.GetComponent<CapsuleCollider2D>();
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
        firePoint.position = new Vector2(transform.position.x + horizontalMoveInput.x,
        transform.position.y + horizontalMoveInput.y);

        // For some reason i can't get it working with Mathf.Atan2. As i get the z rotation wrong. Stored in shootingAngle.
        // Todo get it working with the Mathf.Atan2 method. For testing purposes i will do this the tedious way.

        // float rotZ = Mathf.Atan2(firePoint.transform.position.y, firePoint.transform.position.x) * Mathf.Rad2Deg;
        float rotZ = 0f;
        if (firePoint.localPosition.normalized.x == 1 && m_FacingRight)
        {
            rotZ = -90f;
        }
        else if (firePoint.localPosition.normalized.x == 1 && !m_FacingRight)
        {
            rotZ = 90;
        }

        if(m_FacingRight)
        {
            if (firePoint.localPosition.normalized.x > 0 && firePoint.localPosition.normalized.y > 0)
            {
                rotZ = 315f;
            }
            else if (firePoint.localPosition.normalized.x > 0 && firePoint.localPosition.normalized.y < 0)
            {
                rotZ = 225f;
            }    
        }
        else 
        {
            if (firePoint.localPosition.normalized.x > 0 && firePoint.localPosition.normalized.y < 0)
            {
                rotZ = 135f;
            }
            else if (firePoint.localPosition.normalized.x > 0 && firePoint.localPosition.normalized.y > 0)
            {
                rotZ = 45f;
            }
        }

        if (firePoint.localPosition.normalized.y == 1)
            rotZ = 360f;
        else if (firePoint.localPosition.normalized.y == -1)
            rotZ = 180f;
        firePoint.transform.rotation = Quaternion.Euler(0, 0, rotZ);
    }

    public void UseBulletPowerUp(InputAction.CallbackContext context)
    {
        if (context.action.triggered && bulletCounter > 0)
        {
            var playerName = this.name;
            Bullet.Shoot(firePoint, powerUp[0], shootingAngle, playerName);
            bulletCounter --;
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        animator.SetBool("IsJumping", true);
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

    public void GotBulletPowerUp(string powerUp)
    {
        bulletCounter = 3;
    }

    private bool IsGrounded() 
    {
        RaycastHit2D raycastHit2D = Physics2D.CapsuleCast(cC2D.bounds.center, cC2D.bounds.size, cC2D.direction, 0f, Vector2.down, 0.1f, platformLayerMask);
        return raycastHit2D.collider != null;
    }

    private void HandleMovement()
    {
        // horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        horizontalMove = horizontalMoveInput.x * runSpeed;

        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

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
  	    transform.Rotate(0f, 180f, 0);
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
            animator.SetBool("IsJumping", false);
            canDoubleJump = true;
        }
        HandleMovement();
    }
}

