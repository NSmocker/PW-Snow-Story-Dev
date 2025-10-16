using Unity.AppUI.UI;
using UnityEngine;

public class RotateToNearestMonster : MonoBehaviour
{
     [SerializeField] private float attackRadius = 5f;
    [SerializeField] private LayerMask enemyLayer;
    public Collider[] enemies;
    public Animator animator;

    public CharacterController characterController;
    float rotationTimer = 2f;
   
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      
    }
    

    public void FindAndLookAtNearestEnemy()
    {
        enemies = Physics.OverlapSphere(transform.position, attackRadius, enemyLayer);

        if (enemies.Length > 0)
        {
            Transform closestEnemy = null;
            float minDist = Mathf.Infinity;

            foreach (Collider enemy in enemies)
            {
                float dist = Vector3.Distance(transform.position, enemy.transform.position);
                if (dist < minDist)
                {
                    minDist = dist;
                    closestEnemy = enemy.transform;
                }
            }

            if (closestEnemy != null)
            {
                print("Looking at " + closestEnemy.name);
                animator.applyRootMotion = false;
                transform.LookAt(closestEnemy);
                transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
                animator.applyRootMotion = true;
            }
        }
    }
    
   
}
