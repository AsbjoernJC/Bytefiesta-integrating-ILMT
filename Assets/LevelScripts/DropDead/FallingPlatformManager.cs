using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatformManager : MonoBehaviour
{

    // element 0.x = -10, element 1.x = -5, element 2.x = 0 etc.
    [SerializeField] private Transform[] platformSpawnpoints;
    [SerializeField] private GameObject fallingPlatformPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ChooseSpawnpoint()
    {
        // The largest possible horizontal reach of a player is eg. from platformSpawnpoints[0] to platformSpawnpoints[3]
    }

    private void SpawnPlatform()
    {
        GameObject fallingPlatform = Instantiate(fallingPlatformPrefab);
    }
}
