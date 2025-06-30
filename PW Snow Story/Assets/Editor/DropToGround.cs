using UnityEngine;
using UnityEditor;

public class DropToGround : MonoBehaviour
{
    [MenuItem("Tools/Притягнути до землі з варіацією %g")] // Ctrl + G (Cmd + G на Mac)
    static void DropSelectedObjectsToGround()
    {
        foreach (GameObject obj in Selection.gameObjects)
        {
            DropAndRandomize(obj);
        }
    }

    static void DropAndRandomize(GameObject obj)
    {
        // Центр обʼєкта (вгорі)
        Vector3 origin = obj.transform.position + Vector3.up * 10f;

        // Робимо рейкаст вниз
        if (Physics.Raycast(origin, Vector3.down, out RaycastHit hit, 100f))
        {
            Undo.RecordObject(obj.transform, "Drop and Randomize");

            // Притискаємо до землі
            Vector3 pos = obj.transform.position;
            pos.y = hit.point.y;
            obj.transform.position = pos;

            // Рандомізація масштабу ±10%
            Vector3 originalScale = obj.transform.localScale;
            float scaleFactor = Random.Range(0.9f, 1.1f);
            obj.transform.localScale = originalScale * scaleFactor;

            // Рандомізація обертання по осі Y ±50% від початкового
            float originalYRotation = obj.transform.eulerAngles.y;
            float yOffset = Random.Range(-0.5f, 0.5f) * originalYRotation;
            Vector3 euler = obj.transform.eulerAngles;
            euler.y = originalYRotation + yOffset;
            obj.transform.eulerAngles = euler;
        }
        else
        {
            Debug.LogWarning($"Обʼєкт '{obj.name}' не знайшов землю під собою.");
        }
    }
}
