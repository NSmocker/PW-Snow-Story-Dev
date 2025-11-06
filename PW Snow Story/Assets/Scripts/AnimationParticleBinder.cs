using UnityEngine;

public class AnimationParticleBinder : MonoBehaviour
{
    public ParticleSystem _particleSystem;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void MakeBurst(int particleCount)
    {
        _particleSystem.Emit(particleCount);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
