using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneOnTrigger : MonoBehaviour
{
    [SerializeField]private String sceneToLoad;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerBehavior>())
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
