using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    public static int health = 1;
    public static GameObject player;
    public float deathTimer = 3.5f;
    public float spawnDelay = 0f;
    private GameObject playerReference;

    // Update is called once per frame
    void Start()
    {
        if (playerReference == null)
        {
            playerReference = player;
        }
    }

    void Update() 
    {
        if (health <= 0)
        {
            Death();
        }
    }

    //Get's called when a player has 0 health. Removes the player from the scene and instantiates the player on a delay
    public static void TakeDamage(int damage)
    {
        health = health - damage;
    }

    private void Death()
    {
        Destroy(player);
        InvokeRepeating("Respawn", spawnDelay, deathTimer);
    }

    private void Respawn()
    {
        Instantiate(this.gameObject);
        health = 1;
        CancelInvoke("Respawn");
        // var player;
        // player.GetComponent<PlayerSetupMenuController>().SetPlayerIndex(input.playerIndex);
    }
}
