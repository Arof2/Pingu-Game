using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class VanishingPlatform : MonoBehaviour
{
    [SerializeField] private float maxTransparency = 0.5f;
    [SerializeField] private float timeTillVanish = 1.0f;
    [SerializeField] private float timeTillUnvanish = 5.0f;
    [SerializeField] private float timeTillreappear = 0.5f;
    [SerializeField] private int amtOfBlinksVanish = 3 ;
    [SerializeField] private int amtOfBlinksUnvanish = 3 ;
    private bool vanishing = false;
    private Color originalColor;
    private MeshRenderer rend;
    private Collider col;

    private void Awake()
    {
        originalColor = GetComponent<Renderer>().material.color;
        rend = GetComponent<MeshRenderer>();
        col = GetComponent<Collider>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && !vanishing)
        {
            vanishing = true;
            StartCoroutine(Vanish());
        }
    }


    private IEnumerator Vanish()
    {
        StartCoroutine(Blink(timeTillVanish, amtOfBlinksVanish));
        yield return new WaitForSeconds(timeTillVanish);
        col.enabled = false;
        rend.material.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
        yield return new WaitForSeconds(timeTillUnvanish);
        col.enabled = true;
        rend.material.color = originalColor;
        vanishing = false;
    }

    private IEnumerator Blink(float time, int amt)
    {
        float timeToWait = time / (amt * 2);
        while (amt>0)
        {
            rend.material.color = new Color(1.0f, 1.0f, 1.0f, maxTransparency);
            yield return new WaitForSeconds(timeToWait);
            rend.material.color = originalColor;
            yield return new WaitForSeconds(timeToWait);
            amt--;
        }
    }
    
    
    
}
