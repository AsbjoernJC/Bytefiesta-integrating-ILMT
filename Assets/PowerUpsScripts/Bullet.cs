using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Bullet : MonoBehaviour
{
    public Rigidbody2D rB2D;
    [SerializeField]
    private float smoothing = 0.0001f;
    private Vector3 bulletPosition;
    private Transform firePoint; 
    private Quaternion shootingAngle;
    private string playerWhoShot;
    private string collisionTag;
    private static string bulletTag;
    private GameObject player;
    // Start is called before the first frame update
    private static GameObject targetedPlayer;
    private bool isPowerUpBullet = false;

    public static Bullet instance { get; private set; }

    private void Awake() 
    {
        instance = this;    
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
            FindPlayerPositions();
        }

    }

    private void FixedUpdate() 
    {
        
    }

// Checks who the bullet is created by, by looking at the GameObject's tag
// If it is not the player who shot the bullet, or another bullet from the same player
// the bullet will be destroyed. 
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

// Stops bullets from leaving the scene/arena, however, this is quite intensive.
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

// Todo: should check if the bullet is the powerup form or just the normal.
// If it is the normal bullet the "lifespan" should be shortened via a coroutine
    public static void Shoot(Transform firePoint, GameObject powerUp, Quaternion shootingAngle, string playerName, bool powerUpBullet)
    {
        float bulletSpeed = 21f;
        GameObject bullet = Instantiate(powerUp, firePoint.transform.position, shootingAngle);
        bullet.tag = playerName + " bullet";
        bulletTag = instance.tag;
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * bulletSpeed, ForceMode2D.Impulse);
        if (powerUpBullet)
        {
            instance.StartCoroutine("FindPlayerPositions");
            instance.GetComponent<Bullet>().isPowerUpBullet = true;
            return;
        }
        instance.StartCoroutine("BulletLifeSpan");
    }

    private IEnumerator BulletLifeSpan()
    {
        yield return new WaitForSeconds(0.4f);
        Destroy(this.gameObject);
    }

    private void FindPlayerPositions()
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
                targetedPlayer = GameObject.Find($"Player {i + 1}");
                distanceToPlayer = Vector3.Distance(instance.transform.position, targetedPlayer.transform.position);
                if (distanceToPlayer < smallestDistanceToPlayer)
                {
                    smallestDistanceToPlayer = distanceToPlayer;
                }
            }

// For some reason the transform.position.y value jumps quite a bit at some point instead of slowly increasing
        while (smallestDistanceToPlayer < 13f && smallestDistanceToPlayer > 4f)
        {
            instance.transform.position = Vector3.Lerp(instance.transform.position, targetedPlayer.transform.position, Time.deltaTime * smoothing);
            smallestDistanceToPlayer = Vector3.Distance(instance.transform.position, targetedPlayer.transform.position);
        }
    }

}
