using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class HeadMashScorePasser : MonoBehaviour
{
    [SerializeField] private GameObject gameInstructionsGameObject;
    public Dictionary<string, bool> playerAliveStatus;
    public static HeadMashScorePasser Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Debug.Log("Trying to create another Singleton object");

    }


    // Start is called before the first frame update
    void Start()
    {
        PlayerVitalStatusStart();
    }


    // Function intializes playerAlivStatus at the start of each round. 'true' in this case will indicate that the player is alive
    private void PlayerVitalStatusStart()
    {
        playerAliveStatus = new Dictionary<string, bool>();
        for (int i = 0; i < PlayerConfigurationManager.Instance.numberOfActivePlayers; i++)
        {
            playerAliveStatus.Add($"Player {i + 1}", true);
        }

    }

    
    // Functions is called when a player dies and sets the boolean value to false using the key which is the name of the player
    // eg. "Player 1", "Player 2",... etc.
    public void PlayerVitalStatusOnDeath(string playerName)
    {
        playerAliveStatus[playerName] = false;


        // Checks if there is only one player alive
        if (LastManStanding())
        {
            FindTheRoundWinner();
        }

    }


    private bool LastManStanding()
    {
        // Will amount to the number of players who are alive
        var playersAliveCount = playerAliveStatus.Values.Count(v => v == true);

        // If one player is left, the player has obviously won the round
        if (playersAliveCount == 1)
            return true;

        return false;
    }


    private void FindTheRoundWinner()
    {
        var lastPlayerAlive = playerAliveStatus.Where((p, v) => p.Value == true);



        foreach (var elem in lastPlayerAlive)
        {
            PointMinigameTracker.instance.playerScores[elem.Key] ++;
            ScoreUpdater.Instance.UpdatePlayerScoreUI(elem.Key);

            // Checks if the player has won the minigame as it is first to three points
            if (PointMinigameTracker.instance.playerScores[elem.Key] == 3)
            {
                MinigameIsOver(elem.Key);
                return;
            }

            RemovePlayerBeforeNewRound(elem.Key);
        }


        BeginNewRound();
    }


    private void RemovePlayerBeforeNewRound(string playerName)
    {
        Destroy(GameObject.Find(playerName));
    }


    private void BeginNewRound()
    {
        Time.timeScale = 0;
        gameInstructionsGameObject.SetActive(true);

        for (int i = 0; i < PlayerConfigurationManager.Instance.numberOfActivePlayers; i++)
            NewRoundInitializePlayers.Instance.SpawnPlayer(i);

        gameInstructionsGameObject.GetComponent<GameInstructionsManager>().StartCoroutine("StartNewRoundCountDown");

        Time.timeScale = 1;
    }

    
    private void MinigameIsOver(string playerName)
    {
        PointMinigameTracker.instance.MiniGameEnd(playerName);
    }

}
