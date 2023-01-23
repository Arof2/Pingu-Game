using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class CheckforWin : MonoBehaviour
{
    // Start is called before the first frame update
    private bool wonThisLevel = false;
    private String current;
    void Start()
    {
        //Debug.Log("Saved");
        DontDestroyOnLoad(this);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnLoad;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnLoad;
    }

    public void wonLevel()
    {
        wonThisLevel = true;
    }

    private void OnLoad(Scene scene, LoadSceneMode mode)
    {
        EventSystem evsystem = GameObject.FindObjectOfType<EventSystem>();
        if (!evsystem)
        {
            GameObject g = new GameObject();
            g.AddComponent<EventSystem>();
            g.AddComponent<StandaloneInputModule>();
            g.name = "temporäres Eventsystem";
        }
             
        
        if (scene.Equals(SceneManager.GetSceneByName("Main_HUB")))
        {
            if (wonThisLevel)
            {
                GameObject.FindObjectOfType<SaveProgress>().winLevel(current);
                wonThisLevel = false;
            }
        }
        else
        {
            current = scene.name;
        }
    }
}
