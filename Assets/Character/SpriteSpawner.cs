using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteSpawner : MonoBehaviour
{

    [SerializeField]
    private SpriteRenderer[] spriteLocations;

    [SerializeField]
    private Sprite[] sprites;
    private int bulletSpriteCount;
    private int shieldSpriteCount;


    // should not be assigned to spriteLocations[0] as it could override a useable shield powerup slot.
    public void SpawnBulletSprites(int bulletCounter)
    {
        // If the player has a spriteLocations[0] is the shield sprite (sprites[1]) then the spriteLocations[1] and spriteLocations[2]
        // should be bullet sprites (sprites[0]) 
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
        spriteLocations[bulletCounter].sprite = null;
        bulletSpriteCount --;
    }


    // should not be assigned to spriteLocations[0] as it could override a useable bullet powerup slot.
    // Could make some kind of counter, that is incremented/decremented when sprites are added/removed
    public void SpawnShieldSprite()
    {
        spriteLocations[0].sprite = sprites[1];
        shieldSpriteCount = 1;
    }

// when the shield power up is used and the player has 3 bullets. It shows the three bullet sprites.
    public void RemoveSprite()
    {
        spriteLocations[0].sprite = null;
        if (bulletSpriteCount == 3)
        {
            spriteLocations[0].sprite = sprites[0];
        }
        shieldSpriteCount --;
    }
}
