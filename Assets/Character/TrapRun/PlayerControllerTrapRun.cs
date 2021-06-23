using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerTrapRun : PlayerController
{
    // Start is called before the first frame update
    protected override void Awake() 
    {
        base.Awake();
        Physics2D.IgnoreLayerCollision(6, 6, true);
    }


}
