using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;
using UnityEngine.SceneManagement;

public class JumpTheGunLevelInitializer : LevelInitializer
{
    [SerializeField] Transform[] playerCannonSpawns;
    int playerToRespawnIndex;
    int numberOfScoreUI;


    void Awake() 
    {
        // Tries to locate a PowerUpInitializer gameobject in the scene/minigame
        powerupInitializer = GameObject.Find("PowerUpInitializer");

        sceneName = SceneManager.GetActiveScene().name;

    }
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < PlayerConfigurationManager.Instance.playerControllers.Count; i++)
        {
            SpawnPlayer(i);
            // should respawn players and interact with powerupinitializer
            
            if (LevelInitializer.levelRules[sceneName]["hasScoreUI"])
                InstantiatePlayerUI(i);
        }

    }
    

    // Instantiates the player in the current scene
    public override void SpawnPlayer(int playerIndex)
    {
        var playerController = PlayerConfigurationManager.Instance.playerControllers[playerIndex];
        var playerControlScheme = PlayerConfigurationManager.Instance.playerControlSchemes[playerIndex];

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
        
        playerInput.SwitchCurrentControlScheme(playerControlScheme);
        var inputUser = playerInput.user;
        InputUser.PerformPairingWithDevice(playerController, inputUser, InputUserPairingOptions.UnpairCurrentDevicesFromUser);

        var playerObject = GameObject.Find(playerInput.name); 

        // Example with 3 players:
        // If Player 3 is killed and hasn't respawned and Player 2 then is killed, it will try to insert Player 3 at index[2] even though 
        // activePlayers = {Player 1} where Player 1 is a GameObject. Therefore it results in an ArgumentOutOfRangeException
        
    // if the minigame has a powerUpInitializer we will have to spawn powerups
        if (powerupInitializer != null)
        {
            try
            {
                powerupInitializer.GetComponent<PowerUpInitializer>().activePlayers.Insert(playerIndex, playerObject);
            }
            catch (ArgumentOutOfRangeException)
            {
                powerupInitializer.GetComponent<PowerUpInitializer>().activePlayers.Add(playerObject);
            }
        }


    }


// Spawns the UI which holds the scores of the players in the minigame
    private void InstantiatePlayerUI(int playerIndex)
    {
        scoreUI = GameObject.FindGameObjectWithTag("ScoreUI");
        scoreUI.GetComponent<ScoreSpawner>().SpawnPlayerScoreUI(playerIndex);
    }


// Gets called on death
    public override void PlayerDeathInformation(GameObject player)
    {
        playerToRespawnIndex = Int16.Parse(player.name.Split( )[1]) - 1;
        Destroy(player);

        // Checks in levelRules if this players should respawn in this minigame
        if (LevelInitializer.levelRules[sceneName]["playersRespawn"])
            StartCoroutine(RespawnPlayer(respawnTimer, playerToRespawnIndex));
    }

    // Will respawn the player after a short delay
    public override IEnumerator RespawnPlayer(float seconds, int playerIndex) 
    { 
        yield return new WaitForSeconds(seconds); 
        SpawnPlayer(playerIndex);
    } 


}
