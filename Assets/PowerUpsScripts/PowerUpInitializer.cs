using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class PowerUpInitializer : MonoBehaviour
{
    public float spawnDelay = 4f;
    public float deathTimer = 10f;
    public GameObject[] powerUps; 
    
    private bool powerUpInScene;

    // Update is called once per frame


    void Update()
    {
        FindPowerUps();
    }


// There should only be spawned a powerUp from powerUps if there is no object with the "PowerUp" tag in the scene

// There needs to be a duration between how long before the next power up can be spawned from when it is picked up

// Coroutine tutorial https://youtu.be/5L9ksCs6MbE
// https://stackoverflow.com/questions/30056471/how-to-make-the-script-wait-sleep-in-a-simple-way-in-unity

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
    
    private void SpawnPowerUp()
    {
        if (powerUpInScene == false)
            Instantiate(powerUps[Random.Range(0, powerUps.Length)]);
            powerUpInScene = true;
        if (powerUpInScene == true)
            CancelInvoke("SpawnPowerUp");
    }

}
