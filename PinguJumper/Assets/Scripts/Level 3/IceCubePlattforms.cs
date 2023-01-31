using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class IceCubePlattforms : MonoBehaviour
{
    private bool permenantlyVisible = false;

    private bool tempvisible = false;
    // Start is called before the first frame update

    public void Start()
    {
        GetComponent<Renderer>().enabled = false;
        GetComponentsInChildren<Renderer>()[1].enabled = false;
    }

    public void changeVisibilityPermenantly(bool permenant)
    {
        permenantlyVisible = permenant;
        GetComponent<Renderer>().enabled = permenantlyVisible;
        transform.GetChild(0).GetComponent<Renderer>().enabled = permenantlyVisible;
        
    }

    public void changeVisibilityTemp(bool visible)
    {
        tempvisible = visible;
        if (tempvisible)
        {
            GetComponent<Renderer>().enabled = true;
            transform.GetChild(0).GetComponent<Renderer>().enabled = true;
        }
        else
        {
            if (!permenantlyVisible)
            {
                GetComponent<Renderer>().enabled = false;
                transform.GetChild(0).GetComponent<Renderer>().enabled = false;
            }
        }
    }
}
