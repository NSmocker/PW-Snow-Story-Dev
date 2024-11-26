using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationSystem : MonoBehaviour
{
	public CharacterMovement movementSystem;

	public SwordSystem swordSystem;
	public Animator animLink;
	public float movementMagnitude;
	public bool sprint;
 
    public KeyCode attackKey = KeyCode.Mouse0;

	public void AnimateByInput(Vector2 moveInfo)
	{
		movementMagnitude = moveInfo.magnitude;
		

	}
    // Start is called before the first frame update
    void Start()
    {
		movementSystem.OnJump.AddListener(AnimateJump);
    }
	void AnimateJump() 
	{
		animLink.SetTrigger("jump");
	}

    // Update is called once per frame
    void Update()
    {

		if(Time.timeScale == 0) return;
		sprint = Input.GetButton("Sprint");
		animLink.applyRootMotion = movementSystem.groundChecker.isGrounded;
	    animLink.SetFloat("movement_magnitude",movementMagnitude);
	    animLink.SetBool("sprint",sprint);
		animLink.SetBool("grounded",movementSystem.groundChecker.isGrounded);
		animLink.SetBool("swordIsOn",swordSystem.isOn);
		
		
		animLink.SetBool("makeAttack",Input.GetKey(attackKey));
		 
	    
    }
}
