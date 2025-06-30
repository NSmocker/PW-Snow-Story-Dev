using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

public class PrefabPaletteWindow : EditorWindow
{
    public List<GameObject> palette = new List<GameObject>();
    private Vector2 scrollPos;

    [MenuItem("Tools/Prefab Palette")]
    public static void ShowWindow()
    {
        GetWindow<PrefabPaletteWindow>("Prefab Palette");
    }

    void OnGUI()
    {
        GUILayout.Label("Prefab Palette", EditorStyles.boldLabel);
        EditorGUILayout.HelpBox("Додавайте префаби у палітру для швидкого доступу.", MessageType.Info);

        // Додаємо новий слот
        if (GUILayout.Button("Додати слот"))
        {
            palette.Add(null);
        }

        // Відображаємо палітру
        scrollPos = EditorGUILayout.BeginScrollView(scrollPos);
        for (int i = 0; i < palette.Count; i++)
        {
            EditorGUILayout.BeginHorizontal();
            palette[i] = (GameObject)EditorGUILayout.ObjectField($"Prefab {i+1}", palette[i], typeof(GameObject), false);
            if (GUILayout.Button("Видалити", GUILayout.Width(70)))
            {
                palette.RemoveAt(i);
                i--;
            }
            EditorGUILayout.EndHorizontal();
        }
        EditorGUILayout.EndScrollView();

        // Зберегти палітру у ScriptableObject (можна розширити)
        if (GUILayout.Button("Зберегти палітру (тимчасово у EditorPrefs)"))
        {
            SavePalette();
        }
        if (GUILayout.Button("Завантажити палітру"))
        {
            LoadPalette();
        }
    }

    void SavePalette()
    {
        // Зберігаємо шляхи до префабів у EditorPrefs
        List<string> paths = new List<string>();
        foreach (var prefab in palette)
        {
            if (prefab != null)
                paths.Add(AssetDatabase.GetAssetPath(prefab));
            else
                paths.Add("");
        }
        EditorPrefs.SetString("PrefabPalette", string.Join("|", paths));
        Debug.Log("Палітру збережено!");
    }

    void LoadPalette()
    {
        palette.Clear();
        string data = EditorPrefs.GetString("PrefabPalette", "");
        if (!string.IsNullOrEmpty(data))
        {
            string[] paths = data.Split('|');
            foreach (var path in paths)
            {
                if (!string.IsNullOrEmpty(path))
                    palette.Add(AssetDatabase.LoadAssetAtPath<GameObject>(path));
                else
                    palette.Add(null);
            }
        }
        Debug.Log("Палітру завантажено!");
    }
}
