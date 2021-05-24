using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public static float aspectRatio;
    public static float sceneTargetRatio;
    public static float targetWidth = 1920f;
    public static float targetHeight = 1080f;
    public static float currentWidth;
    public static float currentHeight;
    public static float deltaRatio;
    public SpriteRenderer Background;
    // Start is called before the first frame update
    void Awake()
    {
        currentWidth = Screen.width;
        currentHeight = Screen.height;
        aspectRatio = currentWidth / currentHeight;
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
