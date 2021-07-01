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
        }
        

    }
}
