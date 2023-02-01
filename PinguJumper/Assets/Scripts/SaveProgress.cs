using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

[System.Serializable]
class savingStruct
{
    public String levelName;
    public GameObject toBeActivated;
    public bool won;
    
}
public class SaveProgress : MonoBehaviour
{
     [SerializeField] private List<savingStruct> saves;

    private void Awake()
    {
        UpdateTheModels();
    }
    private void OnDisable()
    {
        for (int i = 0; i < saves.Count; i++)
        {
            PlayerPrefs.SetInt(saves[i].levelName,saves[i].won == false ? 0 : 1);
        }
    }

    public void winLevel(string sceneName)
    {
        for (int i = 0; i < saves.Count; i++)
        {
            if (saves[i].levelName == sceneName)
                saves[i].won = true;
        }
        UpdateTheModels();
    }

    public void UpdateTheModels()
    {
        for (int i = 0; i < saves.Count; i++)
        {
            saves[i].won = PlayerPrefs.GetInt(saves[i].levelName, 0) != 0;
        }

        for (int i = 0; i < saves.Count; i++)
        {
            if (saves[i].toBeActivated != null)
            {
                saves[i].toBeActivated.SetActive(saves[i].won);
            }
        }
    }
}
