using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyAndScore : MonoBehaviour
{
    public static string difficulty;
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
    public static DifficultyAndScore Instance { get; private set; }
    
    private void Awake()
    {
        Instance = this;
    }

    public static void SetDifficulty(string gamemode)
    {
        difficulty = gamemode;
        difficulties[gamemode] = true;
        Debug.Log($"{gamemode} difficulty is set to {difficulties[gamemode]}");
    }
}
