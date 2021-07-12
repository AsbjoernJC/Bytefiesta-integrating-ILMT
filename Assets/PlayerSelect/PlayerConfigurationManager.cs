using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;
using UnityEngine.SceneManagement;
using System;
using Random = UnityEngine.Random;
using TMPro;


// Configuration services
public class PlayerConfigurationManager : MonoBehaviour
{
    private List<PlayerConfiguration> playerConfigurations = new List<PlayerConfiguration>();

    [SerializeField]
    private GameObject controllerLayout;

    [SerializeField] GameObject countdown;
    [SerializeField] TMP_Text countdownText;
    [SerializeField] private int waitToStartCount = 3;
    
    public int numberOfActivePlayers { get; set; } = 0;

    public Dictionary<int, string> playerControlSchemes = new Dictionary<int, string>();
    
    public Dictionary<int, InputDevice> playerControllers = new Dictionary<int, InputDevice>();
    
    //Only one instance of the PlayerConfigurationManager class can be active at a time
    public static PlayerConfigurationManager Instance { get; private set; }

    private void Awake() 
    {
        if (Instance != null)
        {
            Debug.Log("SINGLETON - Trying to create another instance of singleton!!");
        }
        else
        {
            Instance = this;
        }        
    }

    public void ReadyPlayer(int index)
    {
        playerConfigurations[index].isReady = true;


        if (playerConfigurations.Count >= 2 && playerConfigurations.All(p => p.isReady == true))
        {
            StartCoroutine(StartCountdown(playerConfigurations.Count));
        }
    }


// When all the active players in the characterselection lobby has pressed ready a countdown from waitToStartCount
// will commence
    private IEnumerator StartCountdown(int numberOfReadyPlayers)
    {
        float timePassed = 0f;
        countdownText.text = waitToStartCount.ToString();
        countdown.SetActive(true);

        while (timePassed < waitToStartCount)
        {

            if (Math.Round(timePassed, 2) % 1 == 0 && Math.Round(timePassed, 2) != 0)
            {
                switch (Math.Round(timePassed, 2))
                {
                    case 1:
                        countdownText.text = (waitToStartCount - 1).ToString();
                        break;
                    case 2:
                        countdownText.text = (waitToStartCount - 2).ToString(); 
                        break;
                    case 3:
                        countdownText.text = (waitToStartCount - 3).ToString();
                        break;
                    case 4:
                        countdownText.text = (waitToStartCount - 4).ToString();
                        break;
                    case 5:
                        countdownText.text = (waitToStartCount - 5).ToString(); 
                        break;
                }
            }

            if (numberOfActivePlayers != numberOfReadyPlayers)
            {
                // Deactivate the countdown canvas group and set the text to 5
                countdown.SetActive(false);
                countdownText.text = "5";
                yield break;
            }
            
            timePassed += Time.deltaTime;
            yield return null;
        }

        StartGame();
    }


    private void StartGame()
    {
        //Lambda expression in C#
        GameObject[] configurationManagerClones = GameObject.FindGameObjectsWithTag("PlayerConfiguration(Clone)");
        for (int i = 0; i < configurationManagerClones.Length; i++)
        {
            var playerInputComponent = configurationManagerClones[i].GetComponent<PlayerInput>();
            var playerIndex = playerInputComponent.playerIndex;
            playerControllers.Add(playerIndex, playerInputComponent.devices[0]);

            DifficultyAndScore.Instance.playerInputs.Add(playerIndex, playerInputComponent);
            playerControlSchemes.Add(playerInputComponent.playerIndex, playerInputComponent.currentControlScheme);
        }



        // Loads a random minigame
        var unchosenMinigames = DifficultyAndScore.Instance.unchosenMinigames;
        var tailoredMinigames = DifficultyAndScore.Instance.tailoredMinigames;

        // If the amount of players is equal to 4 then we should add 4 player minigames to the unchosen minigames
        if (playerConfigurations.Count == 4)
        {
            unchosenMinigames.AddRange(DifficultyAndScore.fourPlayerMinigames);
            tailoredMinigames.AddRange(DifficultyAndScore.minigames);
            tailoredMinigames.AddRange(DifficultyAndScore.fourPlayerMinigames);
        }

        int chosenScene = unchosenMinigames[Random.Range(0, unchosenMinigames.Count)];
        unchosenMinigames.RemoveAll(scene => scene == chosenScene);
        DifficultyAndScore.Instance.lastMinigameIndex = chosenScene;
        SceneManager.LoadScene(chosenScene);
    }


    //Player joins when pressing y and doing it once more to ready up. Functions is called when
    // the Player Input Manager invokes the unity event in the 'PlayerSelect' scene.
    public void HandlePlayerJoin(PlayerInput pi)
    {
        string controllerName;
        string controllerID;
        int actualControllerNumber;
        if (!playerConfigurations.Any(p => p.playerIndex == pi.playerIndex))
        {
            if (pi.currentControlScheme != "Controller")
            {
                // Todo
                // Could use this place in the future to change the images in the menu for this player
                Debug.Log("Not a controller");
            }
            else
            {
                controllerName = pi.devices[0].name;
                controllerID = string.Join("", controllerName.Where(char.IsDigit));
                if (controllerID == "")
                {
                    controllerID = "1";
                    controllerLayout.GetComponent<ControllerImageAndText>().ChangeControllerText(controllerID, pi.playerIndex);
                }
                else
                {
                    actualControllerNumber = Int32.Parse(controllerID) + 1;
                    controllerID = actualControllerNumber.ToString();
                    controllerLayout.GetComponent<ControllerImageAndText>().ChangeControllerText(controllerID, pi.playerIndex);
                }

            }
            pi.transform.SetParent(transform);
            playerConfigurations.Add(new PlayerConfiguration(pi));
        }
        numberOfActivePlayers = PlayerInput.all.Count;
    }

}


// Lets you control the 'PlayerConfiguration' prefab which is used for the 'PlayerSetupMenu' scene prefab.
public class PlayerConfiguration
{
    public PlayerConfiguration(PlayerInput pi)
    {
        playerIndex = pi.playerIndex;
        input = pi;
    }
    public PlayerInput input { get; set; }
    public int playerIndex { get; set; }
    public bool isReady { get; set; }
}
