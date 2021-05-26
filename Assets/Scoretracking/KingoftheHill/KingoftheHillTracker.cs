using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class KingoftheHillTracker : MonoBehaviour
{
    [SerializeField]
    private Canvas minigameEndImagery;
    [SerializeField]
    public Sprite[] playerSprites;
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

    public static void MiniGameEnd(string playerWhoWon)
    {
        Time.timeScale = 0f;
        Debug.Log("Minigame ended");
        instance.StartCoroutine("DisplayWinTimer");
        // If a player has a score equal to or higher than 5.
        // Here we should load a scene that displays the amount of sips a player should drink
        // After displaying that and or mystery shot it should display the amount of minigame wins a player has
        // SceneManager.LoadScene("KingoftheHill2304");
        DifficultyAndScore.finishedMinigames ++;
    }

    private IEnumerator DisplayWinTimer()
    {
        minigameEndImagery.gameObject.SetActive(true);
        yield return new WaitForSecondsRealtime(3.5f);
        Debug.Log("Waited 3.5 seconds");
        // To do: load some scene that displays how many sips a player should drink
    }

}

