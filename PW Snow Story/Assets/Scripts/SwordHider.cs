using UnityEngine;

public class SwordHider : MonoBehaviour
{
    public Animator animator;
    public GamePlayModeManager gamePlayModeManager;

    public GameObject particleEmitter;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void ActivateParticles()
    {
        particleEmitter.SetActive(true);
    }
    public void DeactivateParticles()
    {
        particleEmitter.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
        animator.SetBool("Hidden", gamePlayModeManager.currentGamePlayMode == GamePlayModes.City);
        
        
    }
}
