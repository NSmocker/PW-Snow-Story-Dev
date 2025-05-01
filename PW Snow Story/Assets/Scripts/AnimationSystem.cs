using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationSystem : MonoBehaviour
{


	public Character masterCharacter;


	[Header("System Vars")]
	public Animator animator;
	public float movementMagnitude;
	public Vector2 moveVector;

	[Header("States")]
	public bool weaponIsOn; 
	public bool isBlocking;
	public bool isAttacking;

    
	
	[Header("Attacks")]
	public float attackKeyStickTime ;
	public float attackKeyStickTimeDelay = 0.1f;

	float attackStatusTime = 0.5f;

	public float juggleryAttackKeyAttackKeyStickTime ;
	public float juggleryAttackKeyStickTimeDelay = 0.1f;
	

	public AnimationCurve blockCurve;
	public void SetBlockingState(bool BlockingState,float blockAxis)
	{	
		if(!isAttacking)
		{
			animator.SetLayerWeight(2,blockAxis);
			animator.SetBool("Block/Lock",BlockingState);
			isBlocking=BlockingState;
		}else
		{
			animator.SetLayerWeight(2,0);
			animator.SetBool("Block/Lock",false);
			isBlocking=false;
		}
	  	


	}

	public void AnimateByMovement(Vector2 moveInfo)
	{
		movementMagnitude = moveInfo.magnitude;
		moveVector= moveInfo;

	}
    // Start is called before the first frame update
    void Start()
    {
		masterCharacter = transform.parent.parent.GetComponent<Character>();
		if(!masterCharacter)Debug.LogError("Master character system is not assigned!");

		#region Event Bindings
		masterCharacter.movementSystem.OnJump.AddListener(AnimateJump);
		#endregion

    }
	void AnimateJump() 
	{
		animator.SetTrigger("jump");
	}
	public void AnimateWeaponOnSpine()
	{
		animator.SetTrigger("SetSwordOnSpine");
	}
    // Update is called once per frame
   
	public void MakeAttack_Click()
	{   
		attackStatusTime = 0.5f;
		attackKeyStickTime=attackKeyStickTimeDelay;
	}
	public void MakeAttackJugglery_Click()
	{	
		attackStatusTime = 0.5f;
		juggleryAttackKeyAttackKeyStickTime=juggleryAttackKeyStickTimeDelay;
	}


	void Timers_Update()
	{
		if(attackStatusTime>0)attackStatusTime-=Time.deltaTime;
		if(attackKeyStickTime>0)attackKeyStickTime-=Time.deltaTime;
		if(juggleryAttackKeyAttackKeyStickTime>0)juggleryAttackKeyAttackKeyStickTime-=Time.deltaTime;
	}

    void Update()
    {

		if(Time.timeScale == 0) return;
		Timers_Update();
		isAttacking = attackStatusTime > 0;
		animator.applyRootMotion = masterCharacter.movementSystem.isGrounded;
		animator.SetBool("wasComboInFloat",masterCharacter.movementSystem.wasComboInFloat);
	    animator.SetFloat("movementMagnitude",movementMagnitude);
	    animator.SetFloat("movementHorizontal",moveVector.x);
	    animator.SetFloat("movementVertical",moveVector.y);
		animator.SetBool("grounded",masterCharacter.movementSystem.isGrounded);
		animator.SetBool("makeAttackJugglery",juggleryAttackKeyAttackKeyStickTime>0);
		animator.SetBool("makeAttackDefault",attackKeyStickTime>0);
		 
	    
    }
}
