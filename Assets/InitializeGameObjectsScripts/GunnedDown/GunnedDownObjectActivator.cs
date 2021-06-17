using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunnedDownObjectActivator : GameObjectActivator
{
    [SerializeField] public GameObject[] cannons;
    [SerializeField] public GameObject[] playerHearts;
    [SerializeField] public GameObject[] playerTexts;

    // Start is called before the first frame update
    private void Awake()
    {
        ActivateObjects(cannons);
    }

}
