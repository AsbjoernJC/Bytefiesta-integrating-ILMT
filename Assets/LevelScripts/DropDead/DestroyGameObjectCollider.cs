using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyGameObjectCollider : MonoBehaviour
{
    private string collisionTag;
    private GameObject player;
    private bool hasCollided = false;

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

        if (collisionTag.Contains("Player") && hasCollided == false)
        {
            player.GetComponent<Stats>().TakeDamageAnonomously(1);
        }

        Destroy(collider.gameObject);
    }
}
