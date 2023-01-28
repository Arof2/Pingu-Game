using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class MissingMirror : MonoBehaviour
{
    [SerializeField] private GameObject text, mirror;
    [SerializeField] private TextMeshProUGUI tmpText;
    private bool nearEnough = false;

    private void Awake()
    {
        text.SetActive(false);
        mirror.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            text.SetActive(true);
            if (other.GetComponent<MirrorForPlayer>().hasMirror())
            {
                nearEnough = true;
                tmpText.text = "press E to place Pirsm";
            }
            else
                tmpText.text = "Missing Prism";
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            nearEnough= false;
            text.SetActive(false);
        }
    }

    private void Update()
    {
        if(nearEnough && Input.GetKeyDown(KeyCode.E))
        {
            mirror.SetActive(true);
            GetComponent<Collider>().enabled = false;
            text.SetActive(false);
            GameObject.FindObjectOfType<MirrorForPlayer>().RequestMirror();
        }
    }
}
