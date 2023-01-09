using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Cinemachine;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
  [SerializeField] private InputSettings inSettings;
  [SerializeField] private MovementSettings movSettings;
  [SerializeField] private Transform spawnpoint;
  private Rigidbody playerRigidbody;
  private Vector3 velocity;
  private Quaternion targetRotation;
  private float forwardInput, sidwaysInput, turnInput, jumpInput;
  private Vector3 baseScale, direction;
  private float turnSmoothVelocity;
  
  
   //input settings
   [System.Serializable]
   public class InputSettings
   {
      public string FORWARD_AXIS = "Vertical";
      public string SIDEWAYS_AXIS = "Horizontal";
      public string TURN_AXIS = "Mouse X";
      public string JUMP_AXIS = "Jump";
   }
   
   
   //movement settings
   [System.Serializable]
   public class MovementSettings
   {
      public float runVelocity = 12;
      public float rotateVelocity = 100;
      public float jumpVelocity = 16;
      public float distanceToGround = 1.3f;
      public float turnSmoothTime = 0.1f;
      public LayerMask ground;
   }
   //input values


   private void Awake()
   {
      //initalze values
      playerRigidbody = GetComponent<Rigidbody>();
      velocity = new Vector3(0f,0f,0f);
      targetRotation = transform.rotation;
      forwardInput = 0f;
      sidwaysInput = 0f;
      turnInput = 0f;
      jumpInput = 0f;
   }


   private void Update()
   {
      GetInput();
      Turn();
   }

   private void FixedUpdate()
   {
      Run();
      Jump();
   }

   private void GetInput()
   {
      forwardInput = -Input.GetAxisRaw(inSettings.FORWARD_AXIS);
      sidwaysInput = Input.GetAxisRaw(inSettings.SIDEWAYS_AXIS);
      turnInput = Input.GetAxis(inSettings.TURN_AXIS);
      jumpInput = Input.GetAxis(inSettings.JUMP_AXIS);
   }

   private void Turn()
   {
      if (sidwaysInput != 0 || forwardInput != 0)
      {
         Transform G = Camera.main.gameObject.transform;
         direction = new Vector3(forwardInput, 0f, sidwaysInput).normalized;
         float yRotation = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
         float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, yRotation + G.eulerAngles.y, ref turnSmoothVelocity, movSettings.turnSmoothTime);
         transform.rotation = Quaternion.Euler(0,angle,0);
      }
      else
      { 
         //if the player is angainst a wall his angular velocity.y will increase. That makes him just rotate
         playerRigidbody.angularVelocity = new Vector3(playerRigidbody.angularVelocity.x, 0, playerRigidbody.angularVelocity.z);
      }
   }

   private void Run()
   {
      //We just set the velocity to forward so he only ever walks forward
      //but thats fine cause he gets rotated the right way in the Turn() function
      if (sidwaysInput != 0 || forwardInput != 0)
      {
         playerRigidbody.velocity = transform.TransformDirection(new Vector3(1 * movSettings.runVelocity, playerRigidbody.velocity.y, 0));
      }
      else
      {
         playerRigidbody.velocity = transform.TransformDirection(new Vector3(0, playerRigidbody.velocity.y, 0));
      }
      
   }

   private void Jump()
   {
      if (jumpInput>0f && Grounded())
      {
         velocity.x = playerRigidbody.velocity.x;
         velocity.y = movSettings.jumpVelocity;
         velocity.z = playerRigidbody.velocity.z;

         playerRigidbody.velocity = velocity;
      }
   }

   private bool Grounded()
   {
      return Physics.Raycast(transform.position, Vector3.down, movSettings.distanceToGround, movSettings.ground);
   }
   
   private void OnCollisionEnter(Collision other)
   {
      if (transform.parent != null)
      {
         GameObject R = transform.parent.gameObject;
         transform.SetParent(null);
         Destroy(R);
      }
      GameObject G = new GameObject();
      G.transform.SetParent(other.transform.parent.transform, true);
      transform.SetParent(G.transform,false);
   }
   private void OnCollisionExit(Collision other)
   {
      if (other.transform.parent.gameObject == transform.parent.parent.gameObject)
      {
         GameObject G = transform.parent.gameObject;
         transform.SetParent(null);
         Destroy(G);
      }
   }


   private void Start()
   {
      Spawn();
   }

   public void Spawn()
   {
      transform.position = spawnpoint.position;
   }

   private void OnTriggerEnter(Collider other)
   {
      if (other.CompareTag("DeathZone"))
      {
         Spawn();
      }
   }
}
