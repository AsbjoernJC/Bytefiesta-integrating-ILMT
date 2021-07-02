using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControllerJumpTheGun : PlayerController
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public override void OnJump(InputAction.CallbackContext context)
    {

        // Players are only allowed to jump a single time in jumpthegun
        animator.SetBool("IsJumping", true);
        if (context.action.triggered)
        {
            if (IsGrounded())
            {
                rB2D.velocity = Vector2.up * m_JumpForce;
                return;
            }
        }
        

    }


// As the playerprefabs for JumpTheGun has their foot (a box collider disabled) we have changed from using the boxcollider2d
// to raycast and instead use the circlecollider
    protected override bool IsGrounded() 
    {
        RaycastHit2D raycastHit2D = Physics2D.BoxCast(bC2D.bounds.center, bC2D.bounds.size, 0f, Vector2.down, .1f, platformLayerMask);
        return raycastHit2D.collider != null;
    }

}
