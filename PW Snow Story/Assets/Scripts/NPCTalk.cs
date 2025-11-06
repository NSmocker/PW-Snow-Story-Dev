using UnityEngine;

public class NPCTalk : MonoBehaviour
{
    public AudioSource mouth;
    [Header("NPC Voice Clips")]
    public AudioClip[] greetingClips;
    public AudioClip[] farewellClip;
    
    [Header("Player Tag")]

    public string playerTag = "Player";

    private int greetingIndex = 0;
    private int farewellIndex = 0;

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
        if (other.CompareTag(playerTag) && greetingClips != null && greetingClips.Length > 0)
        {
            mouth.clip = greetingClips[greetingIndex];
            mouth.Stop();
            mouth.Play();
            greetingIndex = (greetingIndex + 1) % greetingClips.Length;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(playerTag) && farewellClip != null && farewellClip.Length > 0)
        {
            mouth.clip = farewellClip[farewellIndex];
            mouth.Stop();
            mouth.Play();
            farewellIndex = (farewellIndex + 1) % farewellClip.Length;
        }
    }

}
