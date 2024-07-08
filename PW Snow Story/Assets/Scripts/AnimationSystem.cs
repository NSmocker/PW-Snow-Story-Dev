using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationSystem : MonoBehaviour
{
	public CharacterMovement movement_system;
	public Animator anim_link;
	public float movement_magnitude;
	public bool sprint;
 
 
	public void AnimateByInput(Vector2 move_info)
	{
		movement_magnitude = move_info.magnitude;
		sprint = Input.GetButton("Sprint");
	}
    // Start is called before the first frame update
    void Start()
    {
		movement_system.on_jump.AddListener(AnimateJump);
        
    }
	void AnimateJump()
	{
		anim_link.SetTrigger("jump");
	}

    // Update is called once per frame
    void Update()
    {
	    anim_link.SetFloat("movement_magnitude",movement_magnitude);
	    anim_link.SetBool("sprint",sprint);
		anim_link.SetBool("grounded",movement_system.groundChecker.isGrounded);
	    
    }
}
