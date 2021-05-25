using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

public class SpawnPlayerSetupMenu : MonoBehaviour
{
    public GameObject characterJoinMenuPrefab;
    public PlayerInput input;

    private void Awake() 
    {
        // var rootMenu = GameObject.Find("MainLayout");

        // spawns the characterselection menu. Where players in the future will be able to change character
        // for now it is used for them to navigate and lock in that they are ready.
        var characterRootMenu = GameObject.Find("CharacterSelectionLayout");
        if (characterRootMenu != null)
        {
            var menu = Instantiate(characterJoinMenuPrefab, characterRootMenu.transform);
            input.uiInputModule = menu.GetComponentInChildren<InputSystemUIInputModule>();
            menu.GetComponent<PlayerSetupMenuController>().SetPlayerIndex(input.playerIndex);
        }
    }
}
