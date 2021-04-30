using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class PowerUpInitializer : MonoBehaviour
{
    public float duration = 4f;
    public GameObject[] powerUps; 

    // Update is called once per frame


    void Update()
    {
        SpawnPowerUp(powerUps[Random.Range(0, powerUps.Length)]);
    }


// There should only be spawned a powerUp from powerUps if there is no object with the "PowerUp" tag in the scene

// There needs to be a duration between how long before the next power up can be spawned from when it is picked up

// Coroutine tutorial https://youtu.be/5L9ksCs6MbE
    private void SpawnPowerUp(GameObject powerUp) 
    {
        Instantiate(powerUp);
    }
}
