using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    private Rigidbody2D rB2D;

    public static FallingPlatform instance {get; private set; }
    private void Awake()
    {
        instance = this;
        rB2D = this.GetComponent<Rigidbody2D>();
    }


    public void StartFalling(float fallSpeed)
    {
        rB2D.velocity = new Vector2(0, -fallSpeed);
    }

}
