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


// There should only be spawned a powerUp from powerUps if there is no object with the "PowerUp" tag in the scene

// There needs to be a duration between how long before the next power up can be spawned from when it is picked up

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

    private void SpawnPowerUp()
    {
        if (powerUpInScene == false)
            Instantiate(powerUps[Random.Range(0, powerUps.Length)], spawnPoints[Random.Range(0, spawnPoints.Length)]);
            powerUpInScene = true;
        if (powerUpInScene == true)
            CancelInvoke("SpawnPowerUp");
    }

}
