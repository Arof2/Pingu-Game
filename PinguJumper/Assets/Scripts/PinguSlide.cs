using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.UI;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class PinguSlide : MonoBehaviour
{

    [SerializeField] private GameObject animatedPingu;
    [SerializeField] private float speed;
    [SerializeField] private float SlideAngle = 45;
    [SerializeField] private float DistancetoGroundModifier = 0;
    [SerializeField] private float velotcityToStandUp = 0.5f;
    [SerializeField] private float StartHeight = 0.1f;
    [SerializeField] private float HeightOffset = 0.1f;
    [SerializeField] private float airtime;
    [SerializeField] private float degreesPerFrame = 90.0f;
    private PlayerBehavior playerBehavior;
    private Rigidbody playerRigidbody;
    private Quaternion defaultOffset;
    private bool justSlide;
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        playerBehavior = GetComponent<PlayerBehavior>();
        playerRigidbody = GetComponent<Rigidbody>();
        defaultOffset = animatedPingu.transform.localRotation;
        justSlide = false;
        timer = airtime;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        //if grounded (comp. PlayerBehavior.Grounded()
        if (Physics.Raycast(transform.position, Vector3.down, out hit, playerBehavior.movSettings.distanceToGround +DistancetoGroundModifier,
                playerBehavior.movSettings.ground))
        {
            float angle = Vector3.Angle(hit.normal, Vector3.up);
            if ((angle<85 &&angle > SlideAngle) || (justSlide && playerRigidbody.velocity.magnitude>velotcityToStandUp && angle> 5))
            {
                if (justSlide == false)
                {
                    playerRigidbody.position += Vector3.up * StartHeight;
                    animatedPingu.transform.position += new Vector3(0.0f, HeightOffset, 0.0f); 
                    playerRigidbody.velocity = playerRigidbody.velocity * 0.5f;
                    playerBehavior.changePlayerControl(false, true, false);
                }
                Vector3 downDirection = Vector3.Cross(hit.normal, Vector3.Cross(Vector3.down, hit.normal));
                playerRigidbody.velocity += downDirection.normalized*(angle*speed*Time.deltaTime*0.001f);
                animatedPingu.transform.rotation = Quaternion.RotateTowards(animatedPingu.transform.rotation, Quaternion.LookRotation((hit.normal*-1),downDirection),degreesPerFrame);
                justSlide = true;
                timer = airtime;
            }
            else
            {
                if (justSlide)
                {
                    animatedPingu.transform.position -= new Vector3(0.0f, HeightOffset, 0.0f); 
                    playerBehavior.changePlayerControl(true, true, false);
                    Vector3 lookDirection = playerRigidbody.velocity;
                    lookDirection.y = 0;
                    animatedPingu.transform.localRotation = defaultOffset;
                    justSlide = false;

                }
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
                    animatedPingu.transform.position -= new Vector3(0.0f, HeightOffset, 0.0f); 
                    playerBehavior.changePlayerControl(true, true, false);
                    Vector3 lookDirection = playerRigidbody.velocity;
                    lookDirection.y = 0;
                    animatedPingu.transform.localRotation = defaultOffset;
                    justSlide = false;
                }
            }
        }
    }
}
