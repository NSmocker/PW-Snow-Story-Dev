using UnityEngine;

public class NPCAnimationsState : MonoBehaviour
{
    public Animator npcAnimator;
    
    [Header("Player Tag")]

    public string playerTag = "Player";
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag) )
        {
          npcAnimator.SetBool("PlayerNear", true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(playerTag) )
        {
            npcAnimator.SetBool("PlayerNear", false);
        }
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
