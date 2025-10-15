using UnityEditor;
using UnityEngine;

public class ApplyMaterialToTerrains : EditorWindow
{
    private Material terrainMaterial;

    [MenuItem("Tools/Terrain Material Applier")]
    public static void ShowWindow()
    {
        GetWindow<ApplyMaterialToTerrains>("Terrain Material Applier");
    }

    private void OnGUI()
    {
        GUILayout.Label("Apply Material to Selected Terrains", EditorStyles.boldLabel);
        GUILayout.Space(5);

        terrainMaterial = (Material)EditorGUILayout.ObjectField("Material", terrainMaterial, typeof(Material), false);

        GUILayout.Space(10);

        if (GUILayout.Button("Apply Material"))
        {
            ApplyMaterial();
        }
    }

    private void ApplyMaterial()
    {
        if (terrainMaterial == null)
        {
            EditorUtility.DisplayDialog("Error", "Please assign a material first.", "OK");
            return;
        }

        Object[] selectedObjects = Selection.objects;
        int count = 0;

        foreach (Object obj in selectedObjects)
        {
            if (obj is GameObject go)
            {
                Terrain terrain = go.GetComponent<Terrain>();
                if (terrain != null)
                {
                    terrain.materialTemplate = terrainMaterial;
                    EditorUtility.SetDirty(terrain);
                    count++;
                }
            }
        }

        if (count > 0)
        {
            Debug.Log($"✅ Applied material to {count} terrain(s).");
        }
        else
        {
            EditorUtility.DisplayDialog("No Terrains Found", "Please select at least one terrain in the scene.", "OK");
        }
    }
}
