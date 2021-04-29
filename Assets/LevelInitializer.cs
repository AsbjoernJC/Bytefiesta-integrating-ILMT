using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// for bugs see: https://www.youtube.com/watch?v=_5pOiYHJgl0
public class LevelInitializer : MonoBehaviour
{
    [SerializeField]
    private Transform[] playerSpawns;
    [SerializeField]
    private GameObject playerPrefab;

    // Start is called before the first frame update
    void Start()
    {
        var playerConfigs = PlayerConfigurationManager.Instance.GetPlayerConfigs().ToArray();
        for (int i = 0; i < playerConfigs.Length; i++)
        {
            var player = Instantiate(playerPrefab, playerSpawns[i].position, 
            playerSpawns[i].rotation, gameObject.transform);
            player.GetComponent<PlayerController>();
        }
    }

}
