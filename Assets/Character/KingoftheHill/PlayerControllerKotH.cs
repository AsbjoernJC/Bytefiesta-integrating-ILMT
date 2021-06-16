using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerControllerKotH : PlayerController
{

    protected override void Awake() 
    {
        base.Awake();
    }


    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (OutOfBounds() != new Vector3(0f, 0f))
        {
            transform.position = OutOfBounds();
        }
    }

    // Todo have UseBulletPowerUp and ReloadBullet in this child class

    public override void GotBulletPowerUp()
    {
        bulletCounter = 3;
        var sS = GetComponentInChildren<SpriteSpawner>();
        sS.SpawnBulletSprites(bulletCounter);
    }


    public override void GotShieldPowerUp()
    {
        hasShieldPowerUp = true;
        var sS = GetComponentInChildren<SpriteSpawner>();
        sS.SpawnShieldSprite();
    }

    protected override Vector3 OutOfBounds()
    {
        if(transform.position.x >= 30.86)
        {
            playerPosition = new Vector3(-transform.position.x + 0.1f, transform.position.y);
            return playerPosition;
        }
        else if (transform.position.x <= -30.86)
        {
            playerPosition = new Vector3(-transform.position.x - 0.1f, transform.position.y);
            return playerPosition;
        }
        else if (transform.position.y >= 16.87)
        {
            playerPosition = new Vector3(transform.position.x, -16.77f);
            return playerPosition;
        }
        else if (transform.position.y <= -16.87)
        {
            playerPosition = new Vector3(transform.position.x, 16.77f);
            return playerPosition;
        }
        playerPosition = new Vector3(0f, 0f);
        return playerPosition;
    }

}
