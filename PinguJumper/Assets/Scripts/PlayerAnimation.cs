using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] Rigidbody playerRigidbody;
    private Animator animator;
    private bool isRunning; 
    
    private void Awake()
    {
        animator = GetComponent<Animator>();
        isRunning = false;
    }


    // Update is called once per frame
    void Update()
    {
        float speed = playerRigidbody.velocity.magnitude;
        if (!isRunning && speed > 0.1f )
        {
            animator.SetTrigger("Trigger_Walk_Forward");
            isRunning = true;
        }
        if (isRunning && speed <= 0.1f )
        {
            animator.SetTrigger("Trigger_Idle");
            isRunning = false;
        }
        
    }
    
    
    
    
    
}