using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;
using UnityEngine.SceneManagement;

public class MinigameWinManager : MonoBehaviour
{

    [SerializeField]
    private GameObject playerButtonGroup;
    [SerializeField]
    private GridLayoutGroup buttonGroup;

    public int numberOfReadyPlayers = 0;


    public static MinigameWinManager Instance { get; private set; }

    protected void Awake() 
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
        if (numberOfReadyPlayers == PlayerConfigurationManager.Instance.numberOfActivePlayers)
        {
            if (DifficultyAndScore.Instance.gameWinner != "")
            {
                // Todo display the winner here for some time and then go back to the main menu
                // might be better to display the winner at the start of the scene and then allow the players to ready up
                // and start another game
                SceneManager.LoadScene("Menu");
                DifficultyAndScore.Instance.gameWinner = null;
                return;
            }

            // MysterDrink should only be loaded every 3rd minigame, maybe after the first minigame.
            if (DifficultyAndScore.Instance.finishedMinigames == 1 || DifficultyAndScore.Instance.finishedMinigames % 3 == 0)
            {
                SceneManager.LoadScene("MysteryDrink");
            }
            // Loads a random minigame. For now there is only KingoftheHill and GunnedDown
            else 
            {
                SceneManager.LoadScene(Random.Range(4, 6));
            }
                numberOfReadyPlayers = 0;
            }
    }


    private void AllowPlayerControl()
    {
        for (int playerIndex = 0; playerIndex < PlayerConfigurationManager.Instance.numberOfActivePlayers; playerIndex++)
        {
            var playerController = PlayerConfigurationManager.Instance.playerControllers[playerIndex];
            var inputUser = DifficultyAndScore.Instance.playerInputs[playerIndex].user;
            var playerControlScheme = PlayerConfigurationManager.Instance.playerControlSchemes[playerIndex];

            //Might be useful for debugging the problem where a player sometimes can't control their assigned button
            Debug.Log("MinigameWinMenu \n __________________________");
            Debug.Log($"Player {playerIndex + 1}'s deviceId = {PlayerConfigurationManager.Instance.playerControllers[playerIndex].deviceId}");

            // Spawns the playerButtonGroup and assigns the PlayerInput object to a specific controller, controller scheme etc.
            PlayerInput playerInput = PlayerInput.Instantiate(playerButtonGroup, playerIndex, playerControlScheme, -1, playerController);
            playerInput.transform.SetParent(buttonGroup.transform);
            // playerInput.enabled = true;
            // Pairs the correct controller with the playerIndex. So if player 1 is using xboxcontroller2 (starts at 0)
            // Player 1 will controll the leftmost button with xboxcontroller2
            // InputUser.PerformPairingWithDevice(playerController, inputUser, InputUserPairingOptions.UnpairCurrentDevicesFromUser);
        }
    }
}
