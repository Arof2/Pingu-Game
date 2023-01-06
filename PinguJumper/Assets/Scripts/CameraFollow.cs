using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform followdObject;
    [SerializeField] private float smooth = 0f;
    [SerializeField] private float distanceAway, distanceUp;
    
    void Update()
    {
        Vector3 nPosition = followdObject.position + (-followdObject.forward * distanceAway) + (followdObject.up * distanceUp);

        transform.position = Vector3.Lerp(transform.position, nPosition, smooth * Time.deltaTime);
       transform.LookAt((followdObject));
    }
}
