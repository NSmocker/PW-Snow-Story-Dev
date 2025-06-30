using UnityEditor;
using UnityEngine;

public class GrassPainterEditor : EditorWindow
{
    public GameObject prefabToSpawn;
    public float brushRadius = 1.0f;
    public float density = 4f; // Кількість травинок на одиницю площі
    public float scaleVariation = 0.1f;  // ±10%
    public float rotationYVariation = 0.5f; // ±50% (0.5 = 50%)

    private bool painting = false;

    [MenuItem("Tools/Grass Painter")]
    public static void ShowWindow()
    {
        GetWindow<GrassPainterEditor>("Grass Painter");
    }

    void OnGUI()
    {
        GUILayout.Label("Grass Painter Tool", EditorStyles.boldLabel);
        prefabToSpawn = (GameObject)EditorGUILayout.ObjectField("Prefab to Spawn", prefabToSpawn, typeof(GameObject), false);
        brushRadius = EditorGUILayout.FloatField("Brush Radius", brushRadius);
        density = EditorGUILayout.Slider("Density", density, 0.1f, 20f);
        scaleVariation = EditorGUILayout.Slider("Scale Variation ±%", scaleVariation, 0f, 0.5f);
        rotationYVariation = EditorGUILayout.Slider("Y Rotation Variation ±%", rotationYVariation, 0f, 1f);

        if (!painting)
        {
            if (GUILayout.Button("Start Painting"))
            {
                SceneView.duringSceneGui += OnSceneGUI;
                painting = true;
            }
        }
        else
        {
            if (GUILayout.Button("Stop Painting"))
            {
                SceneView.duringSceneGui -= OnSceneGUI;
                painting = false;
            }
        }
    }

    void OnSceneGUI(SceneView sceneView)
    {
        if (prefabToSpawn == null)
            return;

        Event e = Event.current;
        Ray ray = HandleUtility.GUIPointToWorldRay(e.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, 500f))
        {
            Handles.color = new Color(0f, 1f, 0f, 0.3f);
            Handles.DrawSolidDisc(hit.point, hit.normal, brushRadius);

            if (e.type == EventType.MouseDown && e.button == 0 && !e.alt)
            {
                if (e.shift)
                {
                    // Видалення префабів у зоні
                    DeleteGrassInArea(hit.point);
                }
                else
                {
                    SpawnGrassAtPoint(hit.point, hit.normal);
                }
                e.Use();
            }
        }

        SceneView.RepaintAll();
    }

    void SpawnGrassAtPoint(Vector3 center, Vector3 normal)
    {
        int count = Mathf.RoundToInt(density); // density = кількість об'єктів у радіусі brushRadius
        for (int i = 0; i < count; i++)
        {
            Vector2 randomCircle = Random.insideUnitCircle * brushRadius;
            Vector3 spawnPos = center + new Vector3(randomCircle.x, 2f, randomCircle.y); // трохи зверху, щоб спіймав рейкаст

            if (Physics.Raycast(spawnPos, Vector3.down, out RaycastHit hit, 10f))
            {
                GameObject newObj = (GameObject)PrefabUtility.InstantiatePrefab(prefabToSpawn);
                Undo.RegisterCreatedObjectUndo(newObj, "Paint Grass");
                newObj.transform.position = hit.point;

                // Рандомізація масштабу ±scaleVariation
                float scale = Random.Range(1f - scaleVariation, 1f + scaleVariation);
                newObj.transform.localScale = Vector3.one * scale;

                // Рандомізація повороту по Y
                float rotationOffset = Random.Range(-rotationYVariation, rotationYVariation) * 360f;
                newObj.transform.rotation = Quaternion.Euler(0f, rotationOffset, 0f);
            }
        }
    }

    void DeleteGrassInArea(Vector3 center)
    {
        // Знаходимо всі об'єкти з таким же префабом у радіусі brushRadius
        GameObject[] allObjects = GameObject.FindObjectsByType<GameObject>(FindObjectsSortMode.None);
        foreach (GameObject obj in allObjects)
        {
            if (PrefabUtility.GetCorrespondingObjectFromSource(obj) == prefabToSpawn)
            {
                float dist = Vector3.Distance(obj.transform.position, center);
                if (dist <= brushRadius)
                {
                    Undo.DestroyObjectImmediate(obj);
                }
            }
        }
    }

    void OnDisable()
    {
        SceneView.duringSceneGui -= OnSceneGUI;
        painting = false;
    }
}
