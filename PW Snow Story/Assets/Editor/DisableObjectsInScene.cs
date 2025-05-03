using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DisableObjectsInScene : EditorWindow
{
    private string scenePath = "Assets/Scenes/YourHeavyScene.unity";

    [MenuItem("Tools/Disable All Objects In Scene")]
    public static void ShowWindow()
    {
        GetWindow<DisableObjectsInScene>("Disable Scene Objects");
    }

    private void OnGUI()
    {
        GUILayout.Label("Disable All GameObjects In Scene", EditorStyles.boldLabel);

        scenePath = EditorGUILayout.TextField("Scene Path:", scenePath);

        if (GUILayout.Button("Disable and Save"))
        {
            if (!System.IO.File.Exists(scenePath))
            {
                Debug.LogError("Scene not found at path: " + scenePath);
                return;
            }

            DisableObjects(scenePath);
        }
    }

    private static void DisableObjects(string path)
    {
        // Save current scene
        var currentScene = SceneManager.GetActiveScene();

        // Open heavy scene additively
        Scene heavyScene = EditorSceneManager.OpenScene(path, OpenSceneMode.Additive);

        if (!heavyScene.IsValid())
        {
            Debug.LogError("Failed to open the scene.");
            return;
        }

        int count = 0;
        foreach (GameObject obj in heavyScene.GetRootGameObjects())
        {
            obj.SetActive(false);
            count++;
        }

        EditorSceneManager.SaveScene(heavyScene);
        EditorSceneManager.CloseScene(heavyScene, true);

        // Optionally: reload original scene
        EditorSceneManager.OpenScene(currentScene.path, OpenSceneMode.Single);

        Debug.Log($"Disabled {count} root objects in scene: {path}");
    }
}
