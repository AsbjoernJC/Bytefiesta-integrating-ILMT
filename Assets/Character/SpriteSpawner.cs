using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteSpawner : MonoBehaviour
{

    [SerializeField]
    private SpriteRenderer[] spriteLocations;
    [SerializeField]
    private Sprite[] sprites;

    // Start is called before the first frame update
    void Awake()
    {

    }


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
}
