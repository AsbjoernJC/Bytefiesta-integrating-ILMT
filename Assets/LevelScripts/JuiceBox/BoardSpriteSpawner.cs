using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoardSpriteSpawner : MonoBehaviour
{
    [SerializeField] private SpriteRenderer symbolToFind;

}

public class BoardSquares
{

    // Squares will be their own prefab and will pass the information to

    public BoardSquares(SquareInformation SI)
    {
        // Receives

        name = SI.name;
        squareIndex = SI.squareIndex;
        squareSprite = SI.squareSprite;
    }

    public string name { get; set; }
    public int squareIndex { get; set; }
    
    public bool isEdge { get; set; }
    public Image squareSprite { get; set; }
}
