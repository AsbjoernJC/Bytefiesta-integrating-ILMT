using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine.InputSystem.Users;

public class DifficultyAndScore : MonoBehaviour
{
    public static string difficulty;
    public static int finishedMinigames = 0;
    public static Dictionary<string, bool> difficulties = new Dictionary<string, bool>
    {
        {"Hardcore", false},
        {"Medium", false},
        {"Light", false}
    };    

    public static Dictionary<string, int> acrossGamemodePlayerScore = new Dictionary<string, int>()
    {
        {"Player 1", 0},
        {"Player 2", 0},
        {"Player 3", 0},
        {"Player 4", 0}
    };
    public static Dictionary<int, PlayerInput> playerInputs = new Dictionary<int, PlayerInput>();
    
    public static DifficultyAndScore Instance { get; set; }
    

    private void Awake()
    {
        if(Instance != null)
        {
            Debug.Log("SINGLETON - Trying to create another instance of singleton!!");
            DontDestroyOnLoad(Instance);
        }
        else
        {
            Instance = this;
        }        
    }

    public static void SetDifficulty(string gamemode)
    {
        difficulty = gamemode;
        difficulties[gamemode] = true;
        Debug.Log($"{gamemode} difficulty is set to {difficulties[gamemode]}");
    }
}
