using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        while (timePassed < 3f)
        {
            mysteryPlayerImage.sprite = playerSprites[Random.Range(0, 3)];
            yield return new WaitForSeconds(rotationSpeed);
            timePassed += rotationSpeed + Time.deltaTime;
        }
        while (timePassed >= 3 && timePassed < 8.5)
        {
            mysteryPlayerImage.sprite = playerSprites[Random.Range(0, 3)];
            // rotationSpeed is slowed down in this part
            if (rotationSpeed >= 0)
            // 11.5f is just an arbitrary amount of time. It fit well
                rotationSpeed += Time.deltaTime * 14f;
                if (rotationSpeed < 0)
                    rotationSpeed = 0;
            yield return new WaitForSeconds(rotationSpeed);
            timePassed += rotationSpeed + Time.deltaTime;
        }
        mysteryPlayerImage.color = new Color32(255, 255, 255, 255);
        AllowPlayerControl();
        yield return null;
    }

    private void AllowPlayerControl()
    {
        for (int i = 0; i < 1; i++)
        {
            var playerButtonController = Instantiate(playerButtonGroup);
            playerButtonController.transform.SetParent(buttonGroup.transform);
        }

    }

}
