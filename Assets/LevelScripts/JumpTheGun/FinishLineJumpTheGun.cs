using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLineJumpTheGun : FinishLine
{
    private List<int> winnerIndexes = new List<int>();
    private JumpTheGunLevelInitializer jumpTheGunLevelInitializer;

    private void Start()
    {
        jumpTheGunLevelInitializer = GameObject.Find("LevelInitializer").GetComponent<JumpTheGunLevelInitializer>();
    }

    protected override void OnTriggerEnter2D(Collider2D collider) 
    {
        // hasCollided is only set to true when a player has touched the finishline and the end of the minigame has been called
        // therefore we don't need to check for other collisions at this point in time.
        if (hasCollided)
            return;

        string collision = collider.ToString();
        collisionTag = collider.tag;

        // A player's head detection is a child of the player prefab therefore we need to get the gameobject of the parent
        if (collision.Contains("HeadDetect") || collision.Contains("Feet"))
            player = collider.transform.parent.gameObject;
            
        // If the collider still belongs to the player it is directly a part of the player GameObject/prefab
        else
            player = collider.gameObject;
        



        if (collisionTag.Contains("Team 1"))
        {
            hasCollided = true;
            foreach (var elem in jumpTheGunLevelInitializer.teamPlayerData)
            {
                if (elem.Value.team == 1)
                {
                    RaceTracker.instance.playerScores[elem.Value.ToString()] = 1;
                    winnerIndexes.Add(elem.Value.playerIndex);
                }
            }
        }

        else if (collisionTag.Contains("Team 2"))
        {
            hasCollided = true;
            foreach (var elem in jumpTheGunLevelInitializer.teamPlayerData)
            {
                if (elem.Value.team == 2)
                {
                    RaceTracker.instance.playerScores[elem.Value.ToString()] = 1;
                    winnerIndexes.Add(elem.Value.playerIndex);
                }
            }
        }
        RaceTracker.instance.MiniGameEnd(winnerIndexes);
        return;
    }
}
