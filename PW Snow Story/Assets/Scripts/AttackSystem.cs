using UnityEngine;

public class AttackSystem : MonoBehaviour
{
    public Animator animator;

    public KeyCode attackKey = KeyCode.Mouse0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(attackKey)){
           
            animator.SetTrigger("makeAttack");
            }    
    }
}
