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
        if (health == 0)
            return;

        health -= damage;

        // If a player's health is equal to 0 or less then the player should die
        if (health <= 0)
        {
            // Should be dynamic ie. dependant on the current minigame. KingofTheHillTracker is used for the KingoftheHill scene/minigame
            KingoftheHillTracker.playerScores[playerWhoDealtDamage] ++;

            //Should be dynamic aswell. Not every scene has a scoreboard.
            ScoreUpdater.UpdatePlayerScoreUI(playerWhoDealtDamage);

            // PlayerDeathInformation also interacts with PowerUpInitializer which is not used in every minigame
            // Furthermore it will try to respawn the player
            LevelInitializer.Instance.PlayerDeathInformation(player);
            if (KingoftheHillTracker.playerScores[playerWhoDealtDamage] >= 5)
            {
                KingoftheHillTracker.MiniGameEnd(playerWhoDealtDamage);
            }
            return;
        }
        // Believe this is redundant as the player GameObject will be destroyed when LevelInitializer.Instance.PlayerDeathInformation is called
        // The player prefabs have their shieldSprite set to null as a standard
        var shieldPoint = player.GetComponent<PlayerController>().shieldPoint;
        var shieldSprite = player.GetComponent<PlayerController>().shieldSprite;
        shieldPoint.sprite = null;
    }

    public void TakeDamageAnonomously(int damage)
    {
        if (health == 0)
            return;

        health -= damage;

        // If a player's health is equal to 0 or less then the player should die
        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    public void GainHealth(int healthGain)
    {
        // Might also depend on different minigames in the future
        if (health >= 2)
            return;
        health += healthGain;
    }
}