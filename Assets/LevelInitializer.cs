using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;
using UnityEngine.SceneManagement;
// for bugs see: https://www.youtube.com/watch?v=_5pOiYHJgl0
public class LevelInitializer : MonoBehaviour
{
    [SerializeField]
    private Transform[] playerSpawns;
    [SerializeField]
    private GameObject[] playerPrefab;
    private GameObject scoreUI;
    public string sceneName;
    int playerToRespawnIndex;
    int numberOfScoreUI;
    public float respawnTimer = 4f;
    public static Dictionary<string, Dictionary<string, bool>> levelRules = new Dictionary<string, Dictionary<string, bool>>()
    {
        {"KingoftheHill", new Dictionary<string, bool>{
            {"hasPowerUp", true},
            {"playersRespawn", true},
            {"hasScoreUI", true},
            {"lastManStanding", false}
        }
        },

        {"GunnedDown", new Dictionary<string, bool>{
            {"hasPowerUp", false},
            {"playersRespawn", false},
            {"hasScoreUI", false},
            {"lastManStanding", true}
        }
        }
    };
    public static LevelInitializer Instance { get; private set; }


    void Awake() 
    {
        sceneName = SceneManager.GetActiveScene().name;

        if(Instance != null)
        {
            Debug.Log("SINGLETON - Trying to create another instance of singleton!!");
        }
        else
        {
            Instance = this;
        }        
    }
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < PlayerConfigurationManager.playerControllers.Count; i++)
        {
            SpawnPlayer(i);
            // Todo be able to differentiate whether or not the gamemode displays scores,
            // should respawn players and interact with powerupinitializer
            
            if (levelRules[sceneName]["hasScoreUI"])
                InstantiatePlayerUI(i);
        }

    }
    

    // Instantiates the player in the current scene
    public void SpawnPlayer(int playerIndex)
    {
        var playerController = PlayerConfigurationManager.playerControllers[playerIndex];
        var playerControlScheme = PlayerConfigurationManager.playerControlSchemes[playerIndex];

        PlayerInput playerInput = PlayerInput.Instantiate(playerPrefab[playerIndex], playerIndex, playerControlScheme, -1, playerController);
        playerInput.name = "Player " + (playerIndex + 1).ToString();
        playerInput.tag = "Player " + (playerIndex + 1).ToString();
        playerInput.transform.position = new Vector3 (playerSpawns[playerIndex].transform.position.x, playerSpawns[playerIndex].transform.position.y, 0);
        // Activates the player input component on the prefab we just instantiated
        // We have the component disabled by default, otherwise it could not be a "selectable object" independent of the PlayerInput component on the cursor
        // in the selection screen
        playerInput.enabled = true;

        //  *** It seems...that the above Instantiation doesn't exactly work... I'm assuming, because the PlayerInput component on the prefab is starting off
        // disabled, that it...doesn't work.  This code here will force it to keep the device/scheme/etc... that we tried to assign the wretch above!
        var inputUser = playerInput.user;
        playerInput.SwitchCurrentControlScheme(playerControlScheme);
        InputUser.PerformPairingWithDevice(playerController, inputUser, InputUserPairingOptions.UnpairCurrentDevicesFromUser);

        var playerObject = GameObject.Find(playerInput.name); 

        // Example with 3 players:
        // If Player 3 is killed and hasn't respawned and Player 2 then is killed, it will try to insert Player 3 at index[2] even though 
        // activePlayers = {Player 1} where Player 1 is a GameObject. Therefore it results in an ArgumentOutOfRangeException
        try
        {
            PowerUpInitializer.activePlayers.Insert(playerIndex, playerObject);
        }
        catch (ArgumentOutOfRangeException)
        {
            PowerUpInitializer.activePlayers.Add(playerObject);
        }

    }

    private void InstantiatePlayerUI(int playerIndex)
    {
        scoreUI = GameObject.FindGameObjectWithTag("ScoreUI");
        scoreUI.GetComponent<ScoreSpawner>().SpawnPlayerScoreUI(playerIndex);
    }

    public void PlayerDeathInformation(GameObject player)
    {
        playerToRespawnIndex = Int16.Parse(player.name.Split( )[1]) - 1;
        Destroy(player);

        // Checks in levelRules if the players are able to get powerups
        if (levelRules[sceneName]["hasPowerUp"])
        {
            int playerListIndex = PowerUpInitializer.activePlayers.IndexOf(player);
            if (playerListIndex == -1)
                return;
            PowerUpInitializer.activePlayers.RemoveAt(playerListIndex);
        }
        
        // Checks in levelRules if this players should respawn in this minigame
        if (levelRules[sceneName]["playersRespawn"])
            StartCoroutine(RespawnPlayer(4, playerToRespawnIndex));

        // Checks in the levelRules if is a lastmanstanding type of game meaning the last player alive wins
        if (levelRules[sceneName]["lastManStanding"])
        {
            //The maximum amount of players is 4 therefore 4 - the active players will result in the correct player standing
            // on death eg. 3 player game. Player 2 dies first and therefore gets player standing 3 - 0 = 3.
            // So the player correctly gets a 3rd place placement.
            LastManStanding.playerStandings[player.name] = PlayerConfigurationManager.numberOfActivePlayers - LastManStanding.deadPlayers;
            LastManStanding.deadPlayers ++;

            // When all players but one are dead. That is the last man standing.
            if (LastManStanding.deadPlayers == PlayerConfigurationManager.numberOfActivePlayers - 1)
            {
                for (int i = 1; i < PlayerConfigurationManager.numberOfActivePlayers; i++)
                {
                    // Keyvalue pair = string (where string is Player 1-4), int.
                    // When the int is unchanged that is = 0 then it is the last player standing
                    if (LastManStanding.playerStandings[$"Player {i}"] == 0)
                    {
                        LastManStanding.playerStandings[$"Player {i}"] = 1;
                        LastManStanding.MiniGameEnd($"Player {i}");
                    }
                }
            }
        }
    }

    public IEnumerator RespawnPlayer(int seconds, int playerIndex) 
    { 
        yield return new WaitForSeconds(seconds); 
        SpawnPlayer(playerIndex);
    } 


}
