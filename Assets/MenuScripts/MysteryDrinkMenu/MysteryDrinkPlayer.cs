using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
public class MysteryDrinkPlayer : GameInstructionsController
{
    public override void SetPlayerReady()
    {
        if (hasPressedReady)
            return;

        readyButton.gameObject.SetActive(false);
        pressedReadyButton.gameObject.SetActive(true);
        MysteryDrinkManager.numberOfReadyPlayers ++;
        
        hasPressedReady = true;
    }

}
