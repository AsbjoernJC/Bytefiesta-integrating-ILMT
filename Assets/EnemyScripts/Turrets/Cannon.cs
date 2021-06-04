using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{

    [SerializeField]
    private Transform[] cannonFirepoints;
    [SerializeField]
    private GameObject bulletPrefab;
    private float bulletSpeed = 21f;
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
        while (PlayerConfigurationManager.numberOfActivePlayers != LastManStanding.deadPlayers - 1)
        {
            if (timePassed % 1 == 0)
            {
                chosenCannon = Random.Range(0, 2);
                EnemyBullet.Shoot(cannonFirepoints[chosenCannon], bulletPrefab, shootingAngle, bulletSpeed);
                yield return new WaitForSeconds(1f);
                timePassed += 1f;
            } 
            yield return null;
        }
        yield return null;
    }

}
