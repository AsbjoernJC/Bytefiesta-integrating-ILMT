using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class JumpTheGunLevelInitializer : LevelInitializer
{
    [SerializeField] Transform[] playerCannonSpawns;
    [SerializeField] GameObject[] playerCannonPrefab;

    private List<JumpTheGunPlayer> team1PlayerData = new List<JumpTheGunPlayer>();
    private List<JumpTheGunPlayer> team2PlayerData = new List<JumpTheGunPlayer>();
    private Dictionary<int, JumpTheGunPlayer> teamPlayerData = new Dictionary<int, JumpTheGunPlayer>();
    private int team1Counter = 0;
    private int team2Counter = 0;


    // where int is playerIndex and the bool is whether or not the player's role is as a player or cannon controlling player

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
            AssignTeam(i);
            // SpawnPlayer(i);
            // should respawn players and interact with powerupinitializer
            
        }

        for (int i = 0; i < PlayerConfigurationManager.Instance.playerControllers.Count; i++)
        {
            SpawnPlayer(i);
            // should respawn players and interact with powerupinitializer
            
        }

    }
    

    // Instantiates the player in the current scene
    public override void SpawnPlayer(int playerIndex)
    {
        var playerController = PlayerConfigurationManager.Instance.playerControllers[playerIndex];
        var playerControlScheme = PlayerConfigurationManager.Instance.playerControlSchemes[playerIndex];



        // Checking which team the player belongs to and whether or not they control a cannon or a player
        var playerTeam = teamPlayerData[playerIndex].team;
        var hasCannonControl = teamPlayerData[playerIndex].controlsCannon;


        // Todo vary where a player spawns based on playerTeam and hasCannonControl
        // aswell as vary the player prefab
        // PlayerInput.Instantiate will done within the switch cases.
        switch (playerTeam)
        {
            case 1:
                break;
            case 2:
                break;
        }

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


    private void AssignPlayerRole(int playerIndex, int teamNumber)
    {

        var number = Random.Range(1, 3);
        IsPlayerRoleOccupied(number, playerIndex, teamNumber);

    }


    private void IsPlayerRoleOccupied(int randomNumber, int playerIndex, int teamNumber)
    {
        bool controlsCannon;


        // Random number can either be 1 or 2
        if (randomNumber == 1)
            controlsCannon = true;
        else
            controlsCannon = false;


        // If the player is on the first team and there has not been registered any player to team1Playerdata yet
        // We add the new JumpTheGunPlayer which contains information regarding if a player controls the cannon,
        // which team the player is on and the player's index
        if (teamNumber == 1 && team1PlayerData.Count == 0)
        {
            team1PlayerData.Add(new JumpTheGunPlayer(playerIndex, controlsCannon, teamNumber));
            teamPlayerData.Add(playerIndex, team1PlayerData[0]);
            return;
        }

        else if (teamNumber == 2 && team2PlayerData.Count == 0)
        {
            team2PlayerData.Add(new JumpTheGunPlayer(playerIndex, controlsCannon, teamNumber));
            teamPlayerData.Add(playerIndex, team2PlayerData[0]);
            return;
        }


    // There is a maximum of 4 players allowed in the game atm but it might change in the future
    // this is the reason why we loop through teamXPlayerData even though there will at max be 1 element in the list
    // at this point
        else if (teamNumber == 1 && team1PlayerData.Count != 0)
        {
            for (int i = 0; i < team1PlayerData.Count; i++)
            {
                if (team1PlayerData[i].controlsCannon == true)
                {
                    controlsCannon = false;
                    team1PlayerData.Add(new JumpTheGunPlayer(playerIndex, controlsCannon, teamNumber));
                    teamPlayerData.Add(playerIndex, team1PlayerData[1]);
                }
            }
        }

        else if (teamNumber == 2 && team2PlayerData.Count != 0)
        {
            for (int i = 0; i < team2PlayerData.Count; i++)
            {
                if (team2PlayerData[i].controlsCannon == true)
                {
                    controlsCannon = false;
                    team2PlayerData.Add(new JumpTheGunPlayer(playerIndex, controlsCannon, teamNumber));
                    teamPlayerData.Add(playerIndex, team2PlayerData[1]);
                }
            }
        }
    }


    private void AssignTeam(int playerIndex)
    {

        var teamNumber = Random.Range(1, 3);
        if (team1Counter == 2 && teamNumber == 1)
            teamNumber = 2;
        else if (team2Counter == 2 && teamNumber == 2)
            teamNumber = 1;

        switch (teamNumber)
        {
            case 1:
                AssignPlayerRole(playerIndex, teamNumber);
                team1Counter++;
                break;
            case 2:
                AssignPlayerRole(playerIndex, teamNumber);
                team2Counter++;
                break;
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
        string playerTag = player.tag;
        Destroy(player);

        StartCoroutine(RespawnPlayer(respawnTimer, playerToRespawnIndex));
    }

    // Will respawn the player after a short delay
    public override IEnumerator RespawnPlayer(float seconds, int playerIndex) 
    { 
        yield return new WaitForSeconds(seconds); 
        SpawnPlayer(playerIndex);
    } 


}

public class JumpTheGunPlayer 
{
    public JumpTheGunPlayer(int pi, bool cc, int pt)
    {
        playerIndex = pi;
        controlsCannon = cc;
        team = pt;
    }

    public override string ToString()
    {
        return ($"Player {playerIndex + 1}");
    }

    public bool controlsCannon { get; set; }
    public int playerIndex { get; set; }
    public int team { get; set; }
}