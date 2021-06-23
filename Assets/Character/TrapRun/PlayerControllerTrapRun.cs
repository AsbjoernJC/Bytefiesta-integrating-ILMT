using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerTrapRun : PlayerController
{
    // Start is called before the first frame update
    protected override void Awake() 
    {
        base.Awake();
        Physics.IgnoreLayerCollision(6, 6);
    }

    protected override void Update()
    {
        base.Update();
    }

}
