using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTargettingManager : MonoBehaviour
{

    public static PlayerTargettingManager Instance {get; private set; }


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


    public void AddNewSquare(SquareInformation SI)
    {

    }
}
