using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LostPinguAnimation : MonoBehaviour
{
    [SerializeField] Rigidbody playerRigidbody;
    [SerializeField] private Rigidbody pinguRigidbody;
    [SerializeField] private float excitedDistance = 5.0f;
    private Animator animator;
    private bool isHappy; 
    
    private void Awake()
    {
        animator = GetComponent<Animator>();
        isHappy = false;
        animator.SetTrigger("Trigger_Sad");
    }


    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(playerRigidbody.position, pinguRigidbody.position);
        if (isHappy && distance>excitedDistance )
        {
            animator.SetTrigger("Trigger_Sad");
            isHappy = false;
        }
        if (!isHappy && distance <= excitedDistance )
        {
            animator.SetTrigger("Trigger_Excited");
            isHappy = true;
        }
        transform.LookAt(playerRigidbody.position + new Vector3(0.0f, -0.85f, 0.0f));
        
    }
    
    
    
    
    
}