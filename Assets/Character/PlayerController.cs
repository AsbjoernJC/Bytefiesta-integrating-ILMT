using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]

public class PlayerController : MonoBehaviour
{
    // Todo
    // Figure out if the PlayerController Script should be added to in-minigame player objects where all
    // of this class applies
    // or should i create a playerprefab for each minigame with playercontroller variance.
    // These scripts would inherit from PlayerController 
    [SerializeField] private float runSpeed = 1.5f;
    [SerializeField] protected float m_JumpForce = 20.0f;
    [SerializeField] protected LayerMask platformLayerMask;
    private float horizontalMove = 0f;
    private float terminalVelocity = 25.1f;
    private Vector2 horizontalMoveInput;
    protected Rigidbody2D rB2D;
    protected BoxCollider2D bC2D;
    protected CapsuleCollider2D cC2D;
    protected Vector3 playerPosition;
    private bool m_FacingRight = true;
    protected bool canDoubleJump;
    protected bool hasShieldPowerUp = false;
    protected bool hasNormalBullet = true;
    protected bool canCoyote = false;
    private bool coyoteStarted = false;
    protected float reloadSpeed = 0.4f;

    protected Quaternion shootingAngle;
    protected Quaternion normalBulletAngle; 

    protected int bulletCounter = 0;
    public Sprite shieldSprite;
    public SpriteRenderer shieldPoint;
    public Transform firePoint; 
    public GameObject[] powerUp;
    public Animator animator;


    protected virtual void Awake() 
    {
        // awake only runs once ie. on initialization.
        rB2D = gameObject.GetComponent<Rigidbody2D>();
        bC2D = transform.GetComponent<BoxCollider2D>();
        cC2D = transform.GetComponent<CapsuleCollider2D>();
        // Player allows player to collide with one another by default
        Physics2D.IgnoreLayerCollision (6, 6, false);
    }

    protected virtual void Update()
    {
        if (IsGrounded())
        {
            animator.SetBool("IsJumping", false);
            canDoubleJump = true;
            coyoteStarted = false;
        }
        else if (!IsGrounded() && !coyoteStarted)
        {
            StartCoroutine(CoyoteTimer(0.1f));
        }
        
        HandleMovement();
    }

