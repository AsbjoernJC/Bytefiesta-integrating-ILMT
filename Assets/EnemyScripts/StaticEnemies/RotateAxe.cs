using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAxe : MonoBehaviour
{
    [SerializeField] public Transform rotationPoint;

    // rotationSpeed is in units of angle (degrees) / second
    [SerializeField] public float rotationSpeed = 60f;
    private Transform axeTransform;
    private void Awake()
    {
        axeTransform = this.gameObject.transform;
    }


    // Update is called once per frame
    private void Update()
    {
        axeTransform.RotateAround(rotationPoint.position, new Vector3(0f, 0f, 1f), rotationSpeed * Time.deltaTime);
    }
}
