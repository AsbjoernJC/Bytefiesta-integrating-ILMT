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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HorizontalInput(InputAction.CallbackContext context)
    {
        horizontalVector = context.ReadValue<Vector2>();
        ChooseTarget(horizontalVector.x);
    }

    private void ChooseTarget(float horizontalValue)
    {
        currentTarget;
        if (horizontalValue > 0)
        {
            // go up by one index in TargetManager.instance.targets
        }
        else if (horizontalValue < 0)
        {
            // go down by one index in TargetManager.instance.targets
        }
    }

    public void FirePlatform(InputAction.CallbackContext context)
    {
        if (context.action.triggered)
        {

            isShooting = true;
        }
    }

}
