using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DifficultyAndScore : MonoBehaviour
{
    public string difficulty;
    public int finishedMinigames = 0;
    public Dictionary<string, bool> difficulties = new Dictionary<string, bool>
    {
        {"Hardcore", false},
        {"Medium", false},
        {"Light", false}
    };    

    public Dictionary<string, int> acrossGamemodePlayerScore = new Dictionary<string, int>()
    {
        {"Player 1", 0},
        {"Player 2", 0},
        {"Player 3", 0},
        {"Player 4", 0}
    };


    // the build indexes of the minigames start at 4, number 4 included.
    // Made these static as the playable minigames will never be changed dynamically
    public static List<int> minigames = new List<int>()
    {
        // 4,
        // 5,
        // 6,
        // 7,
        // 8,
        10
    };

    public static List<int> fourPlayerMinigames = new List<int>()
    {
        9
    };

    // When there is enough minigames unchosenMinigames might just be a list 
    // containing the only games to pick from. For now we will cycle minigames in and out of this list
    public List<int> tailoredMinigames = new List<int>();
    public List<int> unchosenMinigames = new List<int>();
    public Dictionary<int, PlayerInput> playerInputs = new Dictionary<int, PlayerInput>();
    public string gameWinner;
    public List<string> gameWinners = new List<string>();
    public int lastMinigameIndex;
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

        unchosenMinigames.AddRange(minigames);

    }

    public void SetDifficulty(string gamemode)
    {
        // Makes sure that difficulty is not set if players start playing after a player has already won the entire game

        DifficultyAndScore.Instance.difficulty = gamemode;
        DifficultyAndScore.Instance.difficulties[gamemode] = true;
    }
}
