using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
public class ScoreSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] playerUI;

    // playerScores[0].text = "1" would work.

    public void SpawnPlayerScoreUI(int playerIndex)
    {
        Vector3 position;

        // position is based on transform.parent whose position is 487,5 x, 285 y. 
        // position below will translate to the playerPicture position being (-200, -267, 0)
        position = new Vector3(287.5f, 18, 0);
        int playerMultiplier = playerIndex*100;

        if (playerIndex > 0)
            position = new Vector3(287.5f + playerMultiplier, 18, 0);    

        var playerPicture = Instantiate(playerUI[playerIndex]);
        playerPicture.transform.SetParent(gameObject.transform);

        playerPicture.transform.position = position;
        
        //Gets the Textmeshpro.text element from ther playerPicture component 
        var playerScoreText = playerPicture.GetComponentInChildren<TMP_Text>();
        ScoreUpdater.playerScoreTexts.Add(playerScoreText);

    }

}

public class ScoreUpdater : MonoBehaviour
{
    public static ScoreUpdater Instance { get; private set; }
    public static List<TMP_Text> playerScoreTexts = new List<TMP_Text>();

    private void Awake() 
    {
        Instance = this;        
    }
    public static void UpdatePlayerScoreUI(string player)
    {
        int playerIndex = Int16.Parse(player.Split( )[1]);
        // playerIndex - 1 as player can be Player 1, Player 2, Player 3 or Player 4
        playerScoreTexts[playerIndex - 1].text = KingoftheHillTracker.playerScores[player].ToString();
    }
}
