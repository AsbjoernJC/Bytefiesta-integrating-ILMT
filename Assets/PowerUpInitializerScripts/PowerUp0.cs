using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PowerUp0 : MonoBehaviour
{
    private GameObject player;
    private GameObject SpawnedPowerUp;
    private GameObject powerupInitializer;
    
    
    // Start is called before the first frame update
    void Start() 
    {
        powerupInitializer = GameObject.Find("PowerUpInitializer");
        SpawnedPowerUp = this.gameObject;
    }



    private void OnTriggerEnter2D(Collider2D collider) 
    {
        string powerUpName = SpawnedPowerUp.name;
        string collision = collider.ToString();

        // We only want player characters to be able to collide with this powerup, as only they
        // are allowed to pick it up

        if (collision.Contains("Bullet(Clone)"))
            return;

        if (collision.Contains("HeadDetect"))
            player = collider.transform.parent.gameObject;
        else
            player = collider.gameObject;

        // A player collides with the powerup "KingoftheHill0" which is a bullet powerup
        if (collision.Contains("Player") && powerUpName.Contains("KingoftheHill0"))
        {
            player.GetComponent<PlayerController>().GotBulletPowerUp();
            powerupInitializer.GetComponent<PowerUpInitializer>().spawnpointOccupation[SpawnedPowerUp.tag] = false;
            powerupInitializer.GetComponent<PowerUpInitializer>().occupiedSpawnpoints --;
            Destroy(gameObject);
            return;
        }

        // A player collides with the powerup "KingoftheHill1" which is a shield powerup
        else if (collision.Contains("Player") && powerUpName.Contains("KingoftheHill1"))
        {
            player.GetComponent<PlayerController>().GotShieldPowerUp();
            powerupInitializer.GetComponent<PowerUpInitializer>().spawnpointOccupation[SpawnedPowerUp.tag] = false;
            powerupInitializer.GetComponent<PowerUpInitializer>().occupiedSpawnpoints --;
            Destroy(gameObject);
            return;
        }
        
    }
}
