using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameWinPlayer : GameInstructionsController
{
    public override void SetPlayerReady()
    {
        readyButton.gameObject.SetActive(false);
        pressedReadyButton.gameObject.SetActive(true);
        MinigameWinManager.Instance.numberOfReadyPlayers ++;
    }
}
