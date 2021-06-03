using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{

    [SerializeField]
    private Transform[] cannonFirepoints;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("SpawnCannonball");
    }


    private IEnumerator SpawnCannonball()
    {
        float timePassed = 0;
        
        while ((int)timePassed <= 6)
        {
            if ((int)timePassed % 1 == 0)
            {
                // Shoot
            }
            timePassed = Time.deltaTime;
        }
        yield return null;
    }

}
