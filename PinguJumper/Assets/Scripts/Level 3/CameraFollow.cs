using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]private Transform followedObject;
    public float distanceAway;
    public float distanceUp;
    public float smooth;
    

    private void LateUpdate()
    {
        var toPosition = followedObject.position - followedObject.forward * distanceAway + followedObject.up * distanceUp;
        transform.position = Vector3.Lerp(transform.position, toPosition, smooth * Time.deltaTime);
        transform.LookAt(followedObject);
    }
}
