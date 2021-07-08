using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class RaceTrackerJumpTheGun : RaceTracker
{
    [SerializeField] private Image player2WhoWonSprite;
    private List<int> winners = new List<int>();


    public override void MiniGameEnd(List<int> playersWhoWon)
    {
        winners = playersWhoWon;

        // Adds a point to the player's overall score, amount of wins in each minigame that has been played
        for (int i = 0; i < winners.Count; i++)
        {
            var winner = "Player " + (winners[i] + 1).ToString();
            DifficultyAndScore.Instance.acrossGamemodePlayerScore[winner] ++;
        }
        // Time.timeScale prevents players from moving.
        Time.timeScale = 0f;
        StartCoroutine("DisplayWinner");

    }

    protected override IEnumerator DisplayWinner()
    {

        // Will display the winner's charactersprite on screen
        playerWhoWonSprite.sprite = playerSprites[winners[0]];
        player2WhoWonSprite.sprite = playerSprites[winners[1]];
        minigameEndImagery.gameObject.SetActive(true);

        yield return new WaitForSecondsRealtime(3.5f);

        DifficultyAndScore.Instance.finishedMinigames ++;
        Time.timeScale = 1f;



        // Minigame has finished and therefore we should load MinigameWinsMenu to display
        // The amount of minigame wins a player has and how much they should drink
        SceneManager.LoadScene("MinigameWinsMenu");

    }
}
