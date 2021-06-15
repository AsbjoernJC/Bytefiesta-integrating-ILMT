using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuPlayerInitializer : MonoBehaviour
{
    [SerializeField]
    Image[] playerImages;
    // Start is called before the first frame update
    void Start()
    {
        InitializePlayerSprites();
    }


    // Need to change if there are more characters in the future.
    // As we activate Images that already are assigned a sprite. Player 1, 2, 3 and 4 always
    //have the same sprites as of now as they are the only character models
    private void InitializePlayerSprites()
    {
        for (int i = 0; i < PlayerConfigurationManager.Instance.numberOfActivePlayers; i++)
        {
            playerImages[i].enabled = true;
        }
    }
}
