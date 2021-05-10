using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    public static int health = 1;
    private GameObject player;

    // Update is called once per frame
    void Start()
    {
        player = this.gameObject;
    }

    void Update() 
    {

    }


    // Take Damage will be called when a player takes damage eg. getting shot/jumped on etc.
    // If a player's health goes below 0, the player is dead and should therefore be respawned on a timer.
    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            // LevelInitializer.Instance.RespawnPlayer(player);
            StartCoroutine(LevelInitializer.Instance.RespawnPlayer(4, player));
        }
    }
}
 