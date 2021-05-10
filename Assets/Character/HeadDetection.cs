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
        if (collision.Contains("HeadDetect"))
            return;

        if (collision.Contains("Player") && collision.Contains("BoxCollider"))
            player.GetComponent<Stats>().TakeDamage(1);
    }

}
