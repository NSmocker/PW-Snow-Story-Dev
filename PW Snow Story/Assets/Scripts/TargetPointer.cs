using System.Collections.Generic;
using UnityEngine;

public class TargetPointer : MonoBehaviour
{
    public float scanRadius = 10f; // Радіус сканування
    public List<GameObject> monsters = new List<GameObject>(); // Список знайдених монстрів
    public GameObject nearestTarget; // Найближчий монстр
    public GameObject target;
    public float distanceToTarget = 0f; // Відстань до цілі
    public float minDistanceToAttackLook=4f;
   
    private int frameCounter = 0; // Лічильник кадрів



   public void LookAtTarget()
    {
        if(target!=null)
        {
            
            
            transform.LookAt(target.transform.position);
            var myAngles = transform.eulerAngles;
            myAngles.x = 0;
            myAngles.z = 0;
            transform.eulerAngles = myAngles;
        }
    }

    public void MakeNearestToTarget()
    {   print("MakeNearestToTarget");
        if(nearestTarget!=null) target = nearestTarget;
        else
        {
            print("No target!");
            target = null;
        } 
    }
    public void ResetTarget()
    {
        target = null;
    }
    // Update is called once per frame
    void Update()
    {

        if(target != null)
        {
            distanceToTarget = Vector3.Distance(transform.position, target.transform.position);
        }
        else
        {
            distanceToTarget = -1f;
        }
        frameCounter++;
      

        // Сканувати кожен третій кадр
        if (frameCounter % 3 == 0)
        {
            ScanForMonsters();           
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
        nearestTarget = closestMonster;
    }

    // Для візуалізації радіусу сканування в редакторі Unity
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, scanRadius);
    }
}
