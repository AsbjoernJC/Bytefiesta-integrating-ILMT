using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyBullet : MonoBehaviour
{
    private static Rigidbody2D rB2D;
    private Vector3 bulletPosition;
    private string collisionTag;
    private GameObject player;

    // Start is called before the first frame update

    public static EnemyBullet instance { get; private set; }

    private void Awake() 
    {
        instance = this;    
        
        rB2D = this.GetComponent<Rigidbody2D>();
    }

// Checks who the bullet is created by, by looking at the GameObject's tag
// If it is not the player who shot the bullet, or another bullet from the same player
// the bullet will be destroyed. 
    private void OnTriggerEnter2D(Collider2D collider) 
    {
        string collision = collider.ToString();
        collisionTag = collider.tag;

        if (collision.Contains("KingoftheHill") || collision.Contains(this.name) ||collision.Contains("Cannon"))
            return;

        if (collision.Contains("HeadDetect"))
            player = collider.transform.parent.gameObject;
        else
            player = collider.gameObject;
        



        if (collisionTag.Contains("Player"))
        {
            // Todo create a function in Stats script to take damage "anonymously"
            // player.GetComponent<Stats>().TakeDamage(1, playerWhoShot);
        }
        StopAllCoroutines();
        Destroy(gameObject);
    }

// Creates the bullet in scene and gives it a direction and velocity.
    public static void Shoot(Transform firePoint, GameObject bullet, Quaternion shootingAngle, float bulletSpeed)
    {
        GameObject bulletInstance = Instantiate(bullet, firePoint.transform.position, shootingAngle);
        rB2D.AddForce(firePoint.up * bulletSpeed, ForceMode2D.Impulse);
    }


}
