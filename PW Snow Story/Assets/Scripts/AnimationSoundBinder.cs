using System.Collections.Generic;
using UnityEngine;


public class AnimationSoundBinder : MonoBehaviour
{


    public List<AudioClip> clips = new List<AudioClip>();




    [Header("Auto Init Settings")]
    public AudioSource audioSource;


    public List<AudioClip> footsteps = new List<AudioClip>();
    public List<AudioClip> sword = new List<AudioClip>();
    public AudioClip jumpSound;
    public AudioClip secondJumpSound;

    public void PlayJumpSound()
    {
        audioSource.PlayOneShot(jumpSound);
    }

    public void PlaySecondJumpSound()
    {
        audioSource.PlayOneShot(secondJumpSound);
    }
    public void PlayFootstepSound()
    {
        if (footsteps.Count > 0 && audioSource != null)
        {
            AudioClip clip = footsteps[Random.Range(0, footsteps.Count)];
            audioSource.PlayOneShot(clip);
        }
    }
    public void PlaySwordSound()
    {
        if (sword.Count > 0 && audioSource != null)
        {
            AudioClip clip = sword[Random.Range(0, sword.Count)];
            audioSource.PlayOneShot(clip);
        }
    }
    
    void InitFootsteps()
    {
        footsteps.Clear();
        foreach (var clip in clips)
        {
            if (clip != null && clip.name.ToLower().Contains("footstep"))
            {
            footsteps.Add(clip);
            }
        }    
    }
    void InitSword()
    {
        sword.Clear();
        foreach (var clip in clips)
        {
            if (clip != null && clip.name.ToLower().Contains("sword"))
            {
                sword.Add(clip);
            }
        }
    }

    public void PlayEffectByName(string clipName)
    {
        AudioClip clip = clips.Find(c => c != null && c.name == clipName);
        if (clip != null && audioSource != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }
    public void PlayEffectReversedByName(string clipName)
    {
        AudioClip clip = clips.Find(c => c != null && c.name == clipName);
        if (clip != null && audioSource != null)
        {
            audioSource.pitch = -1f;
            audioSource.PlayOneShot(clip);
            audioSource.pitch = 1f;
        }
    }    
    void Start()
    {
        InitFootsteps();
        InitSword();
        jumpSound = clips.Find(clip => clip != null && clip.name.Contains("First Jump"));
        secondJumpSound = clips.Find(clip => clip != null && clip.name.Contains("Second Jump"));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
