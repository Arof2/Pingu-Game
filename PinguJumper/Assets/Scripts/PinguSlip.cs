using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.UI;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class PinguSlip : MonoBehaviour
{
    
    [SerializeField] private float speed;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float SlideAngle = 45;
    [SerializeField] private float DistancetoGroundModifier = 0;
    [SerializeField] private float velotcityToStandUp = 0.5f;
    [SerializeField] private float airtime;
    [SerializeField] private float runSpeedMultiplier= 0.2f;
    [SerializeField] private float jumpHeightOffset = 0.2f;
    private PlayerBehavior playerBehavior;
    private Rigidbody playerRigidbody;
    private bool justSlide;
    private float timer;
    private bool collided = false;

    // Start is called before the first frame update
    void Start()
    {
        playerBehavior = GetComponent<PlayerBehavior>();
        playerRigidbody = GetComponent<Rigidbody>();
        justSlide = false;
        timer = airtime;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerRigidbody.velocity.magnitude > maxSpeed)
        {
            playerRigidbody.velocity = playerRigidbody.velocity.normalized * maxSpeed;
        }
        
        
        if (collided)
        {
            playerBehavior.GetInput();
            RaycastHit hit;
            //if grounded (comp. PlayerBehavior.Grounded()
            if (Physics.Raycast(transform.position, Vector3.down, out hit,
                    playerBehavior.movSettings.distanceToGround + DistancetoGroundModifier,
                    playerBehavior.movSettings.ground))
                
            {
                float angle = Vector3.Angle(hit.normal, Vector3.up);
                Vector3 downDirection = Vector3.Cross(hit.normal, Vector3.Cross(Vector3.down, hit.normal));
                RunAndSlide(downDirection, hit.normal);
                if ((angle < 85 && angle > SlideAngle) ||
                    (justSlide && playerRigidbody.velocity.magnitude > velotcityToStandUp && angle > 5))
                {
                    
                    playerRigidbody.velocity += downDirection.normalized * (angle * speed * Time.deltaTime);
                    if (playerRigidbody.velocity.magnitude > maxSpeed)
                    {
                        playerRigidbody.velocity = playerRigidbody.velocity.normalized * maxSpeed;
                    }
                    
                    
                    justSlide = true;
                    timer = airtime;
                }
                else
                {
                    collided = false;
                }

            }
            else
            {
                collided = false;
            }
        }
        else
        {
            if (justSlide)
            {
                if (timer > 0.0f)
                {
                    timer -= Time.deltaTime;
                }
                else
                {
                    collided = false;
                    justSlide = false;
                    playerBehavior.changePlayerControl(true, true, false);
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
       if(other.gameObject.CompareTag("SlidingSurface"))
       {
           collided = true;
           playerBehavior.changePlayerControl(false, true, false);
           playerRigidbody.velocity = playerRigidbody.velocity * 0.5f;
       }
    }


    private void RunAndSlide(Vector3 downslope, Vector3 groundnormal)
    {
        Quaternion rot = Quaternion.LookRotation(downslope, Vector3.up);
        Vector3 rotEA = rot.normalized.eulerAngles;
        rotEA.y = 0;
        playerRigidbody.rotation = Quaternion.Euler(rotEA.normalized);

        Vector3 inputVector = playerBehavior.forwardInput!= 0? runSpeedMultiplier*(downslope.normalized):Vector3.zero;
        inputVector+= transform.TransformDirection(playerBehavior.sidwaysInput!= 0? new Vector3(0.0f, 0.0f, -playerBehavior.sidwaysInput).normalized*runSpeedMultiplier:Vector3.zero);
        inputVector += playerBehavior.jumpInput != 0 ? Vector3.up* playerBehavior.movSettings.jumpVelocity * jumpHeightOffset: Vector3.zero;
        playerRigidbody.velocity += inputVector;
    }



}
