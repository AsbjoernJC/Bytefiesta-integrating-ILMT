using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonTrapRun : MonoBehaviour
{

    [SerializeField]
    private Transform[] cannonFirepoints;
    [SerializeField]
    private GameObject bulletPrefab;
    
    // Will fire a bullet every spawnRate seconds
    [SerializeField] public float spawnRate = 1.5f;
    [SerializeField] public float spawnDelay = 1f;
    public float bulletSpeed = 8f;
    private Quaternion shootingAngle;
    private bool delayStarted = false;
    private Quaternion cannonOrientation;


    // Start is called before the first frame update
    private void Start()
    {
        cannonOrientation = this.transform.rotation;
    }

    private void Update()
    {
        if (Time.timeScale != 0 && !delayStarted)
            StartCoroutine("BulletSpawnDelay");
    }


    private IEnumerator BulletSpawnDelay()
    {
        delayStarted = true;
        yield return new WaitForSeconds(spawnDelay);

        StartCoroutine("SpawnCannonball");
    }


    private IEnumerator SpawnCannonball()
    {
        shootingAngle.eulerAngles = new Vector3(0f, 0f, 0f);
        int chosenCannon;

// We want the cannon to keep shooting until the minigame is finished
        while (true)
        {
            chosenCannon = Random.Range(0, cannonFirepoints.Length);
            CannonBulletTrapRun.Shoot(cannonFirepoints[chosenCannon], bulletPrefab, shootingAngle, bulletSpeed, cannonOrientation);
            yield return new WaitForSeconds(spawnRate);
        }
    }
}
