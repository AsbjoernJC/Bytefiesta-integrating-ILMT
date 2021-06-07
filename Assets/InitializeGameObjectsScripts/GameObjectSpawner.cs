using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectSpawner : MonoBehaviour
{
    [SerializeField]
    public GameObject[] cannons;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < PlayerConfigurationManager.numberOfActivePlayers; i++)
        {
            cannons[i].SetActive(true);
        }
    }

}
