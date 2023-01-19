using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GostAnimation : MonoBehaviour
{
    [SerializeField] Rigidbody gostrigidbody;
    [SerializeField] private float minDistancePerFrame = 0.001f;
    private Animator animator;
    private bool isRunning;
    private Vector3 prefPos;
    
    private void Awake()
    {
        animator = GetComponent<Animator>();
        isRunning = false;
        prefPos = Vector3.zero;
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        float distance = Vector3.Distance(prefPos, gostrigidbody.position);
        if (!isRunning && distance > minDistancePerFrame )
        {
            animator.SetTrigger("Trigger_Walk_Forward");
            isRunning = true;
        }
        if (isRunning && distance <= minDistancePerFrame )
        {
            animator.SetTrigger("Trigger_Idle");
            isRunning = false;
        }

        prefPos = gostrigidbody.position;
    }
    
    
    
    
    
}