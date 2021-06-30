using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCannonController : MonoBehaviour
{
    // [SerializeField] float ignoreInputTimer = 0.01f;
    private Target currentTarget;

    private Vector2 horizontalVector;

    private bool isShooting = false;

    private bool gotFirstTarget = false;
    private bool canMoveCursor = true;

    // Start is called before the first frame update
    void Start()
    {
        // Todo:
        // The player controlling the cannon should start off by having gameObject TargetNPlatform (0), which will be TargetManager.Instance.targets[0]
    }

    // Update is called once per frame
    void Update()
    { 
        if (TargetManager.instance.targets != null && !gotFirstTarget)
        {
            currentTarget = TargetManager.instance.targets[0];
            currentTarget.cursorSprite.enabled = true;
            gotFirstTarget = true;
        }
    }

    public void HorizontalInput(InputAction.CallbackContext context)
    {
        if (!canMoveCursor)
            return;
        // StartCoroutine("DisableInputTimer");

        canMoveCursor = false;

        horizontalVector = context.ReadValue<Vector2>();
        if (horizontalVector.x != 0)
            ChooseTarget(horizontalVector.x);

    }


    // private IEnumerator DisableInputTimer()
    // {
    //     yield return new WaitForSeconds(ignoreInputTimer);
    //     canMoveCursor = true;

    // }

    private void ChooseTarget(float horizontalValue)
    {
        // whilst isShooting = true players won't be allowed to move their cursor/choose targets

        if (isShooting)
            return;

        // Removes the cursor from what is about to be the former target

        if (horizontalValue > 0)
        {
            // As the target with targetIndex 0 will be the last element in the array and the right target
            // it would cause an indexoutofrangeexception
            if (currentTarget.targetIndex == 22)
                return;
            
            currentTarget.cursorSprite.enabled = false;

            currentTarget = TargetManager.instance.targets[currentTarget.targetIndex + 1];
            currentTarget.cursorSprite.enabled = true;
        }
        else if (horizontalValue < 0)
        {
            // As the target with targetIndex 0 will be the first element in the array and the leftmost target
            // it would cause an indexoutofrangeexception
            if (currentTarget.targetIndex == 0)
                return;

            currentTarget.cursorSprite.enabled = false;

            currentTarget = TargetManager.instance.targets[currentTarget.targetIndex - 1];
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

            // Todo: use some function to shoot a bullet from the Cannon towards the currentTarget.targetCenter.position
            Debug.Log("Shot");
            isShooting = true;
        }

        isShooting = false;
    }

}
