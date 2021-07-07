using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCannonController : MonoBehaviour
{
    private Target currentTarget;

    private PlayerControlledCannon playerControlledCannon;

    private Vector2 horizontalVector;

    public bool isShooting = false;

    private bool nonZeroValue = false;

    private Vector2 nonZeroVector;

    private TargetManager assignedTargetManager;

    // Start is called before the first frame update
    void Start()
    {

        // Todo player's should be assigned a team
        // Their playerControlledCannon should be assigned given if they are on the 1st team or the 2nd

        if (this.tag == "Team 1")
        {
            playerControlledCannon = GameObject.Find("Cannon Team 1").GetComponent<PlayerControlledCannon>();
            assignedTargetManager = GameObject.Find("TargetManager 1").GetComponent<TargetManager>();
        }
        else
        {
            playerControlledCannon = GameObject.Find("Cannon Team 2").GetComponent<PlayerControlledCannon>();
            assignedTargetManager = GameObject.Find("TargetManager 2").GetComponent<TargetManager>();
        }

        currentTarget = assignedTargetManager.targets[0];
        currentTarget.cursorSprite.enabled = true;
    }


    public void HorizontalInput(InputAction.CallbackContext context)
    {

        horizontalVector = context.ReadValue<Vector2>();

        // Were essentially only moving when the controlstick is put to a side and then released, reset back to the middle
        if (horizontalVector.x != 0)
        {
            nonZeroValue = true;
            nonZeroVector = horizontalVector;
        }

        if (horizontalVector.x == 0 && nonZeroValue)
        {
            ChooseTarget(horizontalVector.x);
            nonZeroValue = false;
        }

    }


    private void ChooseTarget(float horizontalValue)
    {
        // whilst isShooting = true players won't be allowed to move their cursor/choose targets

        if (isShooting)
            return;


        if (nonZeroVector.x > 0)
        {
            // As the target with targetIndex 0 will be the last element in the array and the right target
            // it would cause an indexoutofrangeexception
            if (currentTarget.targetIndex == 22)
                return;
            
            // Removes the cursor from what is about to be the former target

            currentTarget.cursorSprite.enabled = false;

            currentTarget = assignedTargetManager.targets[currentTarget.targetIndex + 1];
            currentTarget.cursorSprite.enabled = true;
        }
        else if (nonZeroVector.x < 0)
        {
            // As the target with targetIndex 0 will be the first element in the array and the leftmost target
            // it would cause an indexoutofrangeexception
            if (currentTarget.targetIndex == 0)
                return;

            currentTarget.cursorSprite.enabled = false;

            currentTarget = assignedTargetManager.targets[currentTarget.targetIndex - 1];
            currentTarget.cursorSprite.enabled = true;
        }
    }

    public void FirePlatform(InputAction.CallbackContext context)
    {
        if (isShooting)
            return;

        if (context.action.triggered)
        {
            // as targetplatforms should be active forever after a player has fired a platform previously; we should return
            // if the targetplatform is already active

            if (currentTarget.targetPlatform.activeSelf == true)
                return;


            playerControlledCannon.Shoot(currentTarget);

            isShooting = true;
        }

        isShooting = false;
    }

}
