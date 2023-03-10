using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBox : MonoBehaviour
{
    [SerializeField] private Transform spawn;
    // Start is called before the first frame update

    public void Spawn()
    {
        transform.position = spawn.position;
    }
    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DeathZone"))
        {
            Spawn();
        }
    }
}
