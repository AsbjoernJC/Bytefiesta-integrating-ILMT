using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using System;

public class SquareInformation : MonoBehaviour
{
    [SerializeField] public Image squareSprite;
    public bool isEdge;
    public int squareIndex;
    public BoardSpriteSpawner BSS;
    private string squareName;
    public bool mustFindSprite;


    private void Awake()
    {
        squareName = this.name;
    }


    // Start is called before the first frame update
    private void Start()
    {

        // Regex pattern used for finding digits in the targets name.
        // Possible names are TargetNPlatform (0) - TargetNPlatform (22)
        // coudln't get groups working with regex in c#
        // otherwise i would normally write "(\d)*". and the match would be one or more digits in the string
        Regex rx = new Regex(@"(\d)", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        MatchCollection matches = rx.Matches(squareName);
        string indexName = "";


        // Loops through each match, which will be a digit character and adds it to the string
        // eg. if squareName is TargetNPlatform (13) the first match will be 1 and the next match will be 3
        // we concatenate those two values in a string "13" and parse it to an int
        foreach (Match match in rx.Matches(squareName))
        {
            indexName += match.Value;
        }
        squareIndex = Int32.Parse(indexName);

        
        DeliverSquareInformation();
    }


    private void DeliverSquareInformation()
    {
        BoardManager.Instance.AddNewSquare(this);
    }

}
