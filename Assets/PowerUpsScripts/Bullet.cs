using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Bullet : MonoBehaviour
{
    public Rigidbody2D rB2D;
    private Vector3 bulletPosition;
    private Transform firePoint; 
    private Quaternion shootingAngle;
    private string playerWhoShot;
    private string collisionTag;
    private string bulletTag;

    // Start is called before the first frame update

    void Update() 
    {

        if (OutOfBounds() != new Vector3(0,0))
        {
            Destroy(gameObject);
        }
        if (bulletTag == null)
            bulletTag = this.tag;
    }



// Checks who the bullet is created by, by looking at the GameObject's tag
// If it is not the player who shot the bullet, or another bullet from the same player
// the bullet will be destroyed. 
    private void OnTriggerEnter2D(Collider2D collider) 
    {
        string collision = collider.ToString();
        collisionTag = collider.tag;
        playerWhoShot = bulletTag.Split( )[0] + " " + bulletTag.Split( )[1];
        if (!collision.Contains(playerWhoShot) && bulletTag != collisionTag)
            // If it hits a Player it should kill/do damage.
            Destroy(gameObject);
    }

// Stops bullets from leaving the scene/arena, however, this is quite intensive.
    private Vector3 OutOfBounds()
    {
        if(transform.position.x >= 17.36)
        {
            bulletPosition = new Vector3(-transform.position.x + 0.1f, transform.position.y);
            return bulletPosition;
        }
        else if (transform.position.x <= -17.36)
        {
            bulletPosition = new Vector3(-transform.position.x - 0.1f, transform.position.y);
            return bulletPosition;
        }
        else if (transform.position.y >= 13.4)
        {
            bulletPosition = new Vector3(transform.position.x, -11.3099f);
            return bulletPosition;
        }
        else if (transform.position.y <= -11.3199)
        {
            bulletPosition = new Vector3(transform.position.x, 13.39f);
            return bulletPosition;
        }
        bulletPosition = new Vector3(0f, 0f);
        return bulletPosition;
    }


    public static void Shoot(Transform firePoint, GameObject powerUp, Quaternion shootingAngle, string playerName)
    {
        float bulletSpeed = 18f;
        GameObject bullet = Instantiate(powerUp, firePoint.transform.position, shootingAngle);
        bullet.tag = playerName + " bullet";
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * bulletSpeed, ForceMode2D.Impulse);
    }

}
