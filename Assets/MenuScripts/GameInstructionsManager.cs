using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;
using TMPro;

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
    [SerializeField]
    private TMP_Text countdownText;
    [SerializeField]
    private GameObject instructionImage;
    [SerializeField]
    private GameObject readyButtonGroup;

    private bool startedInitialiazation = false;
    
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
        Time.timeScale = 0f;
        instructionImage.SetActive(true);
        readyButtonGroup.SetActive(true);
        AllowPlayerControl();    
    }
    private void Update()
    {
        if (numberOfReadyPlayers == PlayerConfigurationManager.numberOfActivePlayers && startedInitialiazation == false)
        {
            startedInitialiazation = true;
            StartLevelInitialization();
        }
    }

// Spawns in a menu where a player's assigned controller (from the characterselect menu) will be assigned to a menu
// Here they are allowed to push a button that increments numberOfReadyPlayers, which is a way for the player to tell
// That they are ready.
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
            // InputUser.PerformPairingWithDevice(playerController, inputUser, InputUserPairingOptions.UnpairCurrentDevicesFromUser);
        }
    }

// Called when all the players are ready and it has not been called before.
    private void StartLevelInitialization()
    {
        Time.timeScale = 0f;
        StartCoroutine("CountDown");
    }
    

    // Counts down to the start of the minigame ie. the excact point in time where players are allowed to move.
    private IEnumerator CountDown()
    {

        //Disables UI elements that should not be displayed when counting down
        instructionImage.SetActive(false);
        readyButtonGroup.SetActive(false);

        // Spawns in the players
        levelInitializer.SetActive(true);

        countdownText.text = "3";
        yield return new WaitForSecondsRealtime(1f);

        countdownText.text = "2";
        yield return new WaitForSecondsRealtime(1f);

        countdownText.text = "1";
        yield return new WaitForSecondsRealtime(1f);

        countdownText.text = "GO";

        // Disables the entirety of the game instructions UI panel. 
        gameInstructions.SetActive(false);

        // Allows for playermovement
        Time.timeScale = 1f;
        numberOfReadyPlayers = 0;
    }
}
