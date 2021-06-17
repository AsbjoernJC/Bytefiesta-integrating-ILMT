using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Cannon : MonoBehaviour
{

    [SerializeField]
    private Transform[] cannonFirepoints;
    [SerializeField]
    private GameObject bulletPrefab;
    private float bulletSpeed = 21f;
    private float x = 1f;
    private Quaternion shootingAngle;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("SpawnCannonball");
    }


    private IEnumerator SpawnCannonball()
    {
        shootingAngle.eulerAngles = new Vector3(0f, 0f, 0f);
        float timePassed = 1f;
        int chosenCannon;

    // Todo spawn bullets more often over time
    // Will now operate in a big while loop that is running whilst there are more than 1 player left
        while (PlayerConfigurationManager.Instance.numberOfActivePlayers != LastManStanding.deadPlayers - 1)
        {
            // if (timePassed % 1 == 0)
            // {
            chosenCannon = Random.Range(0, 2);
            CannonBullets.Shoot(cannonFirepoints[chosenCannon], bulletPrefab, shootingAngle, bulletSpeed);
            yield return new WaitForSeconds(CannonballSpawnRate());
            timePassed += 1f;
            // } 
            yield return null;
        }
        yield return null;
    }

// Will return a float that defines the rate at which the cannons should shoot a cannonball
    private float CannonballSpawnRate()
    {
        // Using a power function
        float resultSpawnRate = 1.5f * Mathf.Pow(x, -2.3f/6f);
        x += 1;
        return resultSpawnRate;
    }

    // Might introduce a function that will choose which turret to send the cannonball out of
    // based on a weight, that will be based on which turret the last few cannonballs were send out of

}
