using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCannonController : MonoBehaviour
{
    private Target currentTarget;

    private Vector2 horizontalVector;

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
    }

    private void ChooseTarget()
    {

    }

    public void FirePlatform(InputAction.CallbackContext context)
    {
        if (context.action.triggered)
        {
            
        }
    }

}
