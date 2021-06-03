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
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("SpawnCannonball");
    }


    private IEnumerator SpawnCannonball()
    {
        float timePassed = 0;
        int chosenCannon;

        while ((int)timePassed <= 6)
        {
            if ((int)timePassed % 1 == 0)
            {
                chosenCannon = Random.Range(0, 1);
                EnemyBullets.Shoot(cannonFirepoints[chosenCannon], bulletPrefab, cannonFirepoints[chosenCannon].rotation ,bulletSpeed);
            }
            timePassed = Time.deltaTime;
        }
        yield return null;
    }

}
