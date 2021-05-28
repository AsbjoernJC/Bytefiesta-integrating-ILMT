using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
public class KingoftheHillTracker : MonoBehaviour
{
    [SerializeField]
    private Canvas minigameEndImagery;
    [SerializeField]
    private Image playerWhoWonSprite;
    [SerializeField]
    public Sprite[] playerSprites;
    private static string winner;
    public static Dictionary<string, int> playerScores = new Dictionary<string, int>()
    {
        {"Player 1", 0},
        {"Player 2", 0},
        {"Player 3", 0},
        {"Player 4", 0}
    };

    public static KingoftheHillTracker instance { get; private set; }
    // Start is called before the first frame update
    void Awake()
    {
        if (instance != null)
            Debug.Log("Singleton, tried to create another object");
        else
            instance = this;
    }

// This function get's called when a player has a score equal to or higer than 5. It will stop the players from moving
// And will display an image of the winner for 3.5 seconds.
    public static void MiniGameEnd(string playerWhoWon)
    {
        winner = playerWhoWon;
        // Maybe MysteryDrinkManager should be called from here. 



        // Adds a point to the player's overall score, amount of wins in each minigame that has been played
        DifficultyAndScore.acrossGamemodePlayerScore[winner] ++;
        // Time.timeScale prevents players from moving.
        Time.timeScale = 0f;
        instance.StartCoroutine("DisplayWinner");
        DifficultyAndScore.finishedMinigames ++;
        Time.timeScale = 1f;
        SceneManager.LoadScene("MysteryDrink");
        
        // SceneManager.LoadScene("KingoftheHill1705backgroundedges");

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
        yield return new WaitForSecondsRealtime(3.5f);
        // To do: load some scene that displays how many sips a player should drink
    }

}

