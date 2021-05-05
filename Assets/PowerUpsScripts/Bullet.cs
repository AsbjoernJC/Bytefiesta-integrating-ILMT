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

    // Start is called before the first frame update

    void Update() 
    {

        if (OutOfBounds() != new Vector3(0,0))
        {
            Destroy(gameObject);
        }
        if (playerWhoShot == null)
            playerWhoShot = this.tag;
    }



// Todo: The bullet should not be able to collide with the player who shot the bullet
// Need to implement a way for the bullet prefab to detect if the bullet 
// comes from the player who shot, as it should not collide with this person or the person's bullets
    private void OnTriggerEnter2D(Collider2D collider) 
    {
        string Collision = collider.ToString();
        playerWhoShot = playerWhoShot.Split( )[0] + " " + playerWhoShot.Split( )[1];
        Debug.Log(Collision);
        Debug.Log(playerWhoShot);
        if (!Collision.Contains(playerWhoShot))
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
