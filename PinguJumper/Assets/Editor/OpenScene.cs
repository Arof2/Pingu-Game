using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using System.Collections;

public class OpenScene 
{
    [MenuItem("Open Scene/Player Movement test %#&0")]
    private static void StartScreen()
    {
        EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
        EditorSceneManager.OpenScene("Assets/Scenes/Player Movement Test.unity");
    }
    
    [MenuItem("Open Scene/Level 2 %#&1")]
    private static void Level1()
    {
        EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
        EditorSceneManager.OpenScene("Assets/Scenes/Level 2.unity");
    }
    
    [MenuItem("Open Scene/Main Hub %#&1")]
    private static void MainHub()
    {
        EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
        EditorSceneManager.OpenScene("Assets/Scenes/Main_HUB.unity");
    }
    
}
