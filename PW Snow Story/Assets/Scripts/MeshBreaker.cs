using System.Collections;
using UnityEngine;

public class MeshBreaker : MonoBehaviour
{
    public GameObject[] triAngles;
    public Material triangleMaterial;
    public float flyDuration = 2f;
    public float flyDistance = 2f;
    public float rotateSpeed = 180f;
    public AnimationCurve flyCurve = AnimationCurve.Linear(0, 0, 1, 1); // Додаємо криву швидкості
    MeshRenderer[] meshRenderers;
    SkinnedMeshRenderer[] skinnedMeshRenderers;
    void Start()
    {
        
        
    }
    void MakeDeathSound()
    {
        var animationSound = GetComponent<AnimationSounds>();
        animationSound?.PlaySound("SAO Death");
    }
    public void MakeDeathAnimation()
    {
        BreakMeshIntoTriangles();
        SetTrianglesMaterial(triangleMaterial);
        StartCoroutine(FlyTriangles());

    }
    public Color emissionTargetColor = Color.white;
    public float emissionFadeDuration = 1f;

    private Coroutine emissionCoroutine;

    public void FadeOriginalMeshesEmission()
    {
        if (emissionCoroutine != null)
            StopCoroutine(emissionCoroutine);
        emissionCoroutine = StartCoroutine(FadeEmissionCoroutineWithDisable());
    }

    public IEnumerator FadeEmissionCoroutineWithDisable()
    {
        // Збираємо всі MeshRenderer та SkinnedMeshRenderer
        meshRenderers = GetComponentsInChildren<MeshRenderer>();
        skinnedMeshRenderers = GetComponentsInChildren<SkinnedMeshRenderer>();

        float timer = 0f;
        var originalColors = new System.Collections.Generic.Dictionary<Material, Color>();

        // Додаємо матеріали з MeshRenderer
        foreach (var renderer in meshRenderers)
        {
            if (renderer == null) continue;
            foreach (var mat in renderer.materials)
            {
                if (mat.HasProperty("_EmissionColor") && !originalColors.ContainsKey(mat))
                {
                    originalColors[mat] = mat.GetColor("_EmissionColor");
                }
            }
        }
        // Додаємо матеріали зі SkinnedMeshRenderer
        foreach (var renderer in skinnedMeshRenderers)
        {
            if (renderer == null) continue;
            foreach (var mat in renderer.materials)
            {
                if (mat.HasProperty("_EmissionColor") && !originalColors.ContainsKey(mat))
                {
                    originalColors[mat] = mat.GetColor("_EmissionColor");
                }
            }
        }
        
        while (timer < emissionFadeDuration)
        {
            float t = timer / emissionFadeDuration;
            foreach (var kvp in originalColors)
            {
                if (kvp.Key != null)
                {
                    Color lerped = Color.Lerp(kvp.Value, emissionTargetColor, t);
                    kvp.Key.SetColor("_EmissionColor", lerped);
                    kvp.Key.EnableKeyword("_EMISSION");
                }
            }
            timer += Time.deltaTime;
            yield return null;
        }

        foreach (var kvp in originalColors)
        {
            if (kvp.Key != null)
            {
                kvp.Key.SetColor("_EmissionColor", emissionTargetColor);
                kvp.Key.EnableKeyword("_EMISSION");
            }
        }
        MakeDeathSound();
        // Вимикаємо всі рендерери після завершення емісії
        DisableOriginalMeshes();
    }

    private void DisableOriginalMeshes()
    {
        foreach (var renderer in meshRenderers)
        {
            if (renderer != null)
                renderer.enabled = false;
        }
        foreach (var renderer in skinnedMeshRenderers)
        {
            if (renderer != null)
                renderer.enabled = false;
        }
    }

    public void SetTrianglesMaterial(Material mat)
    {
        triangleMaterial = mat;
        if (triAngles == null || triAngles.Length == 0)
            return;

        foreach (GameObject triangle in triAngles)
        {
            if (triangle == null) continue;
            MeshRenderer mr = triangle.GetComponent<MeshRenderer>();
            if (mr != null)
            {
                mr.sharedMaterial = triangleMaterial;
            }
        }
    }

    void Update()
    {
        if( Input.GetKeyDown(KeyCode.Alpha1))
        {
            MakeDeathAnimation();
        }
    }

    [ContextMenu("Break Mesh Into Triangles")]
    public void BreakMeshIntoTriangles()
    {
        MeshFilter[] meshFilters = GetComponentsInChildren<MeshFilter>();
        SkinnedMeshRenderer[] skinnedMeshRenderers = GetComponentsInChildren<SkinnedMeshRenderer>();

        GameObject particlesParent = new GameObject("Particles");
        particlesParent.transform.SetParent(transform);
        particlesParent.transform.localPosition = Vector3.zero;
        particlesParent.transform.localRotation = Quaternion.identity;
        particlesParent.transform.localScale = Vector3.one;

        var trianglesList = new System.Collections.Generic.List<GameObject>();

        foreach (MeshFilter meshFilter in meshFilters)
        {
            if (meshFilter == null || meshFilter.sharedMesh == null)
                continue;

            CreateTriangles(meshFilter.sharedMesh, meshFilter.transform, meshFilter.GetComponent<MeshRenderer>(), particlesParent.transform, trianglesList);
        }

        foreach (SkinnedMeshRenderer smr in skinnedMeshRenderers)
        {
            if (smr == null || smr.sharedMesh == null)
                continue;

            Mesh bakedMesh = new Mesh();
            smr.BakeMesh(bakedMesh);

            CreateTriangles(bakedMesh, smr.transform, smr, particlesParent.transform, trianglesList);
        }

        triAngles = trianglesList.ToArray();
    }

