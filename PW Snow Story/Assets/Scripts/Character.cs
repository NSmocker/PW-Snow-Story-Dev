using System;
using System.Collections;
using UnityEngine;



public class Character : MonoBehaviour
{

	public bool isGrounded;
	public bool isAttacking;
	public bool isBlocking;

	
   	public CharacterMovement movementSystem;
	public AnimationSystem animationSystem;
	public InventorySystem inventorySystem;
	public Weapon weapon;
	public DirectionPointer directionPointer;
	public TargetPointer targetPointer;
    public Transform lookAtPoint;
	
	



    // Start is called before the first frame update
    void Start()
    {
       
    }
	public void RotateToNearestTarget_AnimationEvent()
	{	
		if(targetPointer.nearestTarget!=null)
		{
			
			print("RotateToNearestTarget_AnimationEvent");
			
			
			if(targetPointer.distanceToTarget!= -1f && targetPointer.distanceToTarget < targetPointer.minDistanceToAttackLook)
			{
				targetPointer.MakeNearestToTarget();
				targetPointer.LookAtTarget();
				movementSystem.RotateCharacterToDirectionPointer(targetPointer.transform);
			}
		}
		else
		{
			print("No nearest target to rotate to");
		}
	}
	 


	public void HandleMovement_FixedUpdate(Vector2 moveVector)
	{
		
		movementSystem.MoveByCamera(moveVector);
		if(animationSystem.isBlocking)
		{
			if(targetPointer.target==null)targetPointer.MakeNearestToTarget();
			if(targetPointer.target!=null)
			{
			  targetPointer.LookAtTarget();
			  movementSystem.RotateCharacterToDirectionPointer(targetPointer.transform);
			}
			
		}
		else
		{	
			
			if(moveVector.magnitude!=0 && !animationSystem.isAttacking )
			{
				movementSystem.RotateCharacterByCamera(moveVector,directionPointer.transform);
				print("RotateCharacterByCamera");
			}
		}
	}
	public void HandleAnimation_Update(Vector2 moveVector)
	{
		animationSystem.AnimateByMovement(moveVector);
	}

	
	
	public void GetWeaponInArm()
	{
		weapon.SetOnArm();
		
	}
	public void GetWeaponInArmBackGrip()
	{
		weapon.SetOnArmBackGrip();
		
	}
	public void GetWeaponInBack()
	{
		if(!animationSystem.isBlocking) weapon.SetOnSpine();
		
	}
	public void RequestToSpineWeaponFromAnimation()
	{
		if(!animationSystem.isBlocking) animationSystem.AnimateWeaponOnSpine();
	}
	public void AirComboFloatStart()
	{
		if(!movementSystem.wasComboInFloat)
		{
			movementSystem.isFloating=true;
			movementSystem.wasComboInFloat=true;
		}
	}
	public void AirComboFloatEnd()
	{
		movementSystem.isFloating=false;
	}
	

    // Update is called once per frame

	void FixedUpdate()
	{
		isGrounded = movementSystem.isGrounded;
		isAttacking = animationSystem.isAttacking;
		isBlocking = animationSystem.isBlocking;
	}
    void Update()
    {
	    
		if(isAttacking)
		{
			RotateToNearestTarget_AnimationEvent();
			GetWeaponInArm();
		
		}
		if(isBlocking)
		{
			GetWeaponInArmBackGrip();
		}
	    
	    
	    
    }
}
