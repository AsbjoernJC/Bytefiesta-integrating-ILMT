using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;

// for bugs see: https://www.youtube.com/watch?v=_5pOiYHJgl0
public class LevelInitializer : MonoBehaviour
{
    [SerializeField]
    private Transform[] playerSpawns;
    [SerializeField]
    private GameObject playerPrefab;
    int playerToRespawnIndex;
    public float respawnTimer = 4f;

    public static LevelInitializer Instance { get; private set; }


    void Awake() 
    {
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
        }
    }
    

// could try making it a public static void, so we can respawn players using the function
public void SpawnPlayer(int playerIndex)
{
    var player = PlayerConfigurationManager.playerControllers[playerIndex];
    var playerController = PlayerConfigurationManager.playerControllers[playerIndex];
    var playerControlScheme = PlayerConfigurationManager.playerControlSchemes[playerIndex];

    PlayerInput playerInput = PlayerInput.Instantiate(playerPrefab, playerIndex, playerControlScheme, -1, playerController);
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
}


// This should be a coroutine, however, i could not get it working. If i recall correctly the player respawned immediately
// even though respawnTimer was equal to 4f, or the player did not respawn at all, and the function just seemed to stop after yield return statement.

// public IEnumerator RespawnPlayer(GameObject player)
// {
//     playerToRespawnIndex = Int16.Parse(player.name.Split( )[1]) - 1;
//     Destroy(player);
//     yield return new WaitForSeconds(respawnTimer);
//     SpawnPlayer(playerToRespawnIndex);
// }
public void RespawnPlayer(GameObject player)
{
    playerToRespawnIndex = Int16.Parse(player.name.Split( )[1]) - 1;
    Destroy(player);
    InvokeRepeating("WhyCantIGetCoroutinesWorking", respawnTimer, 0f);
}



private void WhyCantIGetCoroutinesWorking()
{
    Debug.Log("This is my scuffed coroutine");
    SpawnPlayer(playerToRespawnIndex);
    CancelInvoke("WhyCantIGetCoroutinesWorking");
}

}
