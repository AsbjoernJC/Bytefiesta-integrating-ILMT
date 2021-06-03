using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyBullets : MonoBehaviour
{
    private Rigidbody2D rB2D;
    private Vector3 bulletPosition;
    private string playerWhoShot;
    private string collisionTag;
    private static string bulletTag;
    private GameObject player;
    // Start is called before the first frame update

    public static EnemyBullets instance { get; private set; }

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
        playerWhoShot = bulletTag.Split( )[0] + " " + bulletTag.Split( )[1];

        if (collision.Contains("KingoftheHill") || playerWhoShot == collisionTag || bulletTag == collisionTag)
            return;

        if (collision.Contains("HeadDetect"))
            player = collider.transform.parent.gameObject;
        else
            player = collider.gameObject;
        



        if (!collision.Contains(playerWhoShot) && bulletTag != collisionTag)
        {
            if (collisionTag.Contains("Player"))
            {
                player.GetComponent<Stats>().TakeDamage(1, playerWhoShot);
            }
            StopAllCoroutines();
            Destroy(gameObject);
        }

    }

// Stops bullets from leaving the scene/arena, however, this is quite intensive.
    private Vector3 OutOfBounds()
    {
        if(transform.position.x >= 31.77)
        {
            bulletPosition = new Vector3(-transform.position.x + 0.1f, transform.position.y);
            return bulletPosition;
        }
        else if (transform.position.x <= -31.77)
        {
            bulletPosition = new Vector3(-transform.position.x - 0.1f, transform.position.y);
            return bulletPosition;
        }
        else if (transform.position.y >= 17.75)
        {
            bulletPosition = new Vector3(transform.position.x, -17.65f);
            return bulletPosition;
        }
        else if (transform.position.y <= -17.75)
        {
            bulletPosition = new Vector3(transform.position.x, 17.65f);
            return bulletPosition;
        }
        bulletPosition = new Vector3(0f, 0f);
        return bulletPosition;
    }

// Todo: should check if the bullet is the powerup form or just the normal.
// If it is the normal bullet the "lifespan" should be shortened via a coroutine

//  The shootingAngle is wrong
    public static void Shoot(Transform firePoint, GameObject bullet, Quaternion shootingAngle, float bulletSpeed)
    {
        GameObject bulletInstance = Instantiate(bullet, firePoint.transform.position, shootingAngle);
        bullet.tag = "Enemy bullet";
        bulletTag = instance.tag;
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * bulletSpeed, ForceMode2D.Impulse);
    }


}
