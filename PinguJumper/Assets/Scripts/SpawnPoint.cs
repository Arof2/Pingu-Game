using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;

    private void Start()
    {
        Spawn();
    }

    private void Spawn()
    {
        transform.position = spawnPoint.position;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawIcon(spawnPoint.position,"SpawnPoint");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DeathZone"))
        {
            Spawn();
        }
    }
}
