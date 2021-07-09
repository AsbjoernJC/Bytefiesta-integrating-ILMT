using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MysteryDrinkManager : MonoBehaviour
{
    [SerializeField]
    private Image mysteryPlayerImage;

    [SerializeField]
    private Sprite[] playerSprites;
    [SerializeField]
    private GameObject playerButtonGroup;
    [SerializeField]
    private GridLayoutGroup buttonGroup;

    public int numberOfReadyPlayers = 0;


    public static MysteryDrinkManager Instance { get; private set; }

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
    // Start is called before the first frame update
    private void Start() 
    {
        mysteryPlayerImage.sprite = playerSprites[Random.Range(0, PlayerConfigurationManager.Instance.numberOfActivePlayers)];
        mysteryPlayerImage.color = new Color32(0, 0, 0, 255);
        StartCoroutine("ShufflePlayers");
    }

    private void Update()
    {
        if (numberOfReadyPlayers == PlayerConfigurationManager.Instance.numberOfActivePlayers)
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
                SceneManager.LoadScene(chosenScene);
        }
    }

// Takes a random sprite from playerSprites (only the ones being used) and shuffles to a new one every x seconds.
// It slows down over time
    private IEnumerator ShufflePlayers()
    {
        float timePassed = 0f;
        float rotationSpeed = 0.27f;
        while (timePassed < 3f)
        {
            mysteryPlayerImage.sprite = playerSprites[Random.Range(0, PlayerConfigurationManager.Instance.numberOfActivePlayers)];
            yield return new WaitForSeconds(rotationSpeed);
            timePassed += rotationSpeed + Time.deltaTime;
        }
        while (timePassed >= 3 && timePassed < 8.5)
        {
            mysteryPlayerImage.sprite = playerSprites[Random.Range(0, PlayerConfigurationManager.Instance.numberOfActivePlayers)];
            // rotationSpeed is slowed down in this part
            if (rotationSpeed >= 0)
            // 13f is just an arbitrary amount of time. It fit well
                rotationSpeed += Time.deltaTime * 13f;
                if (rotationSpeed < 0)
                    rotationSpeed = 0;
            yield return new WaitForSeconds(rotationSpeed);
            timePassed += rotationSpeed + Time.deltaTime;
        }
        

        mysteryPlayerImage.color = new Color32(255, 255, 255, 255);
        AllowPlayerControl();
        yield return null;
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
