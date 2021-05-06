using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    public static int health = 1;
    private GameObject player;
    public float deathTimer = 3.5f;
    public float spawnDelay = 0f;
    private GameObject playerReference;

    // Update is called once per frame
    void Start()
    {
        if (player == null)
        {
            player = this.gameObject;
        }
    }

    void Update() 
    {

    }


    // Take Damage will be called when a player takes damage eg. getting shot/jumped on etc.
    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Death();
        }
    }

    private void Death()
    {
        InvokeRepeating("Respawn", spawnDelay, deathTimer);
    }

    // Should remove the player object and then have it respawned
    // Maybe it is better to just let the player object become invisible
    // and turn off its rigidbody + box collider
    private void Respawn()
    {
        Destroy(player);
        Instantiate(player);
        CancelInvoke("Respawn");
        // var player;
        // player.GetComponent<PlayerSetupMenuController>().SetPlayerIndex(input.playerIndex);
    }
}
