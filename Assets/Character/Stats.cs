using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    public int health = 1;
    protected GameObject player;
    protected LevelInitializer levelInitializer;


    // Update is called once per frame
    protected virtual void Start()
    {
        levelInitializer = GameObject.Find("LevelInitializer").GetComponent<LevelInitializer>();
        player = this.gameObject;
    }

    // Take Damage will be called when a player takes damage eg. getting shot/jumped on etc.
    // If a player's health goes below 0, the player is dead and should therefore be respawned on a timer.
    public virtual void TakeDamage(int damage, string playerWhoDealtDamage)
    {
        // If the player's health is zero when this function is called
        // it must have already taken account for the player's death
        if (health == 0)
            return;

        // as the player's health is decremented here
        health -= damage;

        // If a player's health is equal to 0 or less then the player should die
        if (health <= 0)
        {
            // Should be dynamic ie. dependant on the current minigame. KingofTheHillTracker is used for the KingoftheHill scene/minigame
            PointMinigameTracker.instance.playerScores[playerWhoDealtDamage] ++;

            //Should be dynamic aswell. Not every scene has a scoreboard.
            ScoreUpdater.Instance.UpdatePlayerScoreUI(playerWhoDealtDamage);

            // PlayerDeathInformation also interacts with PowerUpInitializer which is not used in every minigame
            // Furthermore it will try to respawn the player
            levelInitializer.PlayerDeathInformation(player);
            if (PointMinigameTracker.instance.playerScores[playerWhoDealtDamage] >= 5)
            {
                PointMinigameTracker.instance.MiniGameEnd(playerWhoDealtDamage);
            }
            return;
        }
        // Believe this is redundant as the player GameObject will be destroyed when LevelInitializer.Instance.PlayerDeathInformation is called
        // The player prefabs have their shieldSprite set to null as a standard
        var shieldPoint = player.GetComponent<PlayerController>().shieldPoint;
        var shieldSprite = player.GetComponent<PlayerController>().shieldSprite;
        shieldPoint.sprite = null;
    }

    public virtual void TakeDamageAnonomously(int damage)
    {
        if (health == 0)
            return;

        health -= damage;

        // If a player's health is equal to 0 or less then the player should die
        if (health <= 0)
        {
            levelInitializer.PlayerDeathInformation(player);
        }
    }

    public virtual void GainHealth(int healthGain)
    {
        // Might also depend on different minigames in the future
        if (health >= 2)
            return;
        health += healthGain;
    }
}