using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Limitrotation : MonoBehaviour
{
    private Rigidbody rig;
    private void Start()
    {
        rig = GetComponent<Rigidbody>();
    }
    void Update()
    {
        if (transform.eulerAngles.x < 70)
            transform.eulerAngles = new Vector3( 70, transform.eulerAngles.y, transform.eulerAngles.z);
    }

    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            rig.velocity = collision.gameObject.GetComponent<Rigidbody>().velocity;
        }
    }
}
