using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateLight : MonoBehaviour
{
    private Light currentLight;
    private bool nearEnough = false, activated= false;
    private float startIntesity;
    [SerializeField]private float speed = 2;
    [SerializeField]private GameObject text;
    [SerializeField]private bool automaticActivation = false, withParticleSystem = true;
    [SerializeField] private ParticleSystem parts;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            nearEnough = true;
            if(!automaticActivation)
                text.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            nearEnough = false;
            if(!automaticActivation)
                text.SetActive(false);
        }
    }

    private void Update()
    {
        if ((nearEnough && (Input.GetKeyDown(KeyCode.E) || automaticActivation)) && !activated)
        {
            gameObject.GetComponent<Collider>().enabled = false;
            if(!automaticActivation)
                text.SetActive(false);
            StartCoroutine(activateLight());
            if(withParticleSystem)
                if(!parts.isPlaying)
                    parts.Play();

            activated = true;
        }
    }

    private void Awake()
    {
        currentLight = GetComponent<Light>();
        currentLight.enabled = true;
        startIntesity = currentLight.intensity;
        currentLight.intensity = 0;
        if(!automaticActivation)
            text.SetActive(false);
        if(withParticleSystem)
            parts.Stop();
    }

    IEnumerator activateLight()
    {
        for (int i = 0; i < 50; i++)
        {
            yield return new WaitForSeconds(0.1f);
            currentLight.intensity = Mathf.Lerp(currentLight.intensity, startIntesity, Time.deltaTime * speed);
        }

        currentLight.intensity = startIntesity;
    }
}
