using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealthUIUpdater : MonoBehaviour
{
    [SerializeField] public TMP_Text[] playerTexts;

    public static HealthUIUpdater instance {get; private set; }
    void Awake()
    {
        instance = this;
    }

    public void ChangePlayerText(int playerIndex, int playerHealth)
    {
        playerTexts[playerIndex - 1].text = playerHealth.ToString();
    }
}
