using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour
{
    // Start is called before the first frame update
    protected string collisionTag;
    protected GameObject player;
    protected bool hasCollided = false;

    protected virtual void OnTriggerEnter2D(Collider2D collider) 
    {
        // hasCollided is only set to true when a player has touched the finishline and the end of the minigame has been called
        // therefore we don't need to check for other collisions at this point in time.
        if (hasCollided)
            return;

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
        



        if (collisionTag.Contains("Player"))
        {
            RaceTracker.instance.playerScores[player.name] = 1;
            RaceTracker.instance.MiniGameEnd(player.name);
            hasCollided = true;
        }
    }
}
