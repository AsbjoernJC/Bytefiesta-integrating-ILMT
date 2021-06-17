using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CannonBullets : EnemyBullet
{

    protected override void OnTriggerEnter2D(Collider2D collider) 
    {
        string collision = collider.ToString();
        collisionTag = collider.tag;

        // Anything that isn't a player
        if (collision.Contains("KingoftheHill") || collision.Contains(this.name) ||collision.Contains("Cannon"))
            return;

        // A player's head detection is a child of the player prefab therefore we need to get the gameobject of the parent
        if (collision.Contains("HeadDetect"))
            player = collider.transform.parent.gameObject;
        // If the collider still belongs to the player it is directly a part of the player GameObject/prefab
        else
            player = collider.gameObject;
        



        if (collisionTag.Contains("Player") && hasCollided == false)
        {
            player.GetComponent<Stats>().TakeDamageAnonomously(1);
            string playerIndex = player.name.Split( )[1];
            int playerHealth = player.GetComponent<Stats>().health;
            HealthUIUpdater.instance.ChangePlayerText(Int32.Parse(playerIndex), playerHealth);
            hasCollided = true;
        }
        Destroy(gameObject);
    }

    public static void Shoot(Transform firePoint, GameObject bullet, Quaternion shootingAngle, float bulletSpeed)
    {
        GameObject bulletInstance = Instantiate(bullet, firePoint.transform.position, shootingAngle);
        rB2D.AddForce(firePoint.up * bulletSpeed, ForceMode2D.Impulse);
    }

}
