using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public SpriteRenderer Background;
    // Start is called before the first frame update
    void Start()
    {
        float aspectRatio = (float) Screen.width / (float) Screen.height;
        float sceneTargetRatio = Background.bounds.size.x / Background.bounds.size.y;

        if (aspectRatio >= sceneTargetRatio)
        {
            Camera.main.orthographicSize = Background.bounds.size.y / 2;
        }
        else 
        {
            float deltaRatio = sceneTargetRatio / aspectRatio;
            Camera.main.orthographicSize = Background.bounds.size.y / 2 * deltaRatio;
        }
    }

}
