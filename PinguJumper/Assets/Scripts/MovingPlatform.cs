using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MovingPlatform : MonoBehaviour
{
    public Vector3 endPosition;
    private Vector3 startPosition;
    public float speed;
    private bool toEnd = true;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        startPosition=transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        var nextPosition = toEnd ? startPosition + endPosition : startPosition;
        transform.position = Vector3.MoveTowards(transform.position, nextPosition, speed * Time.deltaTime);
        if (transform.position.Equals(nextPosition))
        {
            toEnd = !toEnd;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color= Color.red;
        if (Application.isPlaying)
        {
            Gizmos.DrawLine(startPosition,startPosition+endPosition);
        }
        else
        {
            Gizmos.DrawLine(transform.position,transform.position+endPosition);
        }
    }
}
