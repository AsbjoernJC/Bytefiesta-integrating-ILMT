using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerControllerKotH : PlayerController
{
    // PlayerController for the minigame KingoftheHill
    private SpriteSpawner sS;

    protected override void Awake() 
    {
        base.Awake();
        sS = GetComponentInChildren<SpriteSpawner>();
        // Players start king of the hill with a normal bullet
        sS.SpawnNormalBullet();
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
    public void UseBulletPowerUp(InputAction.CallbackContext context)
    {
        bool powerUpBullet = false;
        if (context.action.triggered && hasShieldPowerUp)
        {
            sS.RemoveSprite();
            var player = this.gameObject;
            player.GetComponent<Stats>().GainHealth(1);
            shieldPoint.sprite = shieldSprite;
            hasShieldPowerUp = false;
            return;
        }

        if (context.action.triggered && bulletCounter > 0)
        {
            powerUpBullet = true;
            var playerName = this.name;
            
            BulletManager.Shoot(firePoint, powerUp[0], shootingAngle, playerName, powerUpBullet);
            bulletCounter --;
            sS.RemoveBulletSprite(bulletCounter);
            return;
        }
        // Shoots the normal bullet.
        // Todo: The player should only be able to shoot a normal bullet once every x seconds thinking (0.5-1)
        // This can be done by calling a coroutine (RefillBullet or the likes) after having shot the normal bullet
        // The players should probably spawn with the normal bullet sprite and have it be ready for use.
        // Should also have a sprite for SpriteSpawner,
        // Can remove sprite here and in the coroutine that rese ts the bullet could draw the sprite and
        // allow the player to shoot again, however, only when bulletCounter < 0. Should maybe allow the player to shoot
        // if hasShieldPowerUp = true;
        if (context.action.triggered && hasNormalBullet)
        {
            powerUpBullet = false;
            var playerName = this.name;
            BulletManager.Shoot(firePoint, powerUp[1], normalBulletAngle, playerName, powerUpBullet);
            sS.RemoveSprite();
            hasNormalBullet = false;
            StartCoroutine(ReloadBullet(sS));
        }
    }


    private IEnumerator ReloadBullet(SpriteSpawner sS)
    {
        yield return new WaitForSeconds(reloadSpeed);
        if (bulletCounter != 0 || hasShieldPowerUp)
            yield return null;
        hasNormalBullet = true;
        sS.SpawnNormalBullet();
    }


    public override void GotBulletPowerUp()
    {
        bulletCounter = 3;
        sS.SpawnBulletSprites(bulletCounter);
    }


    public override void GotShieldPowerUp()
    {
        hasShieldPowerUp = true;
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
