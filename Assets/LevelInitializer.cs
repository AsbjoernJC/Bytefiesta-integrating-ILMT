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

    // Start is called before the first frame update
    void Start()
    {
        foreach (var player in PlayerConfigurationManager.playerControllers)
        {
            var playerController = PlayerConfigurationManager.playerControllers[player.Key];
            var playerControlScheme = PlayerConfigurationManager.playerControlSchemes[player.Key];

            PlayerInput playerInput = PlayerInput.Instantiate(playerPrefab, player.Key, playerControlScheme, -1, playerController);
            playerInput.transform.position = new Vector3 (playerSpawns[player.Key].transform.position.x, playerSpawns[player.Key].transform.position.y, 0);
            // Activates the player input component on the prefab we just instantiated
            // We have the component disabled by default, otherwise it could not be a "selectable object" independent of the PlayerInput component on the cursor
            // in the selection screen
            playerInput.enabled = true;

            //  *** It seems...that the above Instantiation doesn't exactly work... I'm assuming, because the PlayerInput component on the prefab is starting off
            // disabled, that it...doesn't work.  This code here will force it to keep the device/scheme/etc... that we tried to assign the wretch above!
            var inputUser = playerInput.user;
            playerInput.SwitchCurrentControlScheme(playerControlScheme);
            InputUser.PerformPairingWithDevice(playerController, inputUser, InputUserPairingOptions.UnpairCurrentDevicesFromUser);

            // If not the first object (sword/vehicle/etc...) just instantiate, don't associate a PlayerInput
        }
    }

}
