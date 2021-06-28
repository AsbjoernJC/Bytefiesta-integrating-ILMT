using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatformManager : MonoBehaviour
{

    // element 0.x = -10, element 1.x = -5, element 2.x = 0 etc.
    [SerializeField] private Transform[] platformSpawnpoints;
    [SerializeField] private GameObject fallingPlatformPrefab;

    [SerializeField] public GameObject startingPlatform;

    private bool startedCountdown = false;

    private int lastChosenSpawnpoint;
    private int spawnCounter;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale != 0 && !startedCountdown)
        {
            StartCoroutine("Countdown");
            startedCountdown = true;
        }
    }


    private IEnumerator Countdown()
    {
        StartCoroutine("ChooseSpawnpoint");
        yield return new WaitForSeconds(4);
        Destroy(startingPlatform);
    }


    private IEnumerator ChooseSpawnpoint()
    {
        int chosenSpawnpoint;

        if (spawnCounter == 0)
        {
            chosenSpawnpoint = Random.Range(0, 5);
            lastChosenSpawnpoint = chosenSpawnpoint;
            spawnCounter ++;
            SpawnPlatform(chosenSpawnpoint, 3);
            yield return new WaitForSeconds(2);
        }


        while (LastManStanding.instance.deadPlayers < PlayerConfigurationManager.Instance.numberOfActivePlayers -1)
        {
            switch (lastChosenSpawnpoint)
            {
        // The largest possible horizontal reach of a player is eg. from platformSpawnpoints[0] to platformSpawnpoints[3]
                case 0:
                // might have to change if players will be allowed to through the sides of the screen
                    chosenSpawnpoint = Random.Range(0, 4);
                    lastChosenSpawnpoint = chosenSpawnpoint;
                    SpawnPlatform(chosenSpawnpoint, 3f);
                    spawnCounter ++;
                    yield return new WaitForSeconds(2);
                    break;
                case 1:      
                    chosenSpawnpoint = Random.Range(0, 5);
                    lastChosenSpawnpoint = chosenSpawnpoint;
                    SpawnPlatform(chosenSpawnpoint, 3f);
                    spawnCounter ++;
                    yield return new WaitForSeconds(2);
                    break;
                case 2:
                    chosenSpawnpoint = Random.Range(0, 5);
                    lastChosenSpawnpoint = chosenSpawnpoint;
                    SpawnPlatform(chosenSpawnpoint, 3f);
                    spawnCounter ++;
                    yield return new WaitForSeconds(2);
                    break;
                case 3:
                    chosenSpawnpoint = Random.Range(0, 5);
                    lastChosenSpawnpoint = chosenSpawnpoint;
                    SpawnPlatform(chosenSpawnpoint, 3f);
                    spawnCounter ++;
                    yield return new WaitForSeconds(2);
                    break;
                case 4:
                    chosenSpawnpoint = Random.Range(1, 5);
                    lastChosenSpawnpoint = chosenSpawnpoint;
                    SpawnPlatform(chosenSpawnpoint, 3f);
                    spawnCounter ++;
                    yield return new WaitForSeconds(2);
                    break;
            }
        }
        // for testing:
        // while (true)
        // {

        //     switch (lastChosenSpawnpoint)
        //     {
        //         case 0:
        //         // might have to change if players will be allowed to through the sides of the screen
        //             chosenSpawnpoint = Random.Range(0, 4);
        //             lastChosenSpawnpoint = chosenSpawnpoint;
        //             SpawnPlatform(chosenSpawnpoint, 3f);
        //             spawnCounter ++;
        //             yield return new WaitForSeconds(2);
        //             break;
        //         case 1:      
        //             chosenSpawnpoint = Random.Range(0, 5);
        //             lastChosenSpawnpoint = chosenSpawnpoint;
        //             SpawnPlatform(chosenSpawnpoint, 3f);
        //             spawnCounter ++;
        //             yield return new WaitForSeconds(2);
        //             break;
        //         case 2:
        //             chosenSpawnpoint = Random.Range(0, 5);
        //             lastChosenSpawnpoint = chosenSpawnpoint;
        //             SpawnPlatform(chosenSpawnpoint, 3f);
        //             spawnCounter ++;
        //             yield return new WaitForSeconds(2);
        //             break;
        //         case 3:
        //             chosenSpawnpoint = Random.Range(0, 5);
        //             lastChosenSpawnpoint = chosenSpawnpoint;
        //             SpawnPlatform(chosenSpawnpoint, 3f);
        //             spawnCounter ++;
        //             yield return new WaitForSeconds(2);
        //             break;
        //         case 4:
        //             chosenSpawnpoint = Random.Range(1, 5);
        //             lastChosenSpawnpoint = chosenSpawnpoint;
        //             SpawnPlatform(chosenSpawnpoint, 3f);
        //             spawnCounter ++;
        //             yield return new WaitForSeconds(2);
        //             break;
        //     }

        // }
    }

    private void SpawnPlatform(int chosenSpawnpoint, float fallSpeed)
    {
        GameObject fallingPlatform = Instantiate(fallingPlatformPrefab, platformSpawnpoints[chosenSpawnpoint]);
        fallingPlatform.GetComponent<FallingPlatform>().StartFalling(3);
    }
}
