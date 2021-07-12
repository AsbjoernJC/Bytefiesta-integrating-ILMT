using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrownInitializer : MonoBehaviour
{
    [SerializeField]
    GameObject[] playerCrownCanvas;
    [SerializeField]
    Sprite[] crownsWithColor;
    public List<Image> player1CrownSprites;
    public List<Image> player2CrownSprites;
    public List<Image> player3CrownSprites;
    public List<Image> player4CrownSprites;

    
    // Start is called before the first frame update
    void Start()
    {
        ActivateCrownCanvas();
    }


    // Activates a number of canvases according to the number of players in the game
    private void ActivateCrownCanvas()
    {

        for (int i = 0; i < PlayerConfigurationManager.Instance.numberOfActivePlayers; i++)
        {
            playerCrownCanvas[i].SetActive(true);
            CalculateCrowns(i);
        }
    }


    // Calculates the amount of sips a player should drink based on their performance in the last minigame.
    private void CalculateCrowns(int playerIndex)
    {
        int wonCrowns = DifficultyAndScore.Instance.acrossGamemodePlayerScore[$"Player {playerIndex + 1}"];
        // Todo calculate the amount of sips a player should drink
        InitializeCrownSprites(playerIndex, wonCrowns);
    }


    // Displays an amount of crown sprites with color over the player's according to the amount of minigame wins they have
    private void InitializeCrownSprites(int playerIndex, int wonCrowns)
    {
        switch (playerIndex)
        {
            case 0:
                for (int i = 0; i < wonCrowns; i++)
                {
                    player1CrownSprites[i].sprite = crownsWithColor[i];
                }
                break;
            case 1:
                for (int i = 0; i < wonCrowns; i++)
                {
                    player2CrownSprites[i].sprite = crownsWithColor[i];
                }
                break;
            case 2:
                for (int i = 0; i < wonCrowns; i++)
                {
                    player3CrownSprites[i].sprite = crownsWithColor[i];
                }
                break;
            case 3:
                for (int i = 0; i < wonCrowns; i++)
                {
                    player4CrownSprites[i].sprite = crownsWithColor[i];
                }
                break;
        }
        if (wonCrowns == 5)
        {
            DifficultyAndScore.Instance.gameWinner = $"Player {playerIndex + 1}";
            DifficultyAndScore.Instance.gameWinners.Add($"Player {playerIndex + 1}");
        }

        // If it's the last playerindex the winners will have been found and optionally have been added to gamewinners

        // playerIndexes are 0, 1, 2 and 3 therefore numberOfActivePlayers should be decremented by one when comparing
        // as when there are two players they would respectively have index 0 and 1, howver numberOfActivePlayers would equal to 2
        if (playerIndex == PlayerConfigurationManager.Instance.numberOfActivePlayers - 1)
        {
            
            switch(DifficultyAndScore.Instance.gameWinners.Count)
            {
                case 1:
                    MinigameWinManager.Instance.StartCoroutine("DisplayWinner");
                    break;

                case 2:
                    MinigameWinManager.Instance.StartCoroutine("DisplayWinners");
                    break;
            }


        }

    }

}