    private void CreateTriangles(Mesh mesh, Transform meshTransform, Renderer originalRenderer, Transform parent, System.Collections.Generic.List<GameObject> trianglesList)
    {
        Vector3[] vertices = mesh.vertices;
        Vector3[] normals = mesh.normals;
        Vector2[] uvs = mesh.uv;
        int[] triangles = mesh.triangles;

        for (int i = 0; i < triangles.Length; i += 3)
        {
            Vector3[] worldVerts = new Vector3[3];
            for (int j = 0; j < 3; j++)
            {
                int idx = triangles[i + j];
                worldVerts[j] = meshTransform.TransformPoint(vertices[idx]);
            }

            Vector3 worldCenter = (worldVerts[0] + worldVerts[1] + worldVerts[2]) / 3f;

            GameObject triangleObj = new GameObject("Triangle_" + (i / 3));
            triangleObj.transform.SetParent(parent);
            triangleObj.transform.position = worldCenter;
            triangleObj.transform.rotation = Quaternion.identity;
            triangleObj.transform.localScale = Vector3.one;

            Vector3[] triVerts = new Vector3[3];
            Vector3[] triNormals = new Vector3[3];
            Vector2[] triUVs = new Vector2[3];
            int[] triIndices = { 0, 1, 2 };

            Vector3 avgNormal = Vector3.zero;

            for (int j = 0; j < 3; j++)
            {
                int idx = triangles[i + j];
                triVerts[j] = worldVerts[j] - worldCenter;

                Vector3 worldNormal = meshTransform.TransformDirection(normals.Length > 0 ? normals[idx] : Vector3.up);
                triNormals[j] = worldNormal.normalized;
                avgNormal += triNormals[j];

                triUVs[j] = uvs.Length > 0 ? uvs[idx] : Vector2.zero;
            }
            avgNormal.Normalize();

            Mesh triangleMesh = new Mesh();
            triangleMesh.vertices = triVerts;
            triangleMesh.normals = triNormals;
            triangleMesh.uv = triUVs;
            triangleMesh.triangles = triIndices;

            MeshFilter mf = triangleObj.AddComponent<MeshFilter>();
            mf.mesh = triangleMesh;

            MeshRenderer mr = triangleObj.AddComponent<MeshRenderer>();
            if (originalRenderer != null)
            {
                mr.sharedMaterial = originalRenderer.sharedMaterial;
            }

            // Додаємо компонент для анімації
            var anim = triangleObj.AddComponent<TriangleFly>();
            anim.normal = avgNormal;
            anim.duration = flyDuration;
            anim.distance = flyDistance;
            anim.rotateSpeed = rotateSpeed;
            anim.flyCurve = flyCurve; // Передаємо криву

            trianglesList.Add(triangleObj);
        }
    }

    // Корутина для запуску анімації всіх трикутників
    private IEnumerator FlyTriangles()
    {
        if (triAngles == null) yield break;
        foreach (var t in triAngles)
        {
            var anim = t.GetComponent<TriangleFly>();
            if (anim != null)
                anim.StartFly();
        }
        yield return null;
    }
}

// Додаємо допоміжний компонент для анімації трикутника
public class TriangleFly : MonoBehaviour
{
    public Vector3 normal;
    public float duration = 2f;
    public float distance = 2f;
    public float rotateSpeed = 180f;
    public AnimationCurve flyCurve = AnimationCurve.Linear(0, 0, 1, 1);

    public float positionNoiseStrength = 0.2f;
    public float rotationNoiseStrength = 30f;
    public float upwardStrength = 1.0f;

    private Vector3 startPos;
    private Quaternion startRot;
    private float timer = 0f;
    private bool flying = false;

    private Vector3 noiseOffset;
    private Vector3 randomEulerAxis;

    public void StartFly()
    {
        startPos = transform.position;
        startRot = transform.rotation;
        timer = 0f;
        flying = true;

        noiseOffset = Random.onUnitSphere;
        randomEulerAxis = Random.onUnitSphere;
    }

    void Update()
    {
        if (!flying) return;

        timer += Time.deltaTime;
        float t = Mathf.Clamp01(timer / duration);

        float curveT = flyCurve != null ? flyCurve.Evaluate(t) : t;

        Vector3 basePos = startPos
            + normal * distance * curveT
            + Vector3.up * upwardStrength * distance * curveT;

        float noiseT = Mathf.PerlinNoise(timer, noiseOffset.x) - 0.5f;
        Vector3 noise = noiseOffset * positionNoiseStrength * noiseT * (1f - t);

        transform.position = basePos + noise;

        float rotAmount = rotateSpeed * Time.deltaTime;
        transform.Rotate(normal, rotAmount, Space.World);
        transform.Rotate(randomEulerAxis, rotationNoiseStrength * Mathf.Sin(timer * 2f) * Time.deltaTime, Space.World);

        // Додаємо зменшення масштабу до нуля під час польоту
        float scale = Mathf.Lerp(1f, 0f, t);
        transform.localScale = Vector3.one * scale;

        if (t >= 1f)
        {
            flying = false;
            Destroy(gameObject);
        }
    }
}
