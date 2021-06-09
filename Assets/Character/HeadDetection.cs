using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadDetection : MonoBehaviour
{

    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = this.transform.parent.gameObject;
    }

// When something collides with the player's head collider we check if it is another player who is falling: has a velocity.y value < 0
// if it is then the player should take damage and the player who jumped on this player, should be pushed upwards. 
    private void OnTriggerEnter2D(Collider2D collider) 
    {
        string collision = collider.ToString();
        string colliderName = collision.Split( )[0] + " " + collision.Split( )[1];
        var jumper = collider.GetComponent<Rigidbody2D>();
        var jumperTransform = collider.GetComponent<Transform>();
        if (collision.Contains("HeadDetect"))
            return;

        if (collision.Contains("Player") && collision.Contains("BoxCollider") && jumper.velocity.y <= 0)
        {
            player.GetComponent<Stats>().TakeDamage(1, colliderName);

            jumper.velocity = jumperTransform.up * 18f;
        }
    }

}
