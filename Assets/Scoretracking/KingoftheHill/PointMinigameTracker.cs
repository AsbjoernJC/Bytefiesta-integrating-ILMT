using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
public class PointMinigameTracker : MonoBehaviour
{
    [SerializeField]
    private Canvas minigameEndImagery;
    [SerializeField]
    private Image playerWhoWonSprite;
    [SerializeField]
    public Sprite[] playerSprites;
    private int assumedPosition; 
    private int standardizedAssumedPosition;
    private int placement;
    private string winner;
    public Dictionary<string, int> playerScores = new Dictionary<string, int>()
    {
        {"Player 1", 0},
        {"Player 2", 0},
        {"Player 3", 0},
        {"Player 4", 0}
    };

    public List<(string, int)> playerPointStandings = new List<(string, int)>();
    public List<(string, int)> standardizedPlayerStandings = new List<(string, int)>();
    public static PointMinigameTracker instance { get; private set; }
    
    
    // Start is called before the first frame update
    void Awake()
    {
        if (instance != null)
            Debug.Log("Singleton, tried to create another object");
        else
            instance = this;

    }


// This function get's called when a player has a score equal to or higer than 5. It will stop the players from moving
    public void MiniGameEnd(string playerWhoWon)
    {
        winner = playerWhoWon;

        // Adds a point to the player's overall score, amount of wins in each minigame that has been played
        DifficultyAndScore.Instance.acrossGamemodePlayerScore[winner] ++;
        // Time.timeScale prevents players from moving.
        Time.timeScale = 0f;
        StartCoroutine("DisplayWinner");


    }


    private void SortPlayerPlacements()
    {
        (string, int) playerPlacement;

        for (int i = 0; i < PlayerConfigurationManager.Instance.numberOfActivePlayers; i++)
        {
            playerPlacement = ($"Player {i + 1}", playerScores[$"Player {i + 1}"]);
            if (i == 0)
            {
                playerPointStandings.Add(playerPlacement);
            }
            else 
            {
                if (playerPlacement.Item2 > playerPointStandings[playerPointStandings.Count - 1].Item2)
                {
                    playerPointStandings.Add(playerPlacement);
                }
                else
                {
                    // playerStandings.Insert(i - 1, playerPlacement);
                    for (int j = 0; j < playerPointStandings.Count; j++)
                    {
                        if (playerPlacement.Item2 < playerPointStandings[playerPointStandings.Count - 1 - j].Item2)
                        {
                            assumedPosition = playerPointStandings.Count - 1 - j;
                        }
                    }
                    playerPointStandings.Insert(assumedPosition, playerPlacement);

                }
            }

        }

        // Finding standardized player standings meaning players can only place: 1st, 2nd, 3rd and 4th
        for (int i = 0; i < playerPointStandings.Count; i++)
        {
            // Need to check if the PlayerPointstandings[i - 1] in playerPointStandings is equal to playerPointStandings[i]
            // This is for when players are tied in points.
            // Player 1 has a score of 5 (won), player 2 has a score of 3
            // Player 3 has a score of 3 and player 4 has a score of 3.
            // This would in my eyes lead player 2, 3 and 4 to all have a tied second place
            // , however, there will be some exceptions
            for (int j = 0; j < playerPointStandings.Count; j++)
            {
                if (playerPointStandings[i].Item2 == playerPointStandings[j].Item2)
                {
                    standardizedAssumedPosition = playerPointStandings.Count - j;
    
    // If a player scores zero points then they will be placed as if they came in last
    // Coming in last is interpreted by SipInitializer as coming in, in 4th place 
                    if (playerPointStandings[i].Item2 == 0 )
                    {
                        standardizedAssumedPosition = 4;
                    }
                }
            }
            
            // If players tie with 0 points they should still get placed as if they came in last (standardizedAssumedPosition should be
            // equal to 4)
            // If players tie with 1 point they should get placed as if they came in second last (3rd)
            // Imagine a 3-way tie with 1 point in a 4 person game. 1 point is only deserving of 0 points

            standardizedPlayerStandings.Add((playerPointStandings[i].Item1, standardizedAssumedPosition));

        }
    }

    public int ReturnPlayerPlacement(string playerName)
    {
        // Todo:
        // Have to keep in mind: if there is only 2 players the player who loses should be registered as the last place
        // 4th that is
        for (int i = 0; i < standardizedPlayerStandings.Count; i++)
        {
            if (standardizedPlayerStandings[i].Item1 == playerName)
            {
                placement = standardizedPlayerStandings[i].Item2;
            }
        }

        return placement;
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

        SortPlayerPlacements();
        yield return new WaitForSecondsRealtime(3.5f);

        DifficultyAndScore.Instance.finishedMinigames ++;
        Time.timeScale = 1f;



        // Minigame has finished and therefore we should load MinigameWinsMenu to display
        // The amount of minigame wins a player has and how much they should drink
        SceneManager.LoadScene("MinigameWinsMenu");

    }

}

