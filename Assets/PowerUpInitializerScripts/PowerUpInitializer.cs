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

    // There are only 9 spawnpoints. If all are occupied no powerup should be spawned
        if (occupiedSpawnpoints != 9)
            FindPowerUps();
    }


// Searches for players, dead players will not be found
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


// Spawns a powerup every spawnDelay seconds if there is a single spawnpoint that is not occupied
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
    // checks if the spawnpoint is occupied
            if (spawnpointOccupation[$"Spawnpoint {i + 1}"] != true)
            {
                for (int j = 0; j < activePlayers.Count; j++)
            {
                // Finds the distance to the player from the spawnpoint
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


        bestSpawnPoint = assumedSpawnPoint;
        SpawnPowerUp();
        CancelInvoke("FindEligibleSpawnPoint");
    }


// If there are no powerups active in the scene a powerup is randomly spawned from at the time 9 spawnPoints initialized with
// 9 transform prefabs
    private void SpawnPowerUp()
    {
        GameObject powerUp = Instantiate(powerUps[Random.Range(0, powerUps.Length)], spawnPoints[bestSpawnPoint]);
        powerUp.tag = $"Spawnpoint {bestSpawnPoint + 1}";
        spawnpointOccupation[$"Spawnpoint {bestSpawnPoint + 1}"] = true;
        occupiedSpawnpoints ++;
    }

}
