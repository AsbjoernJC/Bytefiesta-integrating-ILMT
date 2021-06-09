using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BulletBase : MonoBehaviour
{

    private Rigidbody2D rB2D;
    private Vector3 bulletPosition;
    private string playerWhoShot;
    private string collisionTag;
    private static string bulletTag;
    private GameObject player;
    // Start is called before the first frame update
    private static GameObject potentialPlayerTarget;
    private static GameObject targetedPlayer;
    private bool isPowerUpBullet = false;

    private void Awake() 
    {
        rB2D = this.GetComponent<Rigidbody2D>();

    }
    void Update() 
    {

        if (OutOfBounds() != new Vector3(0,0))
        {
            StopAllCoroutines();
            Destroy(gameObject);
        }
        if (isPowerUpBullet)
        {
            StartCoroutine(FindPlayerPositions());
        }

    }

    private void SpawnBullet(Transform firePoint, GameObject powerUp, Quaternion shootingAngle, string playerName, bool powerUpBullet)
    {
        float bulletSpeed = 21f;
        GameObject bullet = Instantiate(powerUp, firePoint.transform.position, shootingAngle);
        bullet.tag = playerName + " bullet";
        bulletTag = this.tag;
        rB2D.AddForce(firePoint.up * bulletSpeed, ForceMode2D.Impulse);
        
        StartCoroutine("FindPlayerPositions");
    }

    private IEnumerator BulletLifeSpan()
    {
        yield return new WaitForSeconds(0.4f);
        Destroy(this.gameObject);
    }

    private IEnumerator FindPlayerPositions()
    {
        float distanceToPlayer;
        float smallestDistanceToPlayer = 100f;
        string playerWhoShot = bulletTag.Split( )[0] + " " + bulletTag.Split( )[1];

        for (int i = 0; i < PlayerConfigurationManager.numberOfActivePlayers; i++)
            {
            LoopStart:
                if (playerWhoShot == $"Player {i + 1}")
                {
                    i++;
                    goto LoopStart;
                }

                potentialPlayerTarget = GameObject.Find($"Player {i + 1}");
                if (potentialPlayerTarget == null)
                {
                    break;
                }

                distanceToPlayer = Vector3.Distance(transform.position, potentialPlayerTarget.transform.position);
                if (distanceToPlayer < smallestDistanceToPlayer)
                {
                    smallestDistanceToPlayer = distanceToPlayer;
                    targetedPlayer = potentialPlayerTarget;
                }
            }

    // Navigates the bullet to the targeted player by changing the angularVelocity and thereby the z rotation.
    // Bullets should maybe only be allowed to chase down a single player. Will have to look at that in playdemos.
        while (smallestDistanceToPlayer < 9f && smallestDistanceToPlayer > 3.5f)
        {
            // transform.rotation = new Quaternion.Euler
            Vector2 direction = (Vector2)targetedPlayer.transform.position - (Vector2)transform.position;
            direction.Normalize();
            float rotateAmount = Vector3.Cross(direction, transform.up).z;
            float rotateSpeed = 350f;

            rB2D.angularVelocity = -rotateAmount * rotateSpeed;

            rB2D.velocity = transform.up * 21f;
            // transform.position = Vector3.Lerp(transform.position, targetedPlayer.transform.position, 1f/21f * Time.deltaTime * 1.3f);
            // // instance.transform.position = Vector3.MoveTowards(instance.transform.position, targetedPlayer.transform.position, 0.01f);
            // smallestDistanceToPlayer = Vector3.Distance(transform.position, targetedPlayer.transform.position);
            yield return null;
        }
    }

    private Vector3 OutOfBounds()
    {
        if(transform.position.x >= 31.77)
        {
            bulletPosition = new Vector3(-transform.position.x + 0.1f, transform.position.y);
            return bulletPosition;
        }
        else if (transform.position.x <= -31.77)
        {
            bulletPosition = new Vector3(-transform.position.x - 0.1f, transform.position.y);
            return bulletPosition;
        }
        else if (transform.position.y >= 17.75)
        {
            bulletPosition = new Vector3(transform.position.x, -17.65f);
            return bulletPosition;
        }
        else if (transform.position.y <= -17.75)
        {
            bulletPosition = new Vector3(transform.position.x, 17.65f);
            return bulletPosition;
        }
        bulletPosition = new Vector3(0f, 0f);
        return bulletPosition;
    }

    private void OnTriggerEnter2D(Collider2D collider) 
    {
        string collision = collider.ToString();
        collisionTag = collider.tag;
        playerWhoShot = bulletTag.Split( )[0] + " " + bulletTag.Split( )[1];

        if (collision.Contains("KingoftheHill") || playerWhoShot == collisionTag || bulletTag == collisionTag)
            return;

        if (collision.Contains("HeadDetect"))
            player = collider.transform.parent.gameObject;
        else
            player = collider.gameObject;
        



        if (!collision.Contains(playerWhoShot) && bulletTag != collisionTag)
        {
            if (collisionTag.Contains("Player"))
            {
                player.GetComponent<Stats>().TakeDamage(1, playerWhoShot);
            }
            StopAllCoroutines();
            Destroy(gameObject);
        }

    }

}