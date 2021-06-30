using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetManager : MonoBehaviour
{
    // Target[0] will be the leftmost target in the minigame
    // Players will loop through the possible targets by holding left or right in the minigame
    public Target[] targets = new Target[23];
 
    // Might remove and use gameObject.find in PlayerCannonController instead
    public static TargetManager instance { get; private set; }


    private void Awake()
    {
        // Unsure if we should use Singleton
        instance = this;
    }


    public void AddNewTarget(TargettingInformation ti)
    {
        Target target = new Target(ti);

        targets[target.targetIndex] = target;
    }
}


public class Target
{

    public Target(TargettingInformation ti)
    {
        name = ti.name;
        targetIndex = ti.targetIndex;
        targetPlatform = ti.targetPlatform;
        cursorSprite = ti.cursorSprite;
        targetCenter = ti.targetCenter;
    }

    public string name { get; set; }
    public int targetIndex { get; set; }
    public GameObject targetPlatform { get; set; }
    public SpriteRenderer cursorSprite { get; set; }
    public Transform targetCenter { get; set; }
}