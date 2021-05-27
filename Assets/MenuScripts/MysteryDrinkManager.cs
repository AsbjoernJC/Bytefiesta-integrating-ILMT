using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;

public class MysteryDrinkManager : MonoBehaviour
{
    [SerializeField]
    private Image mysteryPlayerImage;

    [SerializeField]
    private Sprite[] playerSprites;
    [SerializeField]
    private GameObject playerButtonGroup;
    [SerializeField]
    private GridLayoutGroup buttonGroup;
    [SerializeField]
    private PlayerInput playerInput;

    // Start is called before the first frame update
    void Start()
    {
        mysteryPlayerImage.sprite = playerSprites[Random.Range(0, 3)];
        mysteryPlayerImage.color = new Color32(0, 0, 0, 255);
        StartCoroutine("ShufflePlayers");
    }

// Takes a random sprite from playerSprites and shuffles to a new one every x seconds.
// It slows down over time
    private IEnumerator ShufflePlayers()
    {
        float timePassed = 0f;
        float rotationSpeed = 0.27f;
        // while (timePassed < 3f)
        // {
        //     mysteryPlayerImage.sprite = playerSprites[Random.Range(0, 3)];
        //     yield return new WaitForSeconds(rotationSpeed);
        //     timePassed += rotationSpeed + Time.deltaTime;
        // }
        // while (timePassed >= 3 && timePassed < 8.5)
        // {
        //     mysteryPlayerImage.sprite = playerSprites[Random.Range(0, 3)];
        //     // rotationSpeed is slowed down in this part
        //     if (rotationSpeed >= 0)
        //     // 11.5f is just an arbitrary amount of time. It fit well
        //         rotationSpeed += Time.deltaTime * 14f;
        //         if (rotationSpeed < 0)
        //             rotationSpeed = 0;
        //     yield return new WaitForSeconds(rotationSpeed);
        //     timePassed += rotationSpeed + Time.deltaTime;
        // }
        

        mysteryPlayerImage.color = new Color32(255, 255, 255, 255);
        AllowPlayerControl();
        yield return null;
    }

    private void AllowPlayerControl()
    {
        for (int playerIndex = 0; playerIndex < PlayerConfigurationManager.numberOfActivePlayers; playerIndex++)
        {
            var playerController = PlayerConfigurationManager.playerControllers[playerIndex];
            var inputUser = DifficultyAndScore.playerInputs[playerIndex].user;
            // var playerControlScheme = PlayerConfigurationManager.playerControlSchemes[playerIndex];

            Debug.Log(DifficultyAndScore.playerInputs[playerIndex].user.id);
            
            // PlayerInput playerInput = PlayerInput.Instantiate(playerPrefab[playerIndex], playerIndex, playerControlScheme, -1, playerController);
            var playerButtonController = Instantiate(playerButtonGroup);
            playerButtonController.transform.SetParent(buttonGroup.transform);
            playerInput.enabled = true;
            // playerButtonInput.SwitchCurrentControlScheme(playerControlScheme);
            InputUser.PerformPairingWithDevice(playerController, inputUser, InputUserPairingOptions.UnpairCurrentDevicesFromUser);
            // Debug.Log(inputUser);
        }
    }

}
