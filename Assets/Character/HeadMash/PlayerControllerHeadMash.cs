using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControllerHeadMash : PlayerController
{


    protected override void Awake() 
    {
        base.Awake();

        // Player ignore eachother's colissions
        // The Head and feet OnTrigger colliders are on a different layer, so they will
        // still be allowed to jump on each other's head no matter what
        Physics2D.IgnoreLayerCollision(6, 6, true);
    }

    public override void OnJump(InputAction.CallbackContext context)
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
                // canDoubleJump = false; Players are allowed to jump as many times as they want in 'HeadMash'
                // Therefore we dont set canDoubleJump to false
                return;
            }
        }
        

    }
}
