using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class PowerUpInitializer : MonoBehaviour
{
    public float spawnDelay = 8f;
    public float deathTimer = 6f;
    public GameObject[] powerUps; 
    public Transform[] spawnPoints;
    private bool powerUpInScene;
    
    List<GameObject> activePlayers = new List<GameObject>();
    List<string> playerNames = new List<string>() {
        "Player 1",
        "Player 2",
        "Player 3",
        "Player 4",
    };

    private int bestSpawnPoint;


    void Start()
    {

    }

    void Update()
    {
        if (activePlayers.Count == 0)
            FindPlayers();

        FindPowerUps();
    }

    private void FindPlayers()
    {
        GameObject player; 
        for (int i = 0; i < playerNames.Count -1; i++)
        {
            if (GameObject.Find(playerNames[i]) != null)
            {
                player = GameObject.Find(playerNames[i]);
                activePlayers.Add(player);
            }
        }
    }





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
            // InvokeRepeating("SpawnPowerUp", spawnDelay, deathTimer);
            InvokeRepeating("FindEligibleSpawnPoint", spawnDelay, deathTimer);
        }
        else
        {
            powerUpInScene = true;
        }
    }
    

    // This is working fine for now, however, it does not consider the fact that you can go through the sides nor bottom/top of the map
    // Could consider finding the transform position where the player with the smallest distance have to travel the longest distance.
    // As of now this would encourage players staying a large distance from each other to gain powerups.
    // This should probably be a coroutine. Right now it will run all the time as soon as the powerUpInScene is false.
    // Which it is as soon as the powerup is picked up.
    private void FindEligibleSpawnPoint()
    {
        float bestDistanceToPlayers = 0;
        float smallestDistanceToPlayer = 50f;
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            for (int j = 0; j < activePlayers.Count; j++)
            {
                float distanceToPlayer = Vector3.Distance(spawnPoints[i].transform.position, activePlayers[j].transform.position);
                if (distanceToPlayer < smallestDistanceToPlayer)
                    smallestDistanceToPlayer = distanceToPlayer;
            }
            if (smallestDistanceToPlayer > bestDistanceToPlayers)
            {
                bestSpawnPoint = i;
                bestDistanceToPlayers = smallestDistanceToPlayer;
            }
            smallestDistanceToPlayer = 50f;
        }
        SpawnPowerUp();
        CancelInvoke("FindEligibleSpawnPoint");
    }


// If there are no powerups active in the scene a powerup is randomly spawned from at the time 9 spawnPoints initialized with
// 9 transformer prefabs
    private void SpawnPowerUp()
    {
        Instantiate(powerUps[Random.Range(0, powerUps.Length)], spawnPoints[bestSpawnPoint]);
        powerUpInScene = true;
        CancelInvoke("SpawnPowerUp");
    }

}
