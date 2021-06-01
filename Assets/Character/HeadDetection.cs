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

    private void OnTriggerEnter2D(Collider2D collider) 
    {
        string collision = collider.ToString();
        string colliderName = collision.Split( )[0] + " " + collision.Split( )[1];
        var jumper = collider.GetComponent<Rigidbody2D>();
        var jumperTransform = collider.GetComponent<Transform>();
        if (collision.Contains("HeadDetect"))
            return;

        if (collision.Contains("Player") && collision.Contains("BoxCollider") && jumper.velocity.y < 0)
        {
            player.GetComponent<Stats>().TakeDamage(1, colliderName);
            jumper.AddForce(jumperTransform.up * 20f, ForceMode2D.Impulse);
        }
    }

}
