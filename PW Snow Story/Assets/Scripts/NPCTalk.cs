using UnityEngine;

public class NPCTalk : MonoBehaviour
{
    public AudioSource mouth;
    [Header("NPC Voice Clips")]
    public AudioClip[] voiceClips;
    [Header("Player Tag")]
    public string playerTag = "Player";

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag) && voiceClips != null && voiceClips.Length > 0)
        {
            int randomIndex = Random.Range(0, voiceClips.Length);
            mouth.clip = voiceClips[randomIndex];
            mouth.Play();
        }
    }
}
