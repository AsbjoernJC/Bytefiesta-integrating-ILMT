using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
public class PointMinigameTracker : MonoBehaviour
{
    // Todo change name of KingoftheHillTracker as it may be used for other minigames with same score scheme
    [SerializeField]
    private Canvas minigameEndImagery;
    [SerializeField]
    private Image playerWhoWonSprite;
    [SerializeField]
    public Sprite[] playerSprites;
    private int assumedPosition; 
    private static string winner;
    public static Dictionary<string, int> playerScores = new Dictionary<string, int>()
    {
        {"Player 1", 0},
        {"Player 2", 0},
        {"Player 3", 0},
        {"Player 4", 0}
    };

    public static List<(string, int)> playerStandings = new List<(string, int)>();

    public static PointMinigameTracker instance { get; private set; }
    // Start is called before the first frame update
    void Awake()
    {
        if (instance != null)
            Debug.Log("Singleton, tried to create another object");
        else
            instance = this;

        // As the playerScores are saved to a public static dictionary we need to reset them when the minigame's finished
        // As the players might run into the minigame again.
        // Should reevaluate saving playerScores to a public static dict
        for (int i = 0; i < playerScores.Count; i++)
            playerScores[$"Player {i}"] = 0;

        playerStandings = new List<(string, int)>();
    }

// This function get's called when a player has a score equal to or higer than 5. It will stop the players from moving
// And will display an image of the winner for 3.5 seconds.
    public static void MiniGameEnd(string playerWhoWon)
    {
        winner = playerWhoWon;

        // Adds a point to the player's overall score, amount of wins in each minigame that has been played
        DifficultyAndScore.acrossGamemodePlayerScore[winner] ++;
        // Time.timeScale prevents players from moving.
        Time.timeScale = 0f;
        instance.StartCoroutine("DisplayWinner");


    }

    private void FindPlayerPlacements()
    {
        (string, int) playerPlacement;

        for (int i = 0; i < PlayerConfigurationManager.numberOfActivePlayers; i++)
        {
            playerPlacement = ($"Player {i + 1}", playerScores[$"Player {i + 1}"]);
            if (i == 0)
            {
                playerStandings.Add(playerPlacement);
            }
            else 
            {
                if (playerPlacement.Item2 > playerStandings[playerStandings.Count - 1].Item2)
                {
                    playerStandings.Add(playerPlacement);
                }
                else
                {
                    // playerStandings.Insert(i - 1, playerPlacement);
                    for (int j = 0; j < playerStandings.Count; j++)
                    {
                        if (playerPlacement.Item2 < playerStandings[playerStandings.Count - 1 - j].Item2)
                        {
                            assumedPosition = playerStandings.Count - 1 - j;
                        }
                    }
                    playerStandings.Insert(assumedPosition, playerPlacement);

                }
            }
            Debug.Log(playerPlacement);
        }
    }

    // Displays the winner's character sprite for 3.5 seconds and should then load a new scene.
    private IEnumerator DisplayWinner()
    {
        // Instead of doing this everywhere i should just find pi.playerIndex. Look in PlayerConfigurationManager.
        // winner can either be = "Player 1", "Player 2", "Player 3" or "Player 4". 
        // Therefore splitting by spaces and taking the second index will give us the playerIndex eg. 1, 2, 3 or 4
        int winnerIndex = Int32.Parse(winner.Split( )[1]) - 1;

        // Will display the winner's charactersprite on screen
        playerWhoWonSprite.sprite = playerSprites[winnerIndex];
        minigameEndImagery.gameObject.SetActive(true);

        FindPlayerPlacements();
        yield return new WaitForSecondsRealtime(3.5f);

        DifficultyAndScore.finishedMinigames ++;
        Time.timeScale = 1f;



        // Minigame has finished and therefore we should load MinigameWinsMenu to display
        // The amount of minigame wins a player has and how much they should drink
        SceneManager.LoadScene("MinigameWinsMenu");

    }

}

