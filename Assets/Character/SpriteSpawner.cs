using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteSpawner : MonoBehaviour
{

    [SerializeField]
    private SpriteRenderer[] spriteLocations;

    // sprites[0] = bullet sprite, sprites [1] = heart sprite
    [SerializeField]
    private Sprite[] sprites;
    private int bulletSpriteCount;
    private int shieldSpriteCount;
    private int spritesPushed;
    private bool hasNormalBullet;



    // should not be assigned to spriteLocations[0] as it could override a useable shield powerup slot.
    public void SpawnBulletSprites(int bulletCounter)
    {
        // If the player has a spriteLocations[0] is the shield sprite (sprites[1]) 
        // then the spriteLocations[1] and spriteLocations[2] should be bullet sprites (sprites[0]). 
        if (shieldSpriteCount == 1)
        {
            for (int i = 0; i < bulletCounter; i++)
            {
                spriteLocations[i].sprite = sprites[0];
            }
            bulletSpriteCount = 3;
        }
        else
        {
            for (int i = 0; i < bulletCounter; i++)
            {
                spriteLocations[i].sprite = sprites[0];
            }
            bulletSpriteCount = 3;
        }
    }

    public void RemoveBulletSprite(int bulletCounter)
    {
        // Sprites are pushed when bulletCounter is > 0 and the player has picked up a 
        // Shield powerup Look in SpawnShieldSprite().
        if (spritesPushed > 0)
        {
            spriteLocations[bulletCounter + 1].sprite = null;
            spritesPushed --;
            bulletSpriteCount --;
            return;
        }
        else 
        {
            spriteLocations[bulletCounter].sprite = null;
            bulletSpriteCount --;
        }

        if (bulletCounter == 0 && hasNormalBullet)
            spriteLocations[0].sprite = sprites[2];
    }


    // should not be assigned to spriteLocations[0] as it could override a useable bullet powerup slot.
    // Could make some kind of counter, that is incremented/decremented when sprites are added/removed
    public void SpawnShieldSprite()
    {
        if (bulletSpriteCount > 0 && bulletSpriteCount != 3)
        {
            // spriteLocation[0] will be set to the health sprite and will push other sprites' index by 1 
            for (int i = 0; i < bulletSpriteCount; i++)
            {
                spriteLocations[i + 1].sprite = sprites[0];
                spritesPushed = bulletSpriteCount;
            }
        }
        spriteLocations[0].sprite = sprites[1];
        shieldSpriteCount = 1;
    }

// when the shield power up is used and the player has 3 bullets. It shows the three bullet sprites.
// Used only for removing normal bullet sprites or shield sprites.
    public void RemoveSprite()
    {
        var sprite0 = spriteLocations[0].sprite;
        spriteLocations[0].sprite = null;

        if(sprite0 == sprites[2])
        {
            hasNormalBullet = false;
            return;
        }
        // Used when the player has 3 bullets in the chamber, however, one of the sprites have been replaced by a heart sprite (sprites[1])
        if (bulletSpriteCount == 3)
        {
            spriteLocations[0].sprite = sprites[0];
            shieldSpriteCount --;
            return;
        }
        
        if (sprite0 == sprites[1])
        {
            shieldSpriteCount --;

            if (hasNormalBullet && shieldSpriteCount == 0)
            {
                spriteLocations[0].sprite = sprites[2];
            }
        }

    }

    public void SpawnNormalBullet()
    {
        spriteLocations[0].sprite = sprites[2];
        hasNormalBullet = true;
    }
}
