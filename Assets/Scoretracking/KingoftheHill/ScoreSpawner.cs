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


        float widthMultiplier = CameraScript.currentWidth / CameraScript.targetWidth;
        float heightMultiplier = CameraScript.currentHeight / CameraScript.targetHeight;
        // position is based on transform.parent whose position is 960 x, 540 y, when the resolution is 1920x1080. 
        // position below will translate to the playerPicture position being (-160, -513, 0)
        position = new Vector3(780f * widthMultiplier, 27f * heightMultiplier, 0f);
        int playerMultiplier = playerIndex*100;

        if (playerIndex > 0)
            position = new Vector3(position.x + playerMultiplier, position.y, position.z);    

        var playerPicture = Instantiate(playerUI[playerIndex]);
        playerPicture.transform.SetParent(gameObject.transform);
        playerPicture.transform.position = position;
        playerPicture.transform.localScale *= widthMultiplier;

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
