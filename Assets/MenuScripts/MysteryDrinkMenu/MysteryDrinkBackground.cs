using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MysteryDrinkBackground : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer background;

    [SerializeField]
    public List<Sprite> backgroundDifficulties;
    // backgroundDifficulties[0] = the background for hardcore. backgroundDifficulties[1] = the background for medium and light

    // Start is called before the first frame update
    void Awake()
    {
        foreach (var element in DifficultyAndScore.difficulties)
        {
            if (element.Value == true)
            {
                if (element.Key == "Hardcore")
                {
                    background.sprite = backgroundDifficulties[0];
                }
                else 
                {
                    background.sprite = backgroundDifficulties[1];
                }
            }
        }
    }

}
