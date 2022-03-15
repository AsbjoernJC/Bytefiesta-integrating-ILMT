using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTargettingManager : MonoBehaviour
{

    public static PlayerTargettingManager Instance { get; private set; }

    public List<Sprite> boardSprites;

    [SerializeField]
    public List<Texture> boardImages;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.Log("Trying to create another singleton object");
        }
    }


    private void SwitchTargets()
    {

    }

}


public class BoardManager
{
    public BoardSquare[] boardSquares = new BoardSquare[12];
    private static BoardManager _instance = new BoardManager();
    private Sprite spriteToFind;
    private int findBoardSquareIndex;
    private int initializedBoardSquares = 0;
    private SpriteRenderer symbolToFind;


    public static BoardManager Instance { get { return _instance; }}

    private void Awake() {
        var stf = GameObject.Find("SymbolToFind");
        symbolToFind = (SpriteRenderer)stf.GetComponent(typeof(SpriteRenderer));
    }

    public void AddNewSquare(SquareInformation SI)
    {
        // Adds a BoardSquare object with its information to the boardSquares array
        BoardSquare boardSquare = new BoardSquare(SI);
        boardSquares[boardSquare.squareIndex] = boardSquare;
        initializedBoardSquares++;
        
        if (initializedBoardSquares == 12)
        {
            Awake();
            SelectSpriteToFind();
        }
    }

    private void SelectSpriteToFind()
    {
        // Chooses a random sprite that the players should look for on a random boardsquare
        var spriteToFind = PlayerTargettingManager.Instance.boardSprites[Random.Range(0, 12)];
        symbolToFind.sprite = spriteToFind;
        findBoardSquareIndex = Random.Range(0, 12);
        
        // Initializes the chosen boardsquare's values
        boardSquares[findBoardSquareIndex].mustFindSprite = true;
        boardSquares[findBoardSquareIndex].squareSprite.sprite = spriteToFind;
        boardSquares[findBoardSquareIndex].squareSprite.enabled = true;

        SpawnBoardSprites();
    }

    private IEnumerator SpawnBoardSprites()
    {
        yield return new WaitForSeconds(1.5f);
    }

}

public class BoardSquare
{

    // The board where the squares are instantiated can be visualized as such
    // where the number is the index of the square
    // [[0], [1], [2],  [3],
    //  [4], [5], [6],  [7],
    //  [8], [9], [10], [11]]

    public BoardSquare(SquareInformation SI)
    {
        name = SI.name;
        squareIndex = SI.squareIndex;
        squareSprite = SI.squareSprite;
        isEdge = SI.isEdge;
        mustFindSprite = SI.mustFindSprite;
    }

    public string name { get; set; }
    public int squareIndex { get; set; }
    
    public bool isEdge { get; set; }
    public Image squareSprite { get; set; }

    public bool mustFindSprite { get; set; }
}
