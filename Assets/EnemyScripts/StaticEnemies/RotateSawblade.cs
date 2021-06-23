using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateSawblade : MonoBehaviour
{
    // rotationSpeed is in units of angle (degrees) / second meaning the sawblade will have rotated 720degrees after 1 second
    [SerializeField] public float rotationSpeed = 720f;
    private Transform sawBlade;
    // Start is called before the first frame update
    void Start()
    {
        sawBlade = this.transform;
    }

    // Update is called once per frame
    void Update()
    {
        sawBlade.transform.RotateAround(sawBlade.transform.position, new Vector3(0f, 0f, 1f), rotationSpeed * Time.deltaTime);      
    }
}
