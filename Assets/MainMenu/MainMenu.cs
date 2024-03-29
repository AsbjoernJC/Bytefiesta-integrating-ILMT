using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public void HardcorePlayGame ()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        DifficultyAndScore.Instance.SetDifficulty("Hardcore");
    }


    public void MediumPlayGame ()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        DifficultyAndScore.Instance.SetDifficulty("Medium");
    }


    public void LightPlayGame ()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        DifficultyAndScore.Instance.SetDifficulty("Light");
    }


    public void QuitGame()
    {
        Application.Quit();
    }
}
