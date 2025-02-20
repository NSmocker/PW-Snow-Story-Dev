using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationSystem : MonoBehaviour
{
	public CharacterMovement movementSystem;

	//public SwordSystem swordSystem;
	public Animator animLink;
	public float movementMagnitude;
	public bool sprint;
	public bool weaponIsOn; 
    public KeyCode defaultAttackKey = KeyCode.Mouse0;
	 public KeyCode juggleryAttackKey = KeyCode.Mouse0;
	public KeyCode getSwordKey = KeyCode.V;
	
	[Header("Attacks")]
	public float attackKeyStickTime ;
	public float attackKeyStickTimeDelay = 0.1f;
	public float juggleryAttackKeyAttackKeyStickTime ;
	public float juggleryAttackKeyStickTimeDelay = 0.1f;
	

	

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
		
		if(attackKeyStickTime>0)attackKeyStickTime-=Time.deltaTime;
		if(juggleryAttackKeyAttackKeyStickTime>0)juggleryAttackKeyAttackKeyStickTime-=Time.deltaTime;


		sprint = Input.GetButton("Sprint");
		if(movementMagnitude<0.1f && movementSystem.groundChecker.isGrounded)
		{
			if(Input.GetKeyDown(getSwordKey))weaponIsOn = !weaponIsOn;
		}
		animLink.SetBool("wasComboInFloat",movementSystem.wasComboInFloat);
		animLink.applyRootMotion = movementSystem.groundChecker.isGrounded;
	    animLink.SetFloat("movement_magnitude",movementMagnitude);
	    
		animLink.SetBool("grounded",movementSystem.groundChecker.isGrounded);
		animLink.SetBool("swordIsOn",weaponIsOn);
		
		if(Input.GetKeyDown(defaultAttackKey))attackKeyStickTime=attackKeyStickTimeDelay;
		if(Input.GetKeyDown(juggleryAttackKey))juggleryAttackKeyAttackKeyStickTime=juggleryAttackKeyStickTimeDelay;
		 

		animLink.SetBool("makeAttackJugglery",juggleryAttackKeyAttackKeyStickTime>0);

		animLink.SetBool("makeAttackDefault",attackKeyStickTime>0);
		 
	    
    }
}
