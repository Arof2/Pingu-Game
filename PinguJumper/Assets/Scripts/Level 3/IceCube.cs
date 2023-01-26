using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceCube : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("IceBox"))
        {
            foreach (GeneigtePlattform plattforms in FindObjectsOfType<GeneigtePlattform>())
            {
                plattforms.changeVisibility(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        foreach (GeneigtePlattform plattforms in FindObjectsOfType<GeneigtePlattform>())
        {
            plattforms.changeVisibility(false);
        }
    }
}
