using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;
public class NewRoundInitializePlayers : MonoBehaviour
{

    [SerializeField] protected Transform[] playerSpawns;
    [SerializeField] protected GameObject[] playerPrefab;


    public static NewRoundInitializePlayers Instance { get; private set; }



    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Debug.Log("Trying to create another singleton object");
    }


    public void SpawnPlayer(int playerIndex)
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


    }
}
