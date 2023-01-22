using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class MissingMirror : MonoBehaviour
{
    [SerializeField] private GameObject text, mirror;
    [SerializeField] private TextMeshProUGUI tmpText;

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
                tmpText.text = "press E to place Pirsm";
            else
                tmpText.text = "Missing Prism";
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.E) && other.CompareTag("Player") && other.GetComponent<MirrorForPlayer>().hasMirror())
        {
            mirror.SetActive(true);
            GetComponent<Collider>().enabled = false;
            text.SetActive(false);
            other.GetComponent<MirrorForPlayer>().RequestMirror();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            text.SetActive(false);
        }
    }
}
