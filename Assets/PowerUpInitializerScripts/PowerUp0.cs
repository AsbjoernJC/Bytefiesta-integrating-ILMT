using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PowerUp0 : MonoBehaviour
{
    private GameObject player;
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
// Does not allow bullets spawned by players to collide withthe powerup.
    private void OnTriggerEnter2D(Collider2D collider) 
    {
        string powerUpName = SpawnedPowerUp.name;
        string collision = collider.ToString();
        if (collision.Contains("Bullet(Clone)"))
            return;

        if (collision.Contains("HeadDetect"))
            player = collider.transform.parent.gameObject;
        else
            player = collider.gameObject;

        if (collision.Contains("Player") && powerUpName.Contains("KingoftheHill0"))
            player.GetComponent<PlayerController>().GotBulletPowerUp(powerUpName);
            Destroy(gameObject);
        else if (collision.Contains("Player") && powerUpName.Contains("KingoftheHill1"))
            player.GetComponent<PlayerController>().GotBulletPowerUp(powerUpName);
            Destroy(gameObject);
        
    }
}
