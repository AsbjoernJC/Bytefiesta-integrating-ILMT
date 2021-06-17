using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectActivator : MonoBehaviour
{
    
    protected void ActivateObjects(GameObject[] objects)
    {
        for (int i = 0; i < PlayerConfigurationManager.Instance.numberOfActivePlayers; i++)
        {
            objects[i].SetActive(true);
        }
    }
}
