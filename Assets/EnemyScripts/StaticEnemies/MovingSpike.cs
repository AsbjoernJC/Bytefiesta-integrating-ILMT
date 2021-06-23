using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingSpike : MonoBehaviour
{
    // Todo: Should make a base class that collides with players and deals damage. This should derive from that class
    private string collisionTag;
    private GameObject player;
    private bool hasCollided = false;

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D collider) 
    {
        string collision = collider.ToString();
        collisionTag = collider.tag;

        // Anything that isn't a player
        if (collision.Contains("KingoftheHill") || collision.Contains(this.name) || collision.Contains("Cannon") || collision.Contains("Composite"))
            return;

        // A player's head detection is a child of the player prefab therefore we need to get the gameobject of the parent
        if (collision.Contains("HeadDetect") || collision.Contains("Feet"))
            player = collider.transform.parent.gameObject;
            
        // If the collider still belongs to the player it is directly a part of the player GameObject/prefab
        else
            player = collider.gameObject;
        



        if (collisionTag.Contains("Player") && hasCollided == false)
        {
            player.GetComponent<Stats>().TakeDamageAnonomously(1);
        }
    }
}
