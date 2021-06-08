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

    private int minigamePlacement;

    // [LastManStanding][Hardcore][int placement]
    // [Hardcore][int placement]

    // Might give minigames some innate placement system eg. where a lastmanstanding type minigame will place all players
    // who die before 10 seconds have passed as a 4th place
    private Dictionary<string, Dictionary<int, int>> sipAmount = new Dictionary<string, Dictionary<int, int>>()
    {
        // String will be called with the difficulty chosen at start
        // first int is the player's placement in the minigame
        // second int is the amount of sips a player should drink
        // and therefore how many beer sprites to instantiate
        {"Hardcore", new Dictionary<int, int>{
            {1, 0},
            {2, 3},
            {3, 4},
            {4, 5}
        }
        },

        {"Medium", new Dictionary<int, int>{
            {1, 0},
            {2, 1},
            {3, 2},
            {4, 3}
        }
        },

        {"Light" , new Dictionary<int, int>{
            {1, 0},
            {2, 1},
            {3, 1},
            {4, 2}
        }
        }

    };

    // Todo Calculate the amount of sips a player should drink based on their performance in the last minigame.
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
            string minigamePlayed = LevelInitializer.sceneName;
            Debug.Log($"Last minigame was: {minigamePlayed}");
        
        switch (minigamePlayed)
        {
            case "KingoftheHill":
                // Todo add a placement system to PointMinigameTracker
                break;
            case "GunnedDown":
                minigamePlacement = LastManStanding.playerStandings[$"Player {i + 1}"];
                break;
        }

            Debug.Log($"Player {i + 1} came in as number : " + minigamePlacement);
            playerBeerCanvas[i].SetActive(true);

            CalculateSips(i, minigamePlacement);
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
