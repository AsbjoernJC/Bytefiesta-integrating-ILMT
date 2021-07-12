using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
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


        // Checks if multiple people won or just one person.
        switch (DifficultyAndScore.Instance.gameWinners.Count)
        {
            case 1:
                DisplayWinner();
                break;

            case 2:
                DisplayWinners();
                break;
        }

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
                var unchosenMinigames = DifficultyAndScore.Instance.unchosenMinigames;
                var minigames = DifficultyAndScore.Instance.tailoredMinigames;

                // picks a random scene index by choosing the value at a random index in unchosenMinigames
                int chosenScene = unchosenMinigames[Random.Range(0, unchosenMinigames.Count)];
                unchosenMinigames.RemoveAll(scene => scene == chosenScene);

                // Loops through all the possible minigames and adds every minigame/scene to
                // unchosenMinigames except chosenScene as this is the minigame they are about to play
                if (unchosenMinigames.Count == 0)
                {
                    for (int i = 0; i < minigames.Count; i++)
                    {
                        if (minigames[i] != chosenScene)
                        {
                            unchosenMinigames.Add(minigames[i]);
                        }
                    }
                }
                DifficultyAndScore.Instance.lastMinigameIndex = chosenScene;
                SceneManager.LoadScene(chosenScene);
            }
                numberOfReadyPlayers = 0;
            }
    }


    // If there is only a single person who won/with 5 crowns
    private void DisplayWinner()
    {

    }

    
    // As 2vs2 games are allowed there could be multiple winners in which case this function will be called
    private void DisplayWinners()
    {

    }


    private void AllowPlayerControl()
    {
        for (int playerIndex = 0; playerIndex < PlayerConfigurationManager.Instance.numberOfActivePlayers; playerIndex++)
        {
            var playerController = PlayerConfigurationManager.Instance.playerControllers[playerIndex];
            var inputUser = DifficultyAndScore.Instance.playerInputs[playerIndex].user;
            var playerControlScheme = PlayerConfigurationManager.Instance.playerControlSchemes[playerIndex];


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
