using UnityEngine;
using UnityEngine.Events;
public interface IDamageable
{
    void TakeDamage(float amount);
}

public class Health : MonoBehaviour, IDamageable
{
    public UnityEvent OnDeath;
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
    [ContextMenu("Die")]
   public void Die()
    {   
        animator.Play("Death");
        OnDeath.Invoke();
       
       
        
    }
}
