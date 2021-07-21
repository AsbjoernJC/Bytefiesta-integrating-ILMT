using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;
using System;


public class MinigameWinManager : MonoBehaviour
{

    [SerializeField] private GameObject playerButtonGroup;
    [SerializeField] private GridLayoutGroup buttonGroup;
    [SerializeField] private float displayWinnerTimer = 5f;
    [SerializeField] private GameObject playerWinnerCanvas;
    [SerializeField] private GameObject playerWinnersCanvas;
    [SerializeField] private Image winnerSprite;
    [SerializeField] private Image[] winnersSprites;
    [SerializeField] private Sprite[] playerSprites;
    public int numberOfReadyPlayers = 0;
    private bool instantiatedReadyButton = false;


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
            // Loads a random minigame
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

        
        if (!instantiatedReadyButton && DifficultyAndScore.Instance.gameWinner == "")
            AllowPlayerControl();
    }


    // Used when there is only a single person who won/with 5 crowns
    public IEnumerator DisplayWinner()
    {
        // As players are named Player 1, Player 2, Player 3, Player 4
        // "Player 1".split( )[1] would equal "1"
        int playerIndex = Int32.Parse(DifficultyAndScore.Instance.gameWinner.Split( )[1]) - 1;

        // Sets the image's sprite to the winner's sprite
        winnerSprite.sprite = playerSprites[playerIndex];

        // Lets the players see the canvas with text and the winner's sprite
        playerWinnerCanvas.SetActive(true);
        yield return new WaitForSeconds(displayWinnerTimer);
        playerWinnerCanvas.SetActive(false);

        // After the winners have been displayed the player's are allowed to push the ready button
        AllowPlayerControl();
    }

    
    // As 2vs2 games are allowed there could be multiple winners in which case this function will be called
    public IEnumerator DisplayWinners()
    {
        int firstWinnerIndex = Int32.Parse(DifficultyAndScore.Instance.gameWinners[0].Split( )[1]) - 1;
        int secondWinnerIndex = Int32.Parse(DifficultyAndScore.Instance.gameWinners[1].Split( )[1]) - 1;
        
        // Sets the images' sprites to the winners' sprites
        winnersSprites[0].sprite = playerSprites[firstWinnerIndex];
        winnersSprites[1].sprite = playerSprites[secondWinnerIndex];

        // Lets the players see the canvas with text and the winners' sprites
        playerWinnersCanvas.SetActive(true);
        yield return new WaitForSeconds(displayWinnerTimer);
        playerWinnersCanvas.SetActive(false);


        // After the winners have been displayed the player's are allowed to push the ready button
        AllowPlayerControl();
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

        // Bool is used to control whether or not AllowPlayerControl will be executed
        instantiatedReadyButton = true;
    }
}
