using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartAmbient : MonoBehaviour
{
    private AudioManager manager;
    private void Awake()
    {
        manager = FindObjectOfType<AudioManager>();
        manager.playSound("Cave ambient");
    }

    private void OnDestroy()
    {
        manager.stopLoop("Cave ambient");
    }
}
