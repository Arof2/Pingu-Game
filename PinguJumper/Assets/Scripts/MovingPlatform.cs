using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private Vector3 startPosition;
    [SerializeField] private Vector3 endPosition;

    [SerializeField] private float speed;
    private bool toEnd;
    
    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        toEnd = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 nextPosition = toEnd ? startPosition + endPosition : startPosition;
        Vector3 amtMove = (nextPosition - transform.position).normalized;
        amtMove *= Time.deltaTime * speed;
        transform.Translate(amtMove, Space.World);

        if(Vector3. Distance(nextPosition, transform.position)<amtMove.magnitude)
        toEnd = !toEnd;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if (Application.isPlaying)
        {
            Gizmos.DrawLine(startPosition, startPosition+endPosition);
        }
        else
        {
            Gizmos.DrawLine(transform.position,transform.position + endPosition);
        }
    }
}
