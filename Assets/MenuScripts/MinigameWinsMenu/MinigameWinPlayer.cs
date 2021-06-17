using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MinigameWinPlayer : GameInstructionsController
{
    public override void SetPlayerReady()
    {
        if (hasPressedReady)
            return;

        readyButton.gameObject.SetActive(false);
        pressedReadyButton.gameObject.SetActive(true);
        MinigameWinManager.Instance.numberOfReadyPlayers ++;
        
        hasPressedReady = true;
    }
}
