using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using System;

public class TargettingInformation : MonoBehaviour
{
    public GameObject targetPlatform;
    public SpriteRenderer cursorSprite;
    public TargetManager targetManager;
    public Transform targetCenter;

    private string targetName;
    public int targetIndex;

    // Start is called before the first frame update
    private void Awake()
    {
        targetName = this.name;
        targetCenter = this.transform;
    }

    private void Start()
    {

        // Regex pattern used for finding digits in the targets name.
        // Possible names are TargetNPlatform (0) - TargetNPlatform (22)
        // coudln't get groups working with regex in c#
        // otherwise i would normally write "(\d)*". and the match would be one or more digits in the string
        Regex rx = new Regex(@"(\d)", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        MatchCollection matches = rx.Matches(targetName);
        string indexName = "";


        // Loops through each match, which will be a digit character and adds it to the string
        // indexName will store these digit characters.
        foreach (Match match in rx.Matches(targetName))
        {
            indexName += match.Value;
        }
        targetIndex = Int32.Parse(indexName);

        
        DeliverTargetInformation();
    }


    private void DeliverTargetInformation()
    {
        targetManager.AddNewTarget(this);
    }


    public void EnableCursorSprite()
    {
        cursorSprite.enabled = true;
    }


    public void DisableCursorSprite()
    {
        cursorSprite.enabled = false;
    }


// Once a targetplatform object has been enabled it should not be
// disabled ever
    public void EnableTargetPlatform()
    {
        targetPlatform.SetActive(true);
    }

}
