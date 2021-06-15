using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class PowerUpInitializer : MonoBehaviour
{
    public float spawnDelay = 7f;
    public float deathTimer = 6f;
    public GameObject[] powerUps; 
    public Transform[] spawnPoints;
    public int occupiedSpawnpoints = 0;
    private int initialNumberOfPlayers = 0;
    private int assumedSpawnPoint;
    private int bestSpawnPoint;
    
    public List<GameObject> activePlayers = new List<GameObject>();
    private List<string> playerNames = new List<string>() 
    {
        "Player 1",
        "Player 2",
        "Player 3",
        "Player 4",
    };

    public Dictionary<string, bool> spawnpointOccupation = new Dictionary<string, bool>
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

        if (occupiedSpawnpoints != 9)
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
        InvokeRepeating("FindEligibleSpawnPoint", spawnDelay, deathTimer);
    }

    private void FindEligibleSpawnPoint()
    {
        // removes all gameObjects (type) elements from the list that are null.
        // the gameObjects would be null if a player has just died
        activePlayers.RemoveAll(gO => gO == null);

        float bestDistanceToPlayers = 0;
        float smallestDistanceToPlayer = 50f;
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            if (spawnpointOccupation[$"Spawnpoint {i + 1}"] != true)
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
                bestDistanceToPlayers = smallestDistanceToPlayer;
            }
            smallestDistanceToPlayer = 50f;
            }
        }

        // There are only 9 spawnpoints. If all are full no powerup should be spawned
        if (occupiedSpawnpoints == 9)
            return;

        bestSpawnPoint = assumedSpawnPoint;
        SpawnPowerUp();
        CancelInvoke("FindEligibleSpawnPoint");
    }


// If there are no powerups active in the scene a powerup is randomly spawned from at the time 9 spawnPoints initialized with
// 9 transformer prefabs
    private void SpawnPowerUp()
    {
        GameObject powerUp = Instantiate(powerUps[Random.Range(0, powerUps.Length)], spawnPoints[bestSpawnPoint]);
        powerUp.tag = $"Spawnpoint {bestSpawnPoint + 1}";
        spawnpointOccupation[$"Spawnpoint {bestSpawnPoint + 1}"] = true;
        occupiedSpawnpoints ++;
    }

}
