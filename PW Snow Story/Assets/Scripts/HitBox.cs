using System.Collections.Generic;

using UnityEngine;

public class HitBox : MonoBehaviour
{
    public AudioClip[] fleshHitSounds;
    public AudioClip[] stoneHitSounds;
    AudioSource audioSource;
        
    public float damage = 25f;
    private HashSet<IDamageable> damagedTargets = new HashSet<IDamageable>();
    private Collider hitBoxCollider;
    public Character masterCharacter;
    public float pushForwardForce = 5f; // Сила відштовхування
    public float pushUpForce = 5f; // Сила відштовхування
    public float newFadingSpeed = 0.5f; // Затримка для відштовхування вгору
    
    public bool pushForward;
    public bool pushUp;
    public bool resetEnemyVelocity;    

    private void Awake()
    {
        hitBoxCollider = GetComponent<Collider>();
        audioSource = GetComponent<AudioSource>();
    }

    public void Activate()
    {
       damagedTargets.Clear(); // Очистити список перед початком нової атаки
       hitBoxCollider.enabled = true; // Увімкнути коллайдер
        
    }

    public void Deactivate()
    {
        hitBoxCollider.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        print("HitBox Trigger Entered: " + other.gameObject.name);
        IDamageable target = other.GetComponent<IDamageable>();
        if (target != null && !damagedTargets.Contains(target))
        {
            target.TakeDamage(damage);
            damagedTargets.Add(target);
            var targetMovement = other.GetComponent<MonsterMovement>();
            if (targetMovement != null)
            {
                if (pushForward) targetMovement.PushIntoDirection(masterCharacter.targetPointer.transform.forward * pushForwardForce, 0);
                if (pushUp)
                {
                    targetMovement.PushIntoDirection(Vector3.up * pushUpForce, 1f);
                  
                }
                if (resetEnemyVelocity) { targetMovement.PushIntoDirection(Vector3.zero * pushUpForce, 1f ); }
            }
            audioSource.PlayOneShot(fleshHitSounds[Random.Range(0, fleshHitSounds.Length)]);

        }
    }
}
