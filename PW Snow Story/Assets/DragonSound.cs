using UnityEngine;

public class DragonSound : MonoBehaviour
{
    public AudioClip dragon_sound;
    public AudioSource dragon_audio_source;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    public void PlayDragonSound()
    {
        dragon_audio_source.PlayOneShot(dragon_sound);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
