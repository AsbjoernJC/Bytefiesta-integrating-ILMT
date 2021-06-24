using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBulletTrapRun : EnemyBullet
{


    protected override void OnTriggerEnter2D(Collider2D collider) 
    {
        string collision = collider.ToString();
        collisionTag = collider.tag;

        // Anything that isn't a player but not walls and
        if (collision.Contains("KingoftheHill") || collision.Contains(this.name) ||collision.Contains("Cannon") ||
        collision.Contains("SpinningAxe"))
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
            hasCollided = true;
        }
        Destroy(gameObject);
    }

    public static void Shoot(Transform firePoint, GameObject bullet, Quaternion shootingAngle, float bulletSpeed, Quaternion cannonOrientation)
    {
        GameObject bulletInstance = Instantiate(bullet, firePoint.transform.position, shootingAngle);
        bulletInstance.transform.rotation = cannonOrientation;
        rB2D.AddForce(firePoint.up * bulletSpeed, ForceMode2D.Impulse);
    }
}
