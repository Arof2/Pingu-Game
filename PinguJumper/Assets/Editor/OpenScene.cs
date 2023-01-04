using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using System.Collections;

public class OpenScene 
{
    [MenuItem("Open Scene/Start Screen %#&0")]
    private static void StartScreen()
    {
        EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
        EditorSceneManager.OpenScene("Assets/Scenes/StartScreen.unity");
    }
    
    [MenuItem("Open Scene/Level1 %#&1")]
    private static void Level1()
    {
        EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
        EditorSceneManager.OpenScene("Assets/Scenes/Level1.unity");
    }
    
}
