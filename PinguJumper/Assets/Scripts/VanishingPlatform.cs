using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class VanishingPlatform : MonoBehaviour
{
    [SerializeField] private float maxTransparency = 0.5f;
    [SerializeField] private float timeTillVanish = 1.0f;
    [SerializeField] private float timeTillUnvanish = 5.0f;
    [SerializeField] private float timeTillreappear = 0.5f;
    [SerializeField] private int amtOfBlinksVanish = 3 ;
    [SerializeField] private int amtOfBlinksUnvanish = 3 ;

    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
       
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        StartCoroutine(Vanish());
    }


    private IEnumerator Vanish()
    {
        StartCoroutine(Blink(timeTillVanish, amtOfBlinksVanish));
        yield return new WaitForSeconds(timeTillVanish);
        GetComponent<Collider>().enabled = false;
        GetComponent<Renderer>().material.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
        yield return new WaitForSeconds(timeTillUnvanish);
        StartCoroutine(Blink(timeTillreappear, amtOfBlinksUnvanish));
        GetComponent<Collider>().enabled = false;
        GetComponent<Renderer>().material.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
    }

    private IEnumerator Blink(float time, int amt)
    {
        float timeToWait = time / (amt * 2);
        while (amt>0)
        {

            GetComponent<Renderer>().material.color = new Color(1.0f, 1.0f, 1.0f, maxTransparency);
            yield return new WaitForSeconds(timeToWait);
            GetComponent<Renderer>().material.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            yield return new WaitForSeconds(timeToWait);
            amt--;
        }
    }
    
    
    
}
