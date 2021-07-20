using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
public class ScoreSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] playerUI;


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
        ScoreUpdater.Instance.playerScoreTexts.Add(playerScoreText);

    }

}

public class ScoreUpdater : MonoBehaviour
{
    public static ScoreUpdater Instance { get; private set; }
    public List<TMP_Text> playerScoreTexts = new List<TMP_Text>();

    private void Awake() 
    {
        Instance = this;        
    }


    public void UpdatePlayerScoreUI(string player)
    {
        // Removes elements in the list which are null
        // There will be null elements when the scene has ended, as we load a new scene old gameObjects will be null
        //  and we are not removing elements from playerScoreTexts.

        // As playerScoreTexts has been changed to a non static list, we should not need the code below
        // for (int i = 0; i < playerScoreTexts.Count; i++)
        // {
        //     playerScoreTexts.RemoveAll(element => element == null);
        // }


        int playerIndex = Int16.Parse(player.Split( )[1]);
        // playerIndex - 1 as playerIndex can be 1, 2, 3 or 4
        playerScoreTexts[playerIndex - 1].text = PointMinigameTracker.instance.playerScores[player].ToString();
    }
}
