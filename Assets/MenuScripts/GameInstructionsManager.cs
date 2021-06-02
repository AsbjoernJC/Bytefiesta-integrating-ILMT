using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;
using UnityEngine.SceneManagement;

public class GameInstructionsManager : MonoBehaviour
{
    [SerializeField]
    private GameObject levelInitializer;
    [SerializeField]
    private GameObject gameInstructions;
    [SerializeField]
    private GameObject playerButtonGroup;
    [SerializeField]
    private GridLayoutGroup buttonGroup;

    // Should add a script where the player presses ready that inherits from MysteryDrinkPlayer or maybe rather the other way around.
    public static int numberOfReadyPlayers = 0;


    public static GameInstructionsManager Instance { get; private set; }

    private void Awake() 
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


    private void Start() 
    {
        AllowPlayerControl();    
    }
    private void Update()
    {
        if (numberOfReadyPlayers == PlayerConfigurationManager.numberOfActivePlayers)
        {
            StartLevelInitialization();
        }
    }


    private void AllowPlayerControl()
    {
        for (int playerIndex = 0; playerIndex < PlayerConfigurationManager.numberOfActivePlayers; playerIndex++)
        {
            var playerController = PlayerConfigurationManager.playerControllers[playerIndex];
            var inputUser = DifficultyAndScore.playerInputs[playerIndex].user;
            var playerControlScheme = PlayerConfigurationManager.playerControlSchemes[playerIndex];

            // Spawns the playerButtonGroup and assigns the PlayerInput object to a specific controller, controller scheme etc.
            PlayerInput playerInput = PlayerInput.Instantiate(playerButtonGroup, playerIndex, playerControlScheme, -1, playerController);
            playerInput.transform.SetParent(buttonGroup.transform);
            playerInput.enabled = true;
            // Pairs the correct controller with the playerIndex. So if player 1 is using xboxcontroller2 (starts at 0)
            // Player 1 will controll the leftmost button with xboxcontroller2
            InputUser.PerformPairingWithDevice(playerController, inputUser, InputUserPairingOptions.UnpairCurrentDevicesFromUser);
        }
    }

    private void StartLevelInitialization()
    {
        gameInstructions.SetActive(false);
        levelInitializer.SetActive(true);
    }
}
