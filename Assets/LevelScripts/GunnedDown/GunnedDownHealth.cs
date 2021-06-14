using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunnedDownHealth : MonoBehaviour
{
    public int minigamePlayerHealth = 3;
    private bool haveIncrementedHealth = false;
    private int incrementHealthCount;

    void Update()
    {

        if (!haveIncrementedHealth)
        {
            IncreaseHealth();
        }

    }

    private void IncreaseHealth ()
    {
        for (int i = 0; i < PlayerConfigurationManager.numberOfActivePlayers; i++)
        {
            var player = GameObject.Find($"Player {i + 1}");
            if (player != null)
            {
                player.GetComponent<Stats>().health = minigamePlayerHealth;
                incrementHealthCount ++;
            }
        }
        if (incrementHealthCount == PlayerConfigurationManager.numberOfActivePlayers)
            haveIncrementedHealth = true;
        
    }

}
