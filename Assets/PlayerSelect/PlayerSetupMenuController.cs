using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerSetupMenuController : MonoBehaviour
{
    private int playerIndex;
    
    // [SerializeField]
    // private TextMeshProUGUI titleText;
    [SerializeField]
    private GameObject characterPanel;
    [SerializeField]
    private GameObject readyPanel;
    [SerializeField]
    private Image characterSelectionImage;
    [SerializeField]
    private Button characterSelectButton;
    [SerializeField]
    private Button readyButton;
    [SerializeField]
    private Sprite[] playerUI;
    private float ignoreInputTime = 1.5f;
    private bool inputEnabled;

    public void SetPlayerIndex(int pi)
    {
        playerIndex = pi;

        // To do: Add tinycontroller sprite with text displaying which controller controls the character
        // titleText.SetText("Player " + (pi + 1).ToString());
        ignoreInputTime = Time.time + ignoreInputTime;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time > ignoreInputTime)
        {
            inputEnabled = true;
        }
    }

    public void ShowPlayerModel()
    {
        if (!inputEnabled) 
        {
            return;
        }

        characterSelectionImage.sprite = playerUI[playerIndex];
        this.transform.localScale = new Vector3(0.6f, 0.6f);
        characterPanel.SetActive(false);
        readyPanel.SetActive(true);
        readyButton.Select();
        
    }

    public void ReadyPlayer()
    {
        if (!inputEnabled) 
        {
            return;
        }

        PlayerConfigurationManager.Instance.ReadyPlayer(playerIndex);
        readyPanel.SetActive(false);
        
    }

}
