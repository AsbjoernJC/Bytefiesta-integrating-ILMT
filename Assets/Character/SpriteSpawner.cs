using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteSpawner : MonoBehaviour
{

    [SerializeField]
    private SpriteRenderer[] spriteLocations;

    [SerializeField]
    private Sprite[] sprites;


    // should not be assigned to spriteLocations[0] as it could override a useable shield powerup slot.
    public void SpawnBulletSprites(int bulletCounter)
    {
        for (int i = 0; i < bulletCounter; i++)
        {
            spriteLocations[i].sprite = sprites[0];
        }
    }

    public void RemoveBulletSprite(int bulletCounter)
    {
        spriteLocations[bulletCounter].sprite = null;
    }


    // should not be assigned to spriteLocations[0] as it could override a useable bullet powerup slot.
    // Could make some kind of counter, that is incremented/decremented when sprites are added/removed
    public void SpawnShieldSprite()
    {
        spriteLocations[0].sprite = sprites[1];
    }

    public void RemoveSprite()
    {
        spriteLocations[0].sprite = null;
    }
}
