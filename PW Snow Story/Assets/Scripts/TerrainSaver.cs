using System;
using System.IO;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TerrainSaver : EditorWindow
{
    private string SaveFolder = "SavedTerrains";

    [MenuItem("Tools/TerrainSaver")]
    public static void ShowWindow()
    {
        GetWindow<TerrainSaver>("TerrainSaver");
    }

    Terrain[] Terrains;
    private void OnGUI()
    {
        GetTerrains();

        if (Terrains.Length > 0)
        {
            GUILayout.Label($"Выбрано террейнов: {Terrains.Length}");
        }
        else GUILayout.Label("Террейны не выбраны");


        SaveFolder = EditorGUILayout.TextField("Папка для сохранения", SaveFolder);

        if (GUILayout.Button("Сохранить"))
        {
            SaveTerrains();
        }
    }

    void GetTerrains()
    {
        Terrains = Selection.GetFiltered<Terrain>(SelectionMode.Editable | SelectionMode.ExcludePrefab);
        if (Terrains.Length == 0) Terrains = GameObject.FindObjectsByType<Terrain>(FindObjectsSortMode.None);
    }

    private void SaveTerrains()
    {
        GetTerrains();
        if (Terrains.Length == 0)
        {
            Debug.LogError("Не найдено террейнов");
            return;
        }

        var CurrentScene = SceneManager.GetActiveScene();
        string SceneName = CurrentScene.name;
        if (!SaveFolder.ToLower().Contains("assets")) SaveFolder = "Assets/" + SaveFolder;
        if (!SaveFolder.Contains(SceneName)) SaveFolder = Path.Combine(SaveFolder, SceneName);

        if (!Directory.Exists(SaveFolder))
        {
            Directory.CreateDirectory(SaveFolder); AssetDatabase.Refresh();
        }

        foreach (var sTerrain in Terrains)
        {
            SaveTerrain(sTerrain);
        }
        AssetDatabase.Refresh();
        EditorSceneManager.MarkSceneDirty(CurrentScene);
    }

    void SaveTerrain(Terrain sTerrain)
    {
        TerrainData sData = sTerrain.terrainData;

        if (sData.name.Length > 0) return;

        string TerrName = sTerrain.name.Replace(" ", "_");
        string TerrFolder = Path.Combine(SaveFolder, TerrName);
        string TexFolder = Path.Combine(TerrFolder, "Textures");
        string LayersFolder = Path.Combine(TerrFolder, "Layers");

        Directory.CreateDirectory(TerrFolder);
        Directory.CreateDirectory(TexFolder);
        Directory.CreateDirectory(LayersFolder);
        AssetDatabase.Refresh();


        TerrainData nData = Instantiate(sData);
        /*
        nData.heightmapResolution = sData.heightmapResolution;
        nData.baseMapResolution = sData.baseMapResolution;
        nData.alphamapResolution = sData.alphamapResolution;
        nData.size = sData.size;
        nData.SetHeights(0, 0, sData.GetHeights(0, 0, sData.heightmapResolution, sData.heightmapResolution));
        nData.detailPrototypes = sData.detailPrototypes;
        nData.treePrototypes = sData.treePrototypes;
        */

        var sLayers = sData.terrainLayers;
        TerrainLayer[] nLayers = new TerrainLayer[sLayers.Length];
        for (int i = 0; i < sLayers.Length; i++)
        {
            TerrainLayer sLayer = sLayers[i];
            //if (sLayer == null) continue;
            TerrainLayer nLayer = GameObject.Instantiate(sLayer);
            if (sLayer.diffuseTexture != null)
            {
                string nDiffPath = Path.Combine(TexFolder, $"TDiffuse_{i}.png").Replace("\\", "/");
                SaveTextureAsPNG(sLayer.diffuseTexture, nDiffPath);
                nLayer.diffuseTexture = AssetDatabase.LoadAssetAtPath<Texture2D>(nDiffPath);
            }

            if (sLayer.normalMapTexture != null)
            {
                string nNormalPath = Path.Combine(TexFolder, $"TNormal_{i}.png").Replace("\\", "/");
                SaveTextureAsPNG(sLayer.normalMapTexture, nNormalPath);
                nLayer.normalMapTexture = AssetDatabase.LoadAssetAtPath<Texture2D>(nNormalPath);
            }

            if (sLayer.maskMapTexture != null)
            {
                string nMaskPath = Path.Combine(TexFolder, $"TMask_{i}.png").Replace("\\", "/");
                SaveTextureAsPNG(sLayer.maskMapTexture, nMaskPath);
                nLayer.maskMapTexture = AssetDatabase.LoadAssetAtPath<Texture2D>(nMaskPath);
            }

            string nLayerPath = Path.Combine(LayersFolder, $"TLayer_{i}.asset").Replace("\\", "/");
            AssetDatabase.CreateAsset(nLayer, nLayerPath);
            nLayers[i] = AssetDatabase.LoadAssetAtPath<TerrainLayer>(nLayerPath);
        }
        nData.terrainLayers = nLayers;

        float[,,] sAlphamaps = sData.GetAlphamaps(0, 0, sData.alphamapWidth, sData.alphamapHeight);
        nData.SetAlphamaps(0, 0, sAlphamaps);

        if(nLayers.Length != nData.alphamapLayers)
        Debug.LogError("Layers: " + nLayers.Length + " AlphaMaps: " + nData.alphamapLayers);

        string TDataPath = Path.Combine(TerrFolder, $"TData_{TerrName}.asset").Replace("\\", "/");
        AssetDatabase.CreateAsset(nData, TDataPath);
        AssetDatabase.SaveAssets();

        sTerrain.terrainData = nData;

        TerrainCollider TC = sTerrain.GetComponent<TerrainCollider>();
        TC.terrainData = nData;

        Debug.Log($"TerrainData сохранён: {TDataPath}");
    }


    private void SaveTextureAsPNG(Texture2D source, string path)
    {
        if (source == null) return;

        path = path.Replace("\\", "/");

        if (source.isReadable)// source = GetReadableCopy(source);
        {
            byte[] pngData = source.EncodeToPNG();
            File.WriteAllBytes(path, pngData);
        }

        AssetDatabase.ImportAsset(path);
    }

    private Texture2D GetReadableCopy(Texture2D src)
    {
        RenderTexture rt = RenderTexture.GetTemporary(src.width, src.height, 0, RenderTextureFormat.Default, RenderTextureReadWrite.Linear);
        Graphics.Blit(src, rt);

        RenderTexture prev = RenderTexture.active;
        RenderTexture.active = rt;

        Texture2D tex = new Texture2D(src.width, src.height, TextureFormat.RGBA32, false);
        tex.ReadPixels(new Rect(0, 0, src.width, src.height), 0, 0);
        tex.Apply();

        RenderTexture.active = prev;
        RenderTexture.ReleaseTemporary(rt);

        return tex;
    }
}
