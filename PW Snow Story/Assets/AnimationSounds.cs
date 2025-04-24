using UnityEngine;

public class AnimationSounds : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip swordSlash;

    public void PlaySwordSlash()
    {   audioSource.pitch=1.0f;
        audioSource.PlayOneShot(swordSlash);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame

}
