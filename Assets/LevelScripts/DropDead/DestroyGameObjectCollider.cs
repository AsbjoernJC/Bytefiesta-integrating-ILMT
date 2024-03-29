using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyGameObjectCollider : MonoBehaviour
{
    private string collisionTag;
    private GameObject player;

    private void OnTriggerEnter2D(Collider2D collider) 
    {
        string collision = collider.ToString();
        collisionTag = collider.tag;

        // A player's head detection is a child of the player prefab therefore we need to get the gameobject of the parent
        if (collision.Contains("HeadDetect") || collision.Contains("Feet"))
            player = collider.transform.parent.gameObject;
            
        // If the collider still belongs to the player it is directly a part of the player GameObject/prefab
        else
            player = collider.gameObject;
        

        // might be used to only allow a certain amount of FallingPlatform prefabs in the scene
        if (collision.Contains("FallingPlatform"))
        {

        }

        if (collisionTag.Contains("Player") || collision.Contains("Player"))
        {
            player.GetComponent<Stats>().TakeDamageAnonomously(1);
            // We return here as the player object will be destroyed if the player's health goes below 0
            return;
        }

        Destroy(collider.gameObject);
    }
}