    public void OnHorizontalMove(InputAction.CallbackContext context)
    {
        horizontalMoveInput = context.ReadValue<Vector2>();

        // If horizontalMoveInput.x is > 0 then the character is moving to the right and should
        // m_FacingRight should therefore be true
        if (horizontalMoveInput.x > 0 && !m_FacingRight)
        {
            // ... flip the player.
            Flip();
        }
        // Otherwise if the horizontalMoveInput is < 0 then the player is moving left
        else if (horizontalMoveInput.x < 0 && m_FacingRight)
        {
            // ... flip the player.
            Flip();
        }        

        if (horizontalMoveInput != new Vector2(0f, 0f))
        {
            RotateFirePoint();
        }
    }

// now rotates bullets by setting the shootingangle to the correct angle.
    private void RotateFirePoint()
    {
        firePoint.position = new Vector2(transform.position.x + horizontalMoveInput.x,
        transform.position.y + horizontalMoveInput.y);

        // For some reason i can't get it working with Mathf.Atan2. As i get the z rotation wrong. Stored in shootingAngle.
        // Todo get it working with the Mathf.Atan2 method. For testing purposes i will do this the tedious way.

        // float rotZ = Mathf.Atan2(firePoint.transform.position.y, firePoint.transform.position.x) * Mathf.Rad2Deg;
        float rotZ = 0f;
        // Shooting horizontally to the right side
        if (horizontalMoveInput.normalized.x == 1)
        {
            rotZ = -90f;
            shootingAngle.eulerAngles = new Vector3(0f, 0f, -90f);
            normalBulletAngle.eulerAngles = new Vector3(0f, 0f, 0f);
        }
        // Shooting horizontally to the left side
        else if (horizontalMoveInput.normalized.x == -1)
        {
            rotZ = 90;
            shootingAngle.eulerAngles = new Vector3(0f, 0f, 90f);
            normalBulletAngle.eulerAngles = new Vector3(0f, 0f, 180f);
        }

        if(m_FacingRight)
        {
            // Shooting diagonally up to the right side
            if (firePoint.localPosition.normalized.x > 0 && firePoint.localPosition.normalized.y > 0)
            {
                rotZ = 315f;
                shootingAngle.eulerAngles = new Vector3(0f, 0f, 315f);
                normalBulletAngle.eulerAngles = new Vector3(0f, 0f, 45f);
            }
            // Shooting diagonally down to the right side
            else if (firePoint.localPosition.normalized.x > 0 && firePoint.localPosition.normalized.y < 0)
            {
                rotZ = 225f;
                shootingAngle.eulerAngles = new Vector3(0f, 0f, 225f);
                normalBulletAngle.eulerAngles = new Vector3(0f, 0f, 315f);
            }    
        }
        else 
        {
            // Shooting diagonally down to the left side
            if (firePoint.localPosition.normalized.x > 0 && firePoint.localPosition.normalized.y < 0)
            {
                rotZ = 135f;
                shootingAngle.eulerAngles = new Vector3(0f, 0f, 135f);
                normalBulletAngle.eulerAngles = new Vector3(0f, 0f, 225f);
            }
            // Shooting diagonally up to the left side
            else if (firePoint.localPosition.normalized.x > 0 && firePoint.localPosition.normalized.y > 0)
            {
                rotZ = 45f;
                shootingAngle.eulerAngles = new Vector3(0f, 0f, 45f);
                normalBulletAngle.eulerAngles = new Vector3(0f, 0f, 135f);
            }
        }

        if (firePoint.localPosition.normalized.y == 1)
            {
                rotZ = 360f;
                shootingAngle.eulerAngles = new Vector3(0f, 0f, 360f);
                normalBulletAngle.eulerAngles = new Vector3(0f, 0f, 90f);
            }
        else if (firePoint.localPosition.normalized.y == -1)
            {
                rotZ = 180f;
                shootingAngle.eulerAngles = new Vector3(0f, 0f, 180f);
                normalBulletAngle.eulerAngles = new Vector3(0f, 0f, 270f);
            }
        firePoint.transform.rotation = Quaternion.Euler(0, 0, rotZ);
    }


// Weird bug with the animator. When holding down the jumpbutton the Player_Jump animation will not be played but rather the Player_Idle or Player_Run
    public virtual void OnJump(InputAction.CallbackContext context)
    {
        animator.SetBool("IsJumping", true);
        if (context.action.triggered)
        {
            if (canCoyote)
            {
                rB2D.velocity = Vector2.up * m_JumpForce;
                canCoyote = false;
                return;
            }

            if (IsGrounded())
            {
                rB2D.velocity = Vector2.up * m_JumpForce;
                return;
            }
            else if (canDoubleJump)
            {
                rB2D.velocity = Vector2.up * m_JumpForce;
                canDoubleJump = false;
                return;
            }
        }
        

    }

// GotBulletPowerUp() and GotShieldPowerUp() are still in PlayerControlle
// to avoid having to find the minigames specific
// playercontroller dynamically
// This is the reference in PowerUp0
// player.GetComponent<PlayerController>().GotBulletPowerUp();
    public virtual void GotBulletPowerUp()
    {

    }

    public virtual void GotShieldPowerUp()
    {

    }

    protected virtual bool IsGrounded() 
    {
        RaycastHit2D raycastHit2D = Physics2D.BoxCast(bC2D.bounds.center, bC2D.bounds.size, 0f, Vector2.down, .1f, platformLayerMask);
        return raycastHit2D.collider != null;
    }

// Allows players to have both of their jumps for a certain amount of time when IsGrounded() == false;
    private IEnumerator CoyoteTimer(float bufferTime)
    {
        coyoteStarted = true;
        canCoyote = true;
        yield return new WaitForSeconds(bufferTime);
        canCoyote = false;
    }


// Handles vertical and horizontal input from a controller/keyboard and makes the character move on screen
    private void HandleMovement()
    {

        //Players should maybe not be slowed down when holding their control stick diagonally: 0.67 vs 1 (horizontal)
        horizontalMove = horizontalMoveInput.x * runSpeed;

        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

        // terminalVelocity, so y.velocity won't keep increasing after a certain speed when player goes out of bounds in y-direction
        // Help from: https://answers.unity.com/questions/9985/limiting-rigidbody-velocity.html
        var v = rB2D.velocity;
        if(v.sqrMagnitude > terminalVelocity*terminalVelocity)
        {
            rB2D.velocity = v.normalized*terminalVelocity;
        }
        else
        {
            rB2D.velocity = new Vector2(horizontalMove * 10f, rB2D.velocity.y);
        }

    }


    private void Flip()
    {
        // Switch the way the player is labelled as facing.
	    m_FacingRight = !m_FacingRight;
        // Turns the player 180 degrees when turning horizontally
  	    transform.Rotate(0f, 180f, 0);
    }


}

