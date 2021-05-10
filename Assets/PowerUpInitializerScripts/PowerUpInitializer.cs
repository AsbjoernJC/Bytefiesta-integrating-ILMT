using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class PowerUpInitializer : MonoBehaviour
{
    public float spawnDelay = 7f;
    public float deathTimer = 6f;
    public GameObject[] powerUps; 
    public Transform[] spawnPoints;
    private bool powerUpInScene;
    private int initialNumberOfPlayers = 0;
    private bool firstInitialization = true;
    
    public static List<GameObject> activePlayers = new List<GameObject>();
    List<string> playerNames = new List<string>() 
    {
        "Player 1",
        "Player 2",
        "Player 3",
        "Player 4",
    };

    private int bestSpawnPoint;


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
        Debug.Log(activePlayers + " beg");
        for (int i = 0; i < playerNames.Count -1; i++)
        {
            if (GameObject.Find(playerNames[i]) != null)
            {
                player = GameObject.Find(playerNames[i]);
                // if (firstInitialization)
                // {
                activePlayers.Add(player);
                initialNumberOfPlayers ++;
                // }
                // else 
                // {
                //     if (!activePlayers.Contains(player))
                //         activePlayers.Insert(i, player);
                // }
            }
        }
        firstInitialization = false;
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
            // ArePlayersAlive();
        }
        else
        {
            powerUpInScene = true;
        }
    }
    
    // private void ArePlayersAlive()
    // {
    //     activePlayers.RemoveAll(item => item == null);
    //     if (activePlayers.Count < initialNumberOfPlayers)
    //     {
    //         FindPlayers();
    //     }

    //     InvokeRepeating("FindEligibleSpawnPoint", spawnDelay, deathTimer);
    // }

// Iterates through the spawnPoint elements to find the best suited one by iterating through the active players' location
// and choosing the location based on the player who would be the closest to this spawnpoint.
// The spawnpoint index is then stored in bestSpawnPoint if the distance to the closest player is larger than the closest player from
// the spawnpoint chosen before.
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
    }

}
