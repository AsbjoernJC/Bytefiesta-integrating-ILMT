using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PowerUp0 : MonoBehaviour
{

    // Start is called before the first frame update

    void Start() 
    {

    }
    void Update() 
    {

    }


// Todo: The bullet should not be able to collide with the player who shot the bullet
// Should somehow add the picked up powerup to the player, who it collided with.
    private void OnTriggerEnter2D(Collider2D collider) 
    {
        GameObject Player = collider.gameObject;
        string powerUp = "KingoftheHill0";
        string Collision = collider.ToString();
        Debug.Log(Collision);
        if (Collision.Contains("Player"))
            Player.GetComponent<PlayerController>().GotPowerUp(powerUp);
            Destroy(gameObject);
        // if (!Collision.Contains("Player 1") && !Collision.Contains("KingoftheHill0"))
        //     Destroy(gameObject);
        
    }
}
