using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class MainMenu : MonoBehaviour
{

    private string elemToModify;

    // Gets rid of all the public static variables that may have been set
    // private void Awake() 
    // {
    //     PlayerConfigurationManager.playerControllers = new Dictionary<int, InputDevice>();
    //     PlayerConfigurationManager.playerControlSchemes = new Dictionary<int, string>();
    //     PlayerConfigurationManager.numberOfActivePlayers = 0;


    //     DifficultyAndScore.playerInputs = new Dictionary<int, PlayerInput>();


    //     foreach (var elem in DifficultyAndScore.difficulties)
    //     {
    //         if (elem.Value == true)
    //         {
    //             elemToModify = elem.Key;
    //         }
    //     }
    //     if (elemToModify != null)
    //         DifficultyAndScore.difficulties[elemToModify] = false;

        
    // }
    // Should create public static class instances and have the public static variables changed to public

    public void HardcorePlayGame ()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        DifficultyAndScore.SetDifficulty("Hardcore");
    }

    public void MediumPlayGame ()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        DifficultyAndScore.SetDifficulty("Medium");
    }

    public void LightPlayGame ()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        DifficultyAndScore.SetDifficulty("Light");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
