using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PowerUp0 : MonoBehaviour
{
    private GameObject SpawnedPowerUp;
    // Start is called before the first frame update

    void Start() 
    {
        SpawnedPowerUp = this.gameObject;
    }
    void Update() 
    {

    }


// Todo: The bullet should not be able to collide with the player who shot the bullet
// Should somehow add the picked up powerup to the player, who it collided with.
    private void OnTriggerEnter2D(Collider2D collider) 
    {
        GameObject Player = collider.gameObject;
        string powerUpName = SpawnedPowerUp.name;
        string Collision = collider.ToString();
        if (Collision.Contains("Player") && powerUpName.Contains("KingoftheHill0"))
            Player.GetComponent<PlayerController>().GotBulletPowerUp(powerUpName);
            Destroy(gameObject);
    }
}
