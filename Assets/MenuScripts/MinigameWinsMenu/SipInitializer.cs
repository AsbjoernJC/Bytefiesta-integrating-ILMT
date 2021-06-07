using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SipInitializer : MonoBehaviour
{
    [SerializeField]
    GameObject[] playerBeerCanvas;

    public List<Image> player1BeerSprites;
    public List<Image> player2BeerSprites;
    public List<Image> player3BeerSprites;
    public List<Image> player4BeerSprites;
    // Start is called before the first frame update
    void Start()
    {
        ActivateBeerCanvas();
    }


    // Activates a number of canvases according to the number of players in the game
    private void ActivateBeerCanvas()
    {
        for (int i = 0; i < PlayerConfigurationManager.numberOfActivePlayers; i++)
        {
            int minigamePlacement = LastManStanding.playerStandings[$"Player {i + 1}"];
            Debug.Log($"Player {i + 1} came in as number : " + minigamePlacement);
            playerBeerCanvas[i].SetActive(true);

            // Todo should have knowledge of a player's placement in the prior minigame
            CalculateSips(i, 2);

        }
    }


    // Calculates the amount of sips a player should drink based on their performance in the last minigame.
    private void CalculateSips(int playerIndex, int minigamePlacement)
    {
        // Todo Calculate the amount of sips a player should drink based on their performance in the last minigame.
        string chosenDifficulty = DifficultyAndScore.difficulty;

        

        int player1Sips = 5;
        InitializeBeerSprites(playerIndex, player1Sips);
    }


    // Displays an amount of beer sprites over the player's according to the amount of sips they should drink
    private void InitializeBeerSprites(int playerIndex, int sips)
    {
        switch (playerIndex)
        {
            case 0:
                for (int i = 0; i < sips; i++)
                {
                    player1BeerSprites[i].enabled = true;
                }
                break;
            case 1:
                for (int i = 0; i < sips; i++)
                {
                    player2BeerSprites[i].enabled = true;
                }
                break;
            case 2:
                for (int i = 0; i < sips; i++)
                {
                    player3BeerSprites[i].enabled = true;
                }
                break;
            case 3:
                for (int i = 0; i < sips; i++)
                {
                    player4BeerSprites[i].enabled = true;
                }
                break;
        }
    }

}
