using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCannonController : MonoBehaviour
{
    private Target currentTarget;

    private Vector2 horizontalVector;

    private bool isShooting = false;

    // Start is called before the first frame update
    void Start()
    {
        // Todo:
        // The player controlling the cannon should start off by having gameObject TargetNPlatform (0), which will be TargetManager.Instance.targets[0]
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HorizontalInput(InputAction.CallbackContext context)
    {
        horizontalVector = context.ReadValue<Vector2>();

        if (horizontalVector.x != 0)
            ChooseTarget(horizontalVector.x);
    }

    private void ChooseTarget(float horizontalValue)
    {
        // whilst isShooting = true players won't be allowed to move their cursor/choose targets

        if (isShooting)
            return;

        // Removes the cursor from what is about to be the former target
        currentTarget.cursorSprite.enabled = false;

        if (horizontalValue > 0)
        {
            // As the target with targetIndex 0 will be the last element in the array and the right target
            // it would cause an indexoutofrangeexception
            if (currentTarget.targetIndex == 22)
                return;
            // go up by one index in TargetManager.instance.targets
        }
        else if (horizontalValue < 0)
        {
            // As the target with targetIndex 0 will be the first element in the array and the leftmost target
            // it would cause an indexoutofrangeexception
            if (currentTarget.targetIndex == 0)
                return;
            // go down by one index in TargetManager.instance.targets
        }
    }

    public void FirePlatform(InputAction.CallbackContext context)
    {
        if (context.action.triggered)
        {
            // as targetplatforms should be active forever after a player has fired a platform previously; we should return
            // if the targetplatform is already active

            // Todo: use some function to shoot a bullet from the Cannon towards the currentTarget.targetCenter.position

            isShooting = true;
        }

        isShooting = false;
    }

}
