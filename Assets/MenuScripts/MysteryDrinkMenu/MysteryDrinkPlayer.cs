
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
