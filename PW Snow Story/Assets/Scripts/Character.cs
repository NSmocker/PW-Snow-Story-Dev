using System;
using System.Collections;
using UnityEngine;



public class Character : MonoBehaviour
{

	public bool isGrounded;
	public bool isAttacking;
	public bool isMining;
	
	public Vector2 lastMoveInput;
	
   	public CharacterMovement movementSystem;
	public AnimationSystem animationSystem;
	public InventorySystem inventorySystem;
	public MineDiggerSystem mineDiggerSystem;
	public Weapon weapon;
	public HitBox weaponHitBox;
	public DirectionPointer directionPointer;
	public TargetPointer targetPointer;
    public Transform lookAtPoint;



	void OnAnimatorMove()
	{
		if (animationSystem.animator == null) return;
		// Отримуємо рух від аніматора
		Vector3 deltaPosition = animationSystem.animator.deltaPosition;
		// Якщо спринт, множимо на sprintMoveSpeedMultiplier
		if (animationSystem.isSprinting)
			deltaPosition *= animationSystem.sprintMoveSpeedMultiplier;
		// Переміщуємо персонажа
		transform.position += deltaPosition;
		// Обертання залишаємо як є
		transform.rotation = animationSystem.animator.rootRotation;
		movementSystem.UpdateAnimatorMoveSpeed(animationSystem.animator.deltaPosition);
		print(movementSystem.animatorMoveSpeed);
		//print("Animator Move! " + deltaPosition + " | Sprint: " + animationSystem.isSprinting + " | Multiplier: " + animationSystem.sprintMoveSpeedMultiplier);
	}

    // Start is called before the first frame update
    void Start()
    {
       weaponHitBox.masterCharacter = this;
    }
	public void HitBoxPushForward()
	{
		weaponHitBox.pushForward = true;
	}
	public void HitBoxPushForwardOff()
	{
		weaponHitBox.pushForward = false;
	 }
	public void HitBoxPushUp()
	{
		weaponHitBox.pushUp = true;
	}
	public void HitBoxPushUpOff()
	{
		weaponHitBox.pushUp = false;
	}
	public void HitBoxResetEnemyVelecity()
	{
		weaponHitBox.resetEnemyVelocity = true;
	}
	public void HitBoxOffResetEnemyVelecity()
	{
		weaponHitBox.resetEnemyVelocity = false;
	}

	public void ActivateHitBox_AnimationEvent()
	{
		weaponHitBox.Activate();
	}

	public void DeactivateHitBox_AnimationEvent()
	{
		weaponHitBox.Deactivate();
	}
	

	public void RotateToNearestTarget_AnimationEvent()
	{	
		if(targetPointer.nearestTarget!=null)
		{
			
			//print("RotateToNearestTarget_AnimationEvent");
			targetPointer.MakeNearestToTarget();
			
			if(targetPointer.distanceToTarget!= -1f && targetPointer.distanceToTarget < targetPointer.minDistanceToAttackLook)
			{
				
				targetPointer.LookAtTarget();
				movementSystem.RotateCharacterToDirectionPointer(targetPointer.transform);
			}
		}
		else
		{
			//print("No nearest target to rotate to");
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
//				print("RotateCharacterByCamera");
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
//		print("Set On Arm");	
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
		
			movementSystem.isFloating=true;
			movementSystem.wasComboInFloat=true;
		
	}

	public void SetFallingState()
	{
		movementSystem.isFloating=false;
		 
	}
	public void FastLandDown_AnimationEvent()
	{
		movementSystem.FastLandDown();
	}
	

    // Update is called once per frame

	void FixedUpdate()
	{
		isGrounded = movementSystem.isGrounded;
		isAttacking = animationSystem.isAttacking;
		isMining = mineDiggerSystem.isMining;
	
	}
    void Update()
    {
	    
		if(isAttacking)
		{
			RotateToNearestTarget_AnimationEvent();
			GetWeaponInArm();
			print("isAttacking");
		}
		
	    
	    
	    
    }
}
