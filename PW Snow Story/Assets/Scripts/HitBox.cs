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
            audioSource.PlayOneShot(fleshHitSounds[Random.Range(0, fleshHitSounds.Length)]);

        }
    }
}
