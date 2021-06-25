using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;
using UnityEngine.SceneManagement;
using System;
using Random = UnityEngine.Random;


// Configuration services
public class PlayerConfigurationManager : MonoBehaviour
{
    private List<PlayerConfiguration> playerConfigurations = new List<PlayerConfiguration>();

    [SerializeField]
    public int MaxPlayers = 1;

    [SerializeField]
    private GameObject controllerLayout;
    
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

        //Lambda expression in C#
        if (playerConfigurations.Count == MaxPlayers && playerConfigurations.All(p => p.isReady == true))
        {
            GameObject[] configurationManagerClones = GameObject.FindGameObjectsWithTag("PlayerConfiguration(Clone)");
            for (int i = 0; i < configurationManagerClones.Length; i++)
            {
                var playerInputComponent = configurationManagerClones[i].GetComponent<PlayerInput>();
                var playerIndex = playerInputComponent.playerIndex;
                playerControllers.Add(playerIndex, playerInputComponent.devices[0]);

                //Might be useful for debugging the problem where a player sometimes can't control their assigned button
                // Have not seen it happen in a while
                Debug.Log("CharacterSelection \n __________________________");
                Debug.Log($"Player {playerIndex + 1}'s deviceId = {playerInputComponent.devices[0].deviceId}");
                
                DifficultyAndScore.Instance.playerInputs.Add(playerIndex, playerInputComponent);
                playerControlSchemes.Add(playerInputComponent.playerIndex, playerInputComponent.currentControlScheme);
            }


            // Loads a random minigame
            var unchosenMinigames = DifficultyAndScore.Instance.unchosenMinigames;
            int chosenScene = unchosenMinigames[Random.Range(0, unchosenMinigames.Count)];
            unchosenMinigames.RemoveAll(scene => scene == chosenScene);
            DifficultyAndScore.Instance.lastMinigameIndex = chosenScene;
            SceneManager.LoadScene(chosenScene);
        }
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
