using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{

    private void OnDrawGizmos()
    {
       Gizmos.DrawIcon(transform.position,"deathzone" );
    }
    
}
