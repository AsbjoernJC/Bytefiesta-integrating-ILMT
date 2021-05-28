using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
public class MysteryDrinkPlayer : MonoBehaviour
{
    [SerializeField]
    private PlayerInput playerInput;
    [SerializeField]
    private Button readyButton;
    [SerializeField]
    private Button pressedReadyButton;

    public static List<Button> buttons { get; private set; }
    
    // Used to reset the buttons;
    private void Start() 
    {
        readyButton.gameObject.SetActive(true);
        pressedReadyButton.gameObject.SetActive(false);
    }

    public void SetPlayerReady()
    {
        readyButton.gameObject.SetActive(false);
        pressedReadyButton.gameObject.SetActive(true);
        MysteryDrinkManager.numberOfReadyPlayers ++;
    }

}
