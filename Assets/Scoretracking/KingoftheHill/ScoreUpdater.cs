using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreUpdater : MonoBehaviour
{
    [SerializeField]
    private GameObject[] playerUI;
    // playerScores[0].text = "1" would work.

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnPlayerScoreUI(int playerIndex)
    {
        Vector3 position;
        position = new Vector3(600, 267, 0);
        int playerMultiplier = playerIndex*100;

        if (playerIndex > 0)
            position = new Vector3(playerMultiplier, -267, 0);    

        var playerPicture = Instantiate(playerUI[playerIndex]);

        playerPicture.transform.parent = gameObject.transform;

        // position is based on transform.parent
        // with position being = new Vector3(600, 267, 0). The ui will be placed at pos x = 112,5 pos y = -18
        playerPicture.transform.position = position;

    }

    public static void UpdatePlayerScoreUI()
    {

    }
}
