using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneOnTrigger : MonoBehaviour
{
    [SerializeField]private String sceneToLoad;
    [SerializeField] private Boolean exit; 
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerBehavior>())
        {
            if (!exit)
            {
                SceneManager.LoadScene(sceneToLoad);
            }
            else
            {
                Application.Quit();
            }
        }
    }
}
