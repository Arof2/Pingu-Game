using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorRotates : MonoBehaviour
{
    private float speed = 0.05f;
    public void Update()
    {
        transform.rotation *= Quaternion.Euler(0,speed,0);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<MirrorForPlayer>().AddMirror();
            gameObject.SetActive(false);
        }
    }
}
