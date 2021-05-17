using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class KingoftheHillTracker : MonoBehaviour
{
    public static Dictionary<string, int> playerScores = new Dictionary<string, int>()
    {
        {"Player 1", 0},
        {"Player 2", 0},
        {"Player 3", 0},
        {"Player 4", 0}
    };

    public static KingoftheHillTracker instance { get; private set; }
    // Start is called before the first frame update
    void Awake()
    {
        if (instance != null)
            Debug.Log("Singleton, tried to create another object");
        else
            instance = this;
    }

    public void MiniGameEnd()
    {
        // Here we should load a scene that displays the amount of sips a player should drink
        // After displaying that and or mystery shot it should display the amount of minigame wins a player has
        // SceneManager.LoadScene("KingoftheHill2304");
    }

}
