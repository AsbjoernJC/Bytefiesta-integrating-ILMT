using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsHeadMash : Stats
{

    public override void TakeDamage(int damage, string playerWhoDealtDamage)
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

            // PlayerDeathInformation also interacts with PowerUpInitializer which is not used in every minigame
            // Furthermore it will try to respawn the player
            levelInitializer.PlayerDeathInformation(player);
            return;
        }

    }

    public override void TakeDamageAnonomously(int damage)
    {
        if (health == 0)
            return;

        health -= damage;

        // If a player's health is equal to 0 or less then the player should die
        if (health <= 0)
        {
            levelInitializer.PlayerDeathInformation(player);
            HeadMashScorePasser.Instance.PlayerVitalStatusOnDeath(player.name);
        }
    }
}
