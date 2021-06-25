using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerTrapRun : PlayerController
{
    // Start is called before the first frame update
    protected override void Awake() 
    {
        base.Awake();

        // Player ignore eachother's colissions
        Physics2D.IgnoreLayerCollision(6, 6, true);
    }


}
