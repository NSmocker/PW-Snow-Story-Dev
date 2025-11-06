using UnityEngine;
using UnityEditor;
using System.IO;

public class TerrainLayerSaver : EditorWindow
{
    private GameObject rootObject;

    [MenuItem("Tools/Terrain/Зберегти всі шари террейнів у вибраному об’єкті")]
    public static void ShowWindow()
    {
        GetWindow<TerrainLayerSaver>("Збереження Terrain-ів");
    }

    private void OnGUI()
    {
        GUILayout.Label("Зберегти всі шари Terrain у дочірніх об’єктах", EditorStyles.boldLabel);
        rootObject = (GameObject)EditorGUILayout.ObjectField("Головний GameObject:", rootObject, typeof(GameObject), true);

        if (rootObject == null)
        {
            EditorGUILayout.HelpBox("Вибери GameObject, у якого є дочірні Terrain-и.", MessageType.Info);
            return;
        }

        if (GUILayout.Button("💾 Зберегти всі шари террейнів у дочірніх об’єктах"))
        {
            SaveTerrainsInHierarchy();
        }
    }

    private void SaveTerrainsInHierarchy()
    {
        Terrain[] terrains = rootObject.GetComponentsInChildren<Terrain>(true);

        if (terrains.Length == 0)
        {
            Debug.LogWarning($"⚠️ У '{rootObject.name}' не знайдено жодного Terrain!");
            return;
        }

        string rootDir = "Assets/SavedTerrainLayers";
        if (!Directory.Exists(rootDir))
            Directory.CreateDirectory(rootDir);

        int counter = 0;

        foreach (Terrain terrain in terrains)
        {
            if (terrain == null || terrain.terrainData == null)
            {
                Debug.LogWarning("⚠️ Пропущено пустий Terrain або без даних.");
                continue;
            }

            TerrainLayer[] layers = terrain.terrainData.terrainLayers;
            if (layers == null || layers.Length == 0)
            {
                Debug.LogWarning($"⚠️ Terrain '{terrain.name}' не має шарів для збереження.");
                continue;
            }

            // --- Папка для кожного Terrain ---
            string terrainFolderName = terrain.gameObject.name.Replace(" ", "_");
            string saveDir = Path.Combine(rootDir, terrainFolderName);
            if (!Directory.Exists(saveDir))
                Directory.CreateDirectory(saveDir);

            Debug.Log($"📁 Обробка Terrain: {terrain.name} → {saveDir}");

            TerrainLayer[] newLayers = new TerrainLayer[layers.Length];

            for (int i = 0; i < layers.Length; i++)
            {
                TerrainLayer oldLayer = layers[i];
                if (oldLayer == null)
                    continue;

                // --- Зберігаємо текстури ---
                Texture2D diffuse = SaveTexture(oldLayer.diffuseTexture, saveDir, $"_diffuse_{i}");
                Texture2D normal = SaveTexture(oldLayer.normalMapTexture, saveDir, $"_normal_{i}");
                Texture2D mask = SaveTexture(oldLayer.maskMapTexture, saveDir, $"_mask_{i}");

                // --- Створюємо новий TerrainLayer ---
                TerrainLayer newLayer = new TerrainLayer();
                newLayer.diffuseTexture = diffuse;
                newLayer.normalMapTexture = normal;
                newLayer.maskMapTexture = mask;
                newLayer.tileSize = oldLayer.tileSize;
                newLayer.tileOffset = oldLayer.tileOffset;
                newLayer.metallic = oldLayer.metallic;
                newLayer.smoothness = oldLayer.smoothness;
                newLayer.diffuseRemapMin = oldLayer.diffuseRemapMin;
                newLayer.diffuseRemapMax = oldLayer.diffuseRemapMax;
                newLayer.maskMapRemapMin = oldLayer.maskMapRemapMin;
                newLayer.maskMapRemapMax = oldLayer.maskMapRemapMax;

                // --- Зберігаємо слой у проєкт ---
                string layerPath = Path.Combine(saveDir, $"Saved_Layer_{i}.asset");
                AssetDatabase.CreateAsset(newLayer, layerPath);

                newLayers[i] = newLayer;
                Debug.Log($"✅ Збережено шар #{i} для Terrain '{terrain.name}' → {layerPath}");
            }

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            // --- Замінюємо шари у Terrain ---
            terrain.terrainData.terrainLayers = newLayers;

            // --- Додаємо суфікс до імені ---
            if (!terrain.name.EndsWith("_saved"))
            {
                terrain.name += "_saved";
                EditorUtility.SetDirty(terrain.gameObject);
            }

            counter++;
            Debug.Log($"🎉 Terrain '{terrain.name}' оброблено ({newLayers.Length} шарів).");
        }

        EditorUtility.SetDirty(rootObject);
        AssetDatabase.SaveAssets();
        SceneView.RepaintAll();

        Debug.Log($"🏁 Успішно оброблено {counter}/{terrains.Length} Terrain-ів у '{rootObject.name}'.");
    }

    // --- Збереження текстур (у тому числі “нечитабельних”) ---
    private Texture2D SaveTexture(Texture2D source, string saveDir, string suffix)
    {
        if (source == null)
            return null;

        string assetPath = AssetDatabase.GetAssetPath(source);
        if (!string.IsNullOrEmpty(assetPath))
        {
            // Якщо текстура вже є активом — не створюємо копію
            return source;
        }

        RenderTexture rt = RenderTexture.GetTemporary(source.width, source.height, 0, RenderTextureFormat.ARGB32);
        Graphics.Blit(source, rt);

        RenderTexture previous = RenderTexture.active;
        RenderTexture.active = rt;

        Texture2D readableTex = new Texture2D(source.width, source.height, TextureFormat.RGBA32, false);
        readableTex.ReadPixels(new Rect(0, 0, source.width, source.height), 0, 0);
        readableTex.Apply();

        RenderTexture.active = previous;
        RenderTexture.ReleaseTemporary(rt);

        byte[] pngData = readableTex.EncodeToPNG();
        string fileName = Path.Combine(saveDir, source.name + suffix + ".png");
        File.WriteAllBytes(fileName, pngData);

        AssetDatabase.ImportAsset(fileName);
        Texture2D saved = AssetDatabase.LoadAssetAtPath<Texture2D>(fileName);

        Debug.Log($"🖼️ Збережено текстуру: {fileName}");
        return saved;
    }
}
