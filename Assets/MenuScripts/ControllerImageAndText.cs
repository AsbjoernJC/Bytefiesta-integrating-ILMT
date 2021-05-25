using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ControllerImageAndText : MonoBehaviour
{
    [SerializeField]
    public TMP_Text[] controllerText;
    [SerializeField]
    public Image[] controllerPictures;

    public void ChangeControllerText(string controllerID, int playerIndex)
    {
        controllerPictures[playerIndex].enabled = true;
        controllerText[playerIndex].text = controllerID;
        // controllerText 
    }

}

