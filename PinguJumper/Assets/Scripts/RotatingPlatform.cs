using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingPlatform : MonoBehaviour
{
    [SerializeField] private float rotationSpeed;
    [SerializeField] private bool localValues;
    [SerializeField] private Vector3 rotationPoint = Vector3.zero;
    [SerializeField] private Vector3 rotationAxis = Vector3.up;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (localValues)
        {
            Vector3 nrotationPoint = transform.TransformPoint(rotationPoint);
            Vector3 nrotationAxis = transform.TransformDirection(rotationAxis);
            transform.RotateAround(nrotationPoint, nrotationAxis, rotationSpeed * Time.deltaTime);
        }
        else
        {
            transform.RotateAround(rotationPoint, rotationAxis, rotationSpeed * Time.deltaTime);
        }

    }
}
