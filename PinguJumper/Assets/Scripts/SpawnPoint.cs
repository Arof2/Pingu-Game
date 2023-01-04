using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    
    // Start is called before the first frame update

    private void OnDrawGizmos()
    {
        Gizmos.DrawIcon(transform.position,"spawnpoint" );
    }

    
}
