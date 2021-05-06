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
            RespawnPlayer(i);
        }
    }
    

// could try making it a public static void, so we can respawn players using the function
public void RespawnPlayer(int playerIndex)
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


}
