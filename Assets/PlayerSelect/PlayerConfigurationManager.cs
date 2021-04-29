using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;
using UnityEngine.SceneManagement;

public class PlayerConfigurationManager : MonoBehaviour
{
    private List<PlayerConfiguration> playerConfigurations;

    [SerializeField]
    private int MaxPlayers = 1;

    //Singleton pattern. https://en.wikipedia.org/wiki/Singleton_pattern. 
    //Only one instance of the PlayerConfigurationManager class can be active at a time

    public static PlayerConfigurationManager Instance { get; private set; }

    private void Awake() 
    {
        if(Instance != null)
        {
            Debug.Log("SINGLETON - Trying to create another instance of singleton!!");
        }
        else
        {
            Instance = this;
            // DontDestroyOnLoad(Instance);
            playerConfigurations = new List<PlayerConfiguration>();
        }        
    }

    public List<PlayerConfiguration> GetPlayerConfigs()
    {
        return playerConfigurations;
    }

    public void SetPlayercolor(int index, Material color)
    {
        playerConfigurations[index].playerMaterial = color;
    }

    public void ReadyPlayer(int index)
    {
        playerConfigurations[index].isReady = true;

        //Lambda expression in C#
        if (playerConfigurations.Count == MaxPlayers && playerConfigurations.All(p => p.isReady == true))
        {
            SceneManager.LoadScene("KingoftheHill2304");
        }
    }

    //Player joins when pressing join and when pressing y to ready up
    public void HandlePlayerJoin(PlayerInput pi)
    {
        Debug.Log("Player joined: " + pi.playerIndex);
        if (!playerConfigurations.Any(p => p.playerIndex == pi.playerIndex))
        {
            pi.transform.SetParent(transform);
            playerConfigurations.Add(new PlayerConfiguration(pi));
        }
    }

}

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
    public Material playerMaterial { get; set; }
}
