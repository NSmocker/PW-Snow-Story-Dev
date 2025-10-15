using UnityEngine;
using UnityEditor;
using System.IO;

public class MaterialFromTextureWindow : EditorWindow
{
    private Texture2D selectedTexture;

    [MenuItem("Tools/Create URP Lit Material From Selected Texture")]
    public static void ShowWindow()
    {
        GetWindow<MaterialFromTextureWindow>("Material Creator");
    }

    private void OnGUI()
    {
        GUILayout.Label("Створити URP/Lit матеріал із вибраної текстури", EditorStyles.boldLabel);

        // Автоматично отримуємо вибраний у Project файл
        selectedTexture = Selection.activeObject as Texture2D;

        if (selectedTexture != null)
        {
            EditorGUILayout.ObjectField("Вибрана текстура", selectedTexture, typeof(Texture2D), false);

            if (GUILayout.Button("Створити матеріал"))
            {
                CreateMaterialFromTexture(selectedTexture);
            }
        }
        else
        {
            EditorGUILayout.HelpBox("Виберіть текстуру у Project вікні.", MessageType.Info);
            GUI.enabled = false;
            GUILayout.Button("Створити матеріал");
            GUI.enabled = true;
        }
    }

    private void CreateMaterialFromTexture(Texture2D texture)
    {
        string texturePath = AssetDatabase.GetAssetPath(texture);

        if (string.IsNullOrEmpty(texturePath))
        {
            Debug.LogError("Не вдалося знайти шлях до текстури.");
            return;
        }

        string directory = Path.GetDirectoryName(texturePath);
        string materialName = texture.name + "_Material";
        string materialPath = Path.Combine(directory, materialName + ".mat");

        // Отримуємо шейдер URP/Lit
        Shader shader = Shader.Find("Universal Render Pipeline/Lit");
        if (shader == null)
        {
            Debug.LogError("Не знайдено шейдер: Universal Render Pipeline/Lit");
            return;
        }

        // Створюємо матеріал і встановлюємо текстуру
        Material material = new Material(shader);
        material.SetTexture("_BaseMap", texture);

        // Зберігаємо матеріал
        AssetDatabase.CreateAsset(material, materialPath);
        AssetDatabase.SaveAssets();

        // Виділяємо щойно створений матеріал
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = material;

        Debug.Log($"Матеріал створено: {materialPath}");
    }
}
