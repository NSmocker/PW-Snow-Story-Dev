using UnityEngine;

public class ClosestEnemyPointer : MonoBehaviour
{
    [SerializeField] private float checkRadius = 5f;
    [SerializeField] private LayerMask enemyLayer;
    public Collider[] enemies;
    public GameObject closestEnemy;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
         enemies = Physics.OverlapSphere(transform.position, checkRadius, enemyLayer);

        if (enemies.Length > 0)
        {
            float minDist = Mathf.Infinity;
            closestEnemy = null;
            foreach (Collider enemy in enemies)
            {
                float dist = Vector3.Distance(transform.position, enemy.transform.position);
                if (dist < minDist)
                {
                    minDist = dist;
                    closestEnemy = enemy.gameObject;
                }
            }

            if (closestEnemy != null)
            {
                transform.LookAt(closestEnemy.transform);
                transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
            }
        }
        else
        {
            closestEnemy = null;
        }

        // Промальовка променя до найближчого ворога
        if (closestEnemy != null)
        {
            Debug.DrawLine(transform.position, closestEnemy.transform.position, Color.red);
        }
    }
}
