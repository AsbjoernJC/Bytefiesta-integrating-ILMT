using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
public class GameInstructionsController : MonoBehaviour
{
    [SerializeField]
    protected PlayerInput playerInput;
    [SerializeField]
    protected Button readyButton;
    [SerializeField]
    protected Button pressedReadyButton;

    public static List<Button> buttons { get; private set; }
    
    // Used to reset the buttons;
    protected void Start() 
    {
        transform.localScale = new Vector3(1f, 1f, 0f);
        readyButton.gameObject.SetActive(true);
        pressedReadyButton.gameObject.SetActive(false);
    }

// Bugs where SetPlayerReady was not registering could have been due
// to having "Deselect on background click" enabled:
// https://forum.unity.com/threads/feature-request-option-to-disable-deselect-in-ui-input-module.761531/
// Will see if that was the cause of rare and random loss of controls
    public virtual void SetPlayerReady()
    {
        readyButton.gameObject.SetActive(false);
        pressedReadyButton.gameObject.SetActive(true);
        GameInstructionsManager.numberOfReadyPlayers ++;
    }

}
