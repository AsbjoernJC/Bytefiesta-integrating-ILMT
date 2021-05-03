using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Rigidbody2D rB2D;
    public float bulletSpeed = 18f;
    private Vector3 bulletPosition;

    // Start is called before the first frame update

    void Update() 
    {

        if (OutOfBounds() != new Vector3(0,0))
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider) 
    {
        Debug.Log(collider);
        Destroy(gameObject);
    }

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

}
