using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacePillar : MonoBehaviour
{
    public GameObject pillar;
    private bool done = false;

    private void Start()
    {
        pillar.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<Limitrotation>() && !done)
        {
            other.gameObject.SetActive(false);
            pillar.SetActive(true);
            done = true;
        }
    }
}
