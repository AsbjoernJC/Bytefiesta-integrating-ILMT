using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    public int health = 1;
    private GameObject player;

    // Update is called once per frame
    void Start()
    {
        player = this.gameObject;
    }

    // Take Damage will be called when a player takes damage eg. getting shot/jumped on etc.
    // If a player's health goes below 0, the player is dead and should therefore be respawned on a timer.
    public void TakeDamage(int damage, string playerWhoDealtDamage)
    {
        health -= damage;
        if (health <= 0)
        {
            KingoftheHillTracker.playerScores[playerWhoDealtDamage] ++;
            // Debug.Log(playerWhoDealtDamage + " score = " + KingoftheHillTracker.playerScores[playerWhoDealtDamage]);

            ScoreUpdater.UpdatePlayerScoreUI(playerWhoDealtDamage);
            LevelInitializer.Instance.PlayerDeathInformation(player);
            return;
        }
        var shieldPoint = player.GetComponent<PlayerController>().shieldPoint;
        var shieldSprite = player.GetComponent<PlayerController>().shieldSprite;
        shieldPoint.sprite = null;
    }

    public void GainHealth(int healthGain)
    {
        if (health >= 2)
            return;
        health += healthGain;
    }
}