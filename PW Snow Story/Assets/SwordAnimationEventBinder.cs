using UnityEngine;
using UnityEngine.Events;

public class SwordAnimationEventBinder : MonoBehaviour
{
    public ParticleSystem particleSystem;

    public void OnAppearStart()
    {
        particleSystem.Play();
    }
    public void OnAppearEnd()
    {
        particleSystem.Stop();
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
