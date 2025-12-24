using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

public class SceneSwitcher
{
    [MenuItem("Scenes/MainMenu")]
    public static void LoadMainMenu()
    {
        var scenePath = @"Assets/Source/Scenes/MainMenu.unity";
        Debug.Log("Loading scene: Loading from path: " + scenePath);

        if (Application.isPlaying) return;
        EditorSceneManager.OpenScene(scenePath);
    }

    [MenuItem("Scenes/Game")]
    public static void LoadGame()
    {
        var scenePath = @"Assets/Source/Scenes/Game.unity";
        Debug.Log("Loading scene: Menu from path: " + scenePath);

        if (Application.isPlaying) return;
        EditorSceneManager.OpenScene(scenePath);
    }

}