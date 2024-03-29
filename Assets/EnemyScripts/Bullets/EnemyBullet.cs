using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyBullet : MonoBehaviour
{
    protected static Rigidbody2D rB2D;
    protected string collisionTag;
    protected GameObject player;
    protected bool hasCollided = false;

    // Start is called before the first frame update


    protected virtual void Awake() 
    {
        rB2D = this.GetComponent<Rigidbody2D>();
    }


// Checks the object the enemy's bullet is colliding with
    protected virtual void OnTriggerEnter2D(Collider2D collider) 
    {
        string collision = collider.ToString();
        collisionTag = collider.tag;

        // Anything that isn't a player
        if (collision.Contains(this.name))
            return;

        // A player's head detection is a child of the player prefab therefore we need to get the gameobject of the parent
        if (collision.Contains("HeadDetect"))
            player = collider.transform.parent.gameObject;
        // If the collider still belongs to the player it is directly a part of the player GameObject/prefab
        else
            player = collider.gameObject;
        



        if (collisionTag.Contains("Player") && hasCollided == false)
        {
            player.GetComponent<Stats>().TakeDamageAnonomously(1);
            hasCollided = true;
        }
        Destroy(gameObject);
    }

// Creates the bullet in scene and gives it a direction and velocity.


}
