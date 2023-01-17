using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorRotates : MonoBehaviour
{
    public void Update()
    {
        transform.rotation *= Quaternion.Euler(0,1,0);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //Add the Mirror to the players inventory or place it on its correct spot
        }
    }
}
