using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ControllerImageAndText : MonoBehaviour
{
    [SerializeField] public TMP_Text[] controllerText;
    [SerializeField] public Image[] controllerPictures;

    [SerializeField] private Sprite keyboardSprite;


    public void ChangeControllerText(string controllerID, int playerIndex)
    {
        // Used to enable the controller sprites above the player aswell as assign their controller id to the text element
        controllerPictures[playerIndex].enabled = true;
        controllerText[playerIndex].text = controllerID;
    }

    public void KeyboardPlayer(int playerIndex)
    {
        // Used to enable a keyboard sprite above the player
        controllerPictures[playerIndex].sprite = keyboardSprite;
        controllerPictures[playerIndex].enabled = true;
    }

}

