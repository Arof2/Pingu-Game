using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Cinemachine;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerBehavior : MonoBehaviour
{
  [SerializeField] private InputSettings inSettings;
  [SerializeField] public MovementSettings movSettings;
  [SerializeField] private Transform spawnpoint;
  [SerializeField] private GameObject uiOveraly;
  [SerializeField] private Slider sensitivitySlider;
  [SerializeField] private TextMeshProUGUI sensitivitySliderValueDisplay;
  private Rigidbody playerRigidbody;
  private Vector3 velocity;
  private Quaternion targetRotation;
  public float forwardInput, sidwaysInput, turnInput, jumpInput, scrollInput, downwardInput;
  private Vector3 baseScale, direction;
  private float turnSmoothVelocity;
  public CinemachineFreeLook cam;
  private float[] startOrbits = new float[3], targetOrbits = new float[3];
  private float startSensitivityAxisY, startSensitivityAxisX;
  private float currentMultiplier = 1;
  private bool godMode = false, inControl = true, mouseSettings = false;
  private float lastMouseSpeed;

  //input settings
   [System.Serializable]
   public class InputSettings
   {
      public string FORWARD_AXIS = "Vertical";
      public string SIDEWAYS_AXIS = "Horizontal";
      public string TURN_AXIS = "Mouse X";
      public string JUMP_AXIS = "Jump";
      public KeyCode godModeDownwards = KeyCode.LeftControl;
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
      [Range(0.25f, 1f)]
      public float zoomIn = 0.8f;
      [Range(1f, 3f)]
      public float zoomOut = 2f;
      public float zoomStepSize = 0.1f;
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
      for (int i = 0; i < 3; i++)
      {
         startOrbits[i] = cam.m_Orbits[i].m_Radius;
         targetOrbits[i] = cam.m_Orbits[i].m_Radius;
      }

      startSensitivityAxisY = cam.m_YAxis.m_MaxSpeed;
      startSensitivityAxisX = cam.m_XAxis.m_MaxSpeed;
   }

   private void ChangeOrbitSpeed(float range)
   {
      float val = range + 0.25f;
      cam.m_XAxis.m_MaxSpeed = startSensitivityAxisX * val;
      cam.m_YAxis.m_MaxSpeed = startSensitivityAxisY * val;
      lastMouseSpeed = range;
   }

   public void returnToHub()
   {
      SceneManager.LoadScene("Main_HUB");
   }

   private void Update()
   {
      if (Input.GetKeyDown(KeyCode.Escape))
      {
         changePlayerControl(mouseSettings,mouseSettings, !mouseSettings);
         uiOveraly.SetActive(!mouseSettings);
         mouseSettings = !mouseSettings;
      }

      if (mouseSettings)
      {
         ChangeOrbitSpeed(sensitivitySlider.value);
         sensitivitySliderValueDisplay.text = Mathf.Round(((sensitivitySlider.value + 0.25f) * 100)) + "%";
      }

      if (inControl)
      {
         KeyCode[] combo = new[] { KeyCode.LeftControl, KeyCode.G };
         if ((Input.GetKeyDown(KeyCode.LeftControl) && Input.GetKey(KeyCode.G)) ||
             (Input.GetKeyDown(KeyCode.G) && Input.GetKey(KeyCode.LeftControl)))
         {
            if (godMode)
            {
               godMode = false;
               playerRigidbody.useGravity = true;
            }
            else
            {
               godMode = true;
               playerRigidbody.useGravity = false;
            }
         }
         GetInput();
         Turn();
         ScrollCamera();
      }
   }

   private void FixedUpdate()
   {
      if (inControl)
      {
         if(godMode)
            Fly();
         else
            Jump();
         Run();
      }
   }

   public void GetInput()
   {
      forwardInput = -Input.GetAxisRaw(inSettings.FORWARD_AXIS);
      sidwaysInput = Input.GetAxisRaw(inSettings.SIDEWAYS_AXIS);
      turnInput = Input.GetAxis(inSettings.TURN_AXIS);
      jumpInput = Input.GetAxis(inSettings.JUMP_AXIS);
      scrollInput = -Input.mouseScrollDelta.y;
      downwardInput = Input.GetKey(inSettings.godModeDownwards) ? 1 : 0;
   }

   private void ScrollCamera()
   {
      if (scrollInput != 0)
      {
         float mult = movSettings.zoomStepSize;
         if (!(currentMultiplier + scrollInput * mult > movSettings.zoomOut ||
               currentMultiplier + scrollInput * mult < movSettings.zoomIn))
         {
            currentMultiplier += scrollInput * mult;
            for (int i = 0; i < 3; i++)
            {
               targetOrbits[i] = startOrbits[i] * currentMultiplier;
            }
         }
      }

      //SmoothMovement of the camera
      for (int i = 0; i < 3; i++)
      {
         cam.m_Orbits[i].m_Radius = Mathf.Lerp(cam.m_Orbits[i].m_Radius, targetOrbits[i], Time.deltaTime * 6);
      }
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

   // allows to disable/enable the control over the player
   //the Camera Cinemachine FreeLook Gameobject will be disabled so that the camera can be used
   public void changePlayerControl(bool playerControls = true, bool freecam = true, bool kinemtatic = true)
   {
      inControl = playerControls;
      cam.gameObject.SetActive(freecam);
      playerRigidbody.isKinematic = kinemtatic;
   }

   private void Fly()
   {
      if (jumpInput>0f || downwardInput > 0)
      {
         velocity.y = movSettings.jumpVelocity * jumpInput - downwardInput *  movSettings.jumpVelocity;
      }
      else
      {
         velocity.y = 0;
      }
      velocity.x = playerRigidbody.velocity.x;
      
      velocity.z = playerRigidbody.velocity.z;
      playerRigidbody.velocity = velocity;
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
      if (!other.gameObject.CompareTag("DontParent"))
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
      if (other.gameObject.CompareTag("InvisiblePlattform"))
      {
         other.gameObject.GetComponent<IceCubePlattforms>().changeVisibilityPermenantly();
      }
   }
   private void OnCollisionExit(Collision other)
   {
      if (!other.gameObject.CompareTag("DontParent"))
      {
         if (other.transform.parent.gameObject == transform.parent.parent.gameObject)
         {
            GameObject G = transform.parent.gameObject;
            transform.SetParent(null);
            Destroy(G);
         }
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

      if (other.CompareTag("Ice"))
      {
         foreach(IceCubePlattforms plattform in FindObjectsOfType<IceCubePlattforms>())
         {
            plattform.changeVisibilityTemp(true);
         }
      }
   }

   private void OnTriggerExit(Collider other)
   {
      if (other.CompareTag("Ice"))
      {
         foreach(IceCubePlattforms plattform in FindObjectsOfType<IceCubePlattforms>())
         {
            plattform.changeVisibilityTemp(false);
         }
      }
   }

   private void OnEnable()
   {
      lastMouseSpeed = float.Parse(PlayerPrefs.GetString("MouseSpeed", "0,75"));
      ChangeOrbitSpeed(lastMouseSpeed);
      sensitivitySlider.value = lastMouseSpeed;
   }

   private void OnDestroy()
   {
      PlayerPrefs.SetString("MouseSpeed", lastMouseSpeed.ToString());
   }
}
