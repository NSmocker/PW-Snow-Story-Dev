using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class FolderShortcut
{
    public DefaultAsset folder;     // перетягни папку з Project
    public string label = "Перейти"; // підпис кнопки (можна змінити)
}

public class ProjectFolderShortcutsData : ScriptableObject
{
    public List<FolderShortcut> shortcuts = new List<FolderShortcut>();
}

public class ProjectFolderShortcutsWindow : EditorWindow
{
    private const string assetPath = "Assets/Editor/ProjectFolderShortcutsData.asset";
    private ProjectFolderShortcutsData data;

    [MenuItem("Window/Custom/Project Folder Shortcuts")]
    public static void ShowWindow()
    {
        GetWindow<ProjectFolderShortcutsWindow>("Folder Shortcuts");
    }

    private void OnEnable()
    {
        LoadOrCreateData();
    }

    private void OnGUI()
    {
        if (data == null)
        {
            EditorGUILayout.HelpBox("Не вдалося завантажити дані", MessageType.Error);
            if (GUILayout.Button("Створити дані"))
                CreateDataAsset();
            return;
        }

        EditorGUILayout.LabelField("Шорткати до папок у Project", EditorStyles.boldLabel);
        EditorGUILayout.Space();

        // Кнопки зверху
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("+ Додати шорткат", GUILayout.Width(140)))
        {
            Undo.RecordObject(data, "Add Shortcut");
            data.shortcuts.Add(new FolderShortcut());
            SaveData();
        }
        if (GUILayout.Button("Зберегти", GUILayout.Width(80)))
        {
            SaveData();
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space();

        // Список шорткатів
        for (int i = 0; i < data.shortcuts.Count; i++)
        {
            var s = data.shortcuts[i];
            EditorGUILayout.BeginHorizontal();

            // Поле для вибору папки (обробка змін)
            EditorGUI.BeginChangeCheck();
            var newFolder = (DefaultAsset)EditorGUILayout.ObjectField(s.folder, typeof(DefaultAsset), false);
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(data, "Change Shortcut Folder");
                s.folder = newFolder;
                SaveData();
            }

            // Редагування підпису кнопки
            EditorGUI.BeginChangeCheck();
            var newLabel = EditorGUILayout.TextField(s.label, GUILayout.Width(140));
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(data, "Change Shortcut Label");
                s.label = newLabel;
                SaveData();
            }

            // Кнопка переходу (деактивована якщо папка не вказана)
            GUI.enabled = s.folder != null;
            if (GUILayout.Button(s.label, GUILayout.Width(90)))
            {
                OpenFolderInProject(s.folder);
            }
            GUI.enabled = true;

            // Видалити рядок
            if (GUILayout.Button("X", GUILayout.Width(25)))
            {
                Undo.RecordObject(data, "Remove Shortcut");
                data.shortcuts.RemoveAt(i);
                SaveData();
                EditorGUILayout.EndHorizontal();
                // перериваємо, щоб уникнути проблем з індексами під час OnGUI
                break;
            }

            EditorGUILayout.EndHorizontal();
        }
    }

    private void OpenFolderInProject(DefaultAsset folder)
    {
        if (folder == null) return;
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = folder;
        EditorGUIUtility.PingObject(folder);
    }

    private void LoadOrCreateData()
    {
        data = AssetDatabase.LoadAssetAtPath<ProjectFolderShortcutsData>(assetPath);
        if (data == null)
            CreateDataAsset();
    }

    private void CreateDataAsset()
    {
        // Створюємо папку Assets/Editor якщо її немає
        if (!AssetDatabase.IsValidFolder("Assets/Editor"))
            AssetDatabase.CreateFolder("Assets", "Editor");

        data = ScriptableObject.CreateInstance<ProjectFolderShortcutsData>();
        AssetDatabase.CreateAsset(data, assetPath);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = data;
    }

    private void SaveData()
    {
        if (data == null) return;
        EditorUtility.SetDirty(data);
        AssetDatabase.SaveAssets();
    }
}
