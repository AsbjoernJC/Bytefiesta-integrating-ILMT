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
    private bool hasCollided = false;

    // Start is called before the first frame update

    public static EnemyBullet instance { get; private set; }

    private void Awake() 
    {
        instance = this;    
        
        rB2D = this.GetComponent<Rigidbody2D>();
    }


// Checks the object the enemy's bullet is colliding with
    private void OnTriggerEnter2D(Collider2D collider) 
    {
        string collision = collider.ToString();
        collisionTag = collider.tag;

        // Anything that isn't a player
        if (collision.Contains("KingoftheHill") || collision.Contains(this.name) ||collision.Contains("Cannon"))
            return;

        // A player's head detection is a child of the player prefab therefore we need to get the gameobject of the parent
        if (collision.Contains("HeadDetect"))
            player = collider.transform.parent.gameObject;
        // If the collider still belongs to the player it is directly a part of the player GameObject/prefab
        else
            player = collider.gameObject;
        



        if (collisionTag.Contains("Player") && hasCollided == false)
        {
            // Todo make dynamic
            player.GetComponent<Stats>().TakeDamageAnonomously(1);
            hasCollided = true;
        }
        Destroy(gameObject);
    }

// Creates the bullet in scene and gives it a direction and velocity.
    public static void Shoot(Transform firePoint, GameObject bullet, Quaternion shootingAngle, float bulletSpeed)
    {
        GameObject bulletInstance = Instantiate(bullet, firePoint.transform.position, shootingAngle);
        rB2D.AddForce(firePoint.up * bulletSpeed, ForceMode2D.Impulse);
    }


}
