using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class PowerUpInitializer : MonoBehaviour
{
    public float spawnDelay = 7f;
    public float deathTimer = 6f;
    public GameObject[] powerUps; 
    public Transform[] spawnPoints;
    private bool powerUpInScene = false;
    private int initialNumberOfPlayers = 0;
    private int assumedSpawnPoint;
    private int bestSpawnPoint;
    
    public static List<GameObject> activePlayers = new List<GameObject>();
    private List<string> playerNames = new List<string>() 
    {
        "Player 1",
        "Player 2",
        "Player 3",
        "Player 4",
    };

    private Dictionary<string, bool> spawnpointOccupation = new Dictionary<string, bool>
    {
        {"Spawnpoint 1", false},
        {"Spawnpoint 2", false},
        {"Spawnpoint 3", false},
        {"Spawnpoint 4", false},
        {"Spawnpoint 5", false},
        {"Spawnpoint 6", false},
        {"Spawnpoint 7", false},
        {"Spawnpoint 8", false},
        {"Spawnpoint 9", false}
    };



    void Update()
    {
        if (activePlayers.Count == 0)
            FindPlayers();

        FindPowerUps();
    }


// Sometimes throws errors when multiple people have to be respawned.
    private void FindPlayers()
    {
        GameObject player; 
        for (int i = 0; i < playerNames.Count -1; i++)
        {
            if (GameObject.Find(playerNames[i]) != null)
            {
                player = GameObject.Find(playerNames[i]);
                activePlayers.Add(player);
                initialNumberOfPlayers ++;
            }
        }
    }


// Successfully checks if none of the powerups in powerUps are active. If none are active it will call FindEligibleSpawnPoint()
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
            InvokeRepeating("FindEligibleSpawnPoint", spawnDelay, deathTimer);
        }
        else
        {
            powerUpInScene = true;
        }
    }

    private void FindEligibleSpawnPoint()
    {
        // removes all gameObjects (type) elements from the list that are null
        activePlayers.RemoveAll(gO => gO == null);

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
                assumedSpawnPoint = i;
                // todo if (assumedSpawnPoint already contains a powerup then we should break)
                bestDistanceToPlayers = smallestDistanceToPlayer;
            }
            smallestDistanceToPlayer = 50f;
        }
        bestSpawnPoint = assumedSpawnPoint;
        SpawnPowerUp();
        CancelInvoke("FindEligibleSpawnPoint");
    }


// If there are no powerups active in the scene a powerup is randomly spawned from at the time 9 spawnPoints initialized with
// 9 transformer prefabs
    private void SpawnPowerUp()
    {
        Instantiate(powerUps[Random.Range(0, powerUps.Length)], spawnPoints[bestSpawnPoint]);
        spawnpointOccupation[$"Spawnpoint {bestSpawnPoint + 1}"] = true;
        powerUpInScene = true;
    }

}
