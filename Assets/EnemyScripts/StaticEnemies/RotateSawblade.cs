using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateSawblade : MonoBehaviour
{
    // Todo: Should make a base class that collides with players and deals damage. This should derive from that class
    // rotationSpeed is in units of angle (degrees) / second meaning the sawblade will have rotated 720degrees after 1 second
    [SerializeField] public float rotationSpeed = 720f;
    private Transform sawBlade;

    private string collisionTag;
    private GameObject player;
    private bool hasCollided = false;

    // Start is called before the first frame update
    void Start()
    {
        sawBlade = this.transform;
    }


    // Update is called once per frame
    void Update()
    {
        sawBlade.transform.RotateAround(sawBlade.transform.position, new Vector3(0f, 0f, 1f), rotationSpeed * Time.deltaTime);      
    }

    private void OnTriggerEnter2D(Collider2D collider) 
    {
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
        



        if (collisionTag.Contains("Player") && hasCollided == false)
        {
            player.GetComponent<Stats>().TakeDamageAnonomously(1);
        }
    }
}
