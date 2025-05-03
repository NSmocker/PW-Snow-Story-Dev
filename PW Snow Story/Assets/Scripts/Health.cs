using UnityEngine;
public interface IDamageable
{
    void TakeDamage(float amount);
}

public class Health : MonoBehaviour, IDamageable
{
    public float maxHealth = 100f;
    public float currentHealth;
    public Animator animator;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        animator.SetTrigger("takeDamage");
        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            
        }
    }

    void Die()
    {
        Debug.Log($"{gameObject.name} загинув.");
        // Анімація смерті, видалення об'єкта, тощо
        Destroy(gameObject);
    }
}
