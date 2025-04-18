using System.Collections.Generic;
using UnityEngine;

public class TargetLocker : MonoBehaviour
{
    public float scanRadius = 10f; // Радіус сканування
    public List<GameObject> monsters = new List<GameObject>(); // Список знайдених монстрів
    public GameObject nearestMonster; // Найближчий монстр
    public DirectionPointer directionPointer; // Посилання на DirectionPointer
    private int frameCounter = 0; // Лічильник кадрів

    // Update is called once per frame
    void Update()
    {
        frameCounter++;

        // Сканувати кожен третій кадр
        if (frameCounter % 3 == 0)
        {
            ScanForMonsters();

            // Якщо DirectionPointer блокує ціль, встановити найближчого монстра як target
            if (directionPointer != null && directionPointer.isTargetLocked)
            {
                directionPointer.target = nearestMonster;
                print("+++");
            }
        }
    }

    void ScanForMonsters()
    {
        // Отримати всі об'єкти в радіусі
        Collider[] hits = Physics.OverlapSphere(transform.position, scanRadius);

        float nearestDistance = float.MaxValue;
        GameObject closestMonster = null;

        foreach (var hit in hits)
        {
            if (hit.CompareTag("Monster"))
            {
                // Додати монстра в список, якщо його ще немає
                if (!monsters.Contains(hit.gameObject))
                {
                    monsters.Add(hit.gameObject);
                }

                // Обчислити дистанцію до монстра
                float distance = Vector3.Distance(transform.position, hit.transform.position);

                // Знайти найближчого монстра
                if (distance < nearestDistance)
                {
                    nearestDistance = distance;
                    closestMonster = hit.gameObject;
                }
            }
        }

        // Оновити змінну з найближчим монстром
        nearestMonster = closestMonster;
    }

    // Для візуалізації радіусу сканування в редакторі Unity
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, scanRadius);
    }
}
