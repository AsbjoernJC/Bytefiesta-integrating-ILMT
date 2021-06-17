

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
