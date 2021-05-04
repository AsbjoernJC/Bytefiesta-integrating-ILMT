using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class PowerUpInitializer : MonoBehaviour
{
    public float spawnDelay = 4f;
    public float deathTimer = 10f;
    public GameObject[] powerUps; 
    public Transform[] spawnPoints;
    private bool powerUpInScene;
    
    // List<GameObject> activePlayers = new List<GameObject>();
    // List<string> playerNames = new List<string>() {
    //     "Player 1",
    //     "Player 2",
    //     "Player 3",
    //     "Player 4",
    // };


    // void Start()
    // {
    //     if (activePlayers != null)
    //         FindPlayers();
    // }

    void Update()
    {
        FindPowerUps();
    }

    // private void FindPlayers()
    // {
    //     GameObject player; 
    //     for (int i = 0; i < playerNames.Count -1; i++)
    //     {
    //         if (GameObject.Find(playerNames[i]) != null)
    //         {
    //             player = GameObject.Find(playerNames[i]);
    //             activePlayers.Add(player);
    //         }
    //     }
    // }





// Successfully checks if none of the powerups in powerUps are active. If none are active it will call SpawnPowerUp()
// This is done accordingly to the spawnDelay (the point in time where the scene is first initialized + spawnDelay float)
// The next powerup is spawned after waiting on the deathTimer, which is started from the point when no powerup is found in the scene 
    private void FindPowerUps()
    {
        int inactivePowerUps = 0;
        for (int i = 0; i < powerUps.Length; i++)
        {
            if (GameObject.Find(powerUps[i].name + "(Clone)") == null)
            {
                inactivePowerUps ++;
            }
        }
        if (inactivePowerUps == powerUps.Length)
        {
            powerUpInScene = false;
            InvokeRepeating("SpawnPowerUp", spawnDelay, deathTimer);
        }
        else
        {
            powerUpInScene = true;
        }
    }
    
    // private void FindEligibleSpawnPoint()
    // {

    // }


// If there are no powerups active in the scene a powerup is randomly spawned from at the time 9 spawnPoints initialized with
// 9 transformer prefabs
    private void SpawnPowerUp()
    {
        if (powerUpInScene == false)
        Instantiate(powerUps[Random.Range(0, powerUps.Length)], spawnPoints[Random.Range(0, spawnPoints.Length)]);
        powerUpInScene = true;
        if (powerUpInScene == true)
            CancelInvoke("SpawnPowerUp");

        // Turns out the if statements were not redundant. Probably due to me having removed CancelInvoke
    }

}
