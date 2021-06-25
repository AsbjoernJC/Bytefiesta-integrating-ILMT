using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAxe : MonoBehaviour
{
    // Todo: Should make a base class that collides with players and deals damage. This should derive from that class
    [SerializeField] public Transform rotationPoint;

    // rotationSpeed is in units of angle (degrees) / second
    [SerializeField] public float rotationSpeed = 100f;
    [SerializeField] public float waitTimer = 0.5f;


    private Transform axeTransform;

    private string collisionTag;
    private GameObject player;
    private bool hasCollided = false;
    private bool axeShouldSpin = false;
    private bool calledCoroutine = false;


    private void Awake()
    {
        axeTransform = this.gameObject.transform;
    }


    // Update is called once per frame
    private void Update()
    {

        // If time does not stand still we will start spinning the axes on a delay accordingly to the waitTimer float
        if (Time.timeScale != 0 && !calledCoroutine)
        {
            StartCoroutine("SpinAxeOnDelay");
        }

        if (axeShouldSpin)
            axeTransform.RotateAround(rotationPoint.position, new Vector3(0f, 0f, 1f), rotationSpeed * Time.deltaTime);
    }

    private IEnumerator SpinAxeOnDelay()
    {
        calledCoroutine = true;
        yield return new WaitForSeconds(waitTimer);
        axeShouldSpin = true;
    }


    private void OnTriggerEnter2D(Collider2D collider) 
    {
        string collision = collider.ToString();
        collisionTag = collider.tag;

        // Anything that isn't a player
        if (collision.Contains("KingoftheHill") || collision.Contains(this.name) || collision.Contains("Cannon") || collision.Contains("Composite"))
            return;

        // A player's head detection is a child of the player prefab therefore we need to get the gameobject of the parent
        if (collision.Contains("HeadDetect") || collision.Contains("Feet"))
            player = collider.transform.parent.gameObject;
            
        // If the collider still belongs to the player it is directly a part of the player GameObject/prefab
        else
            player = collider.gameObject;
        



        if (collisionTag.Contains("Player") && hasCollided == false)
        {
            player.GetComponent<Stats>().TakeDamageAnonomously(1);
        }
    }
}
