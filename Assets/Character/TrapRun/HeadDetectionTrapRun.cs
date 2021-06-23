using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadDetectionTrapRun : HeadDetection
{


    protected override void OnTriggerEnter2D(Collider2D collider) 
    {
        string collision = collider.ToString();
        string colliderName = collision.Split( )[0] + " " + collision.Split( )[1];

        if (collision.Contains("HeadDetect"))
            return;

        Rigidbody2D jumper;
        if (collision.Contains("Player") && collision.Contains("Feet"))
        {
            jumper = collider.transform.parent.GetComponent<Rigidbody2D>();

            // For a player to take from a person jumping on their head the jumper will have to be in a falling motion or still in the air
            if (jumper.velocity.y <= 0)
            {
                player.GetComponent<Stats>().TakeDamage(1, colliderName);
            }

            return;
        }

        jumper = collider.GetComponent<Rigidbody2D>();


        if (collision.Contains("Player") && collision.Contains("BoxCollider") && jumper.velocity.y <= 0)
        {
            player.GetComponent<Stats>().TakeDamage(1, colliderName);
        }
    }
}
