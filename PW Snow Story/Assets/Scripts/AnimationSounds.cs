using UnityEngine;

public class AnimationSounds : MonoBehaviour
{
   
    public AudioSource skillAudioSource;
    public AudioClip autoAttack;
    public AudioClip[] autoAttackClips;
    public AudioClip[] audioClips;

    public void PlaySound(string soundName)
    {   skillAudioSource.pitch=1.0f;
        foreach (AudioClip clip in audioClips)
        {
            if (clip.name == soundName)
            {
                skillAudioSource.PlayOneShot(clip);
                return;
            }
        }
        Debug.LogWarning("Sound not found: " + soundName);
    }
    public void PlaySwordSlash()
    {   

        skillAudioSource.PlayOneShot(autoAttackClips[Random.Range(0, autoAttackClips.Length)]);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       
    }

    // Update is called once per frame

}
