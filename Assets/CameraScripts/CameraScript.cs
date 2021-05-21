using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public static float aspectRatio;
    public static float sceneTargetRatio;
    public static float deltaRatio;
    public SpriteRenderer Background;
    // Start is called before the first frame update
    void Start()
    {
        aspectRatio = (float) Screen.width / (float) Screen.height;
        sceneTargetRatio = Background.bounds.size.x / Background.bounds.size.y;

        if (aspectRatio >= sceneTargetRatio)
        {
            Camera.main.orthographicSize = Background.bounds.size.y / 2;
        }
        else 
        {
            deltaRatio = sceneTargetRatio / aspectRatio;
            Camera.main.orthographicSize = Background.bounds.size.y / 2 * deltaRatio;
        }
    }

}
