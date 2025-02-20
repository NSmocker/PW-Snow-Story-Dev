using UnityEngine;

public class AttackSystem : MonoBehaviour
{
    public Animator animator;

    public KeyCode defaultAttackKey = KeyCode.Mouse0;
    public KeyCode juggleryAttackKey = KeyCode.F;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(defaultAttackKey)){
           
            animator.SetTrigger("makeAttack");
            }    
    }
}
