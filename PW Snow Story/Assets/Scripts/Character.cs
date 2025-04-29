using System;
using UnityEngine;



public class Character : MonoBehaviour
{

	public bool isGrounded;
	public bool isAttacking;

	
   	public CharacterMovement movementSystem;
	public AnimationSystem animationSystem;
	public InventorySystem inventorySystem;
	public Weapon weapon;
	public DirectionPointer directionPointer;
    public Transform lookAtPoint;
	public Transform targetLookAtPoint;
	



    // Start is called before the first frame update
    void Start()
    {
       
    }
	public void HandleMovement_FixedUpdate(Vector2 moveVector)
	{
		
		movementSystem.MoveByCamera(moveVector);
		if(animationSystem.isBlocking)
		{
			if(directionPointer.target!=null)movementSystem.RotateCharacterToDirectionPointer(directionPointer.transform);
			
		}
		else
		{
			movementSystem.RotateCharacterByCamera(moveVector,directionPointer.transform);
		}
	}
	public void HandleAnimation_Update(Vector2 moveVector)
	{
		animationSystem.AnimateByMovement(moveVector);
	}

	
	public void CheckLockTarget_Update()
	{
		if(animationSystem.isBlocking)
		{
			GetWeaponInArmBackGrip();
			if(directionPointer.target!=null)
			{
				directionPointer.isTargetLocked=true;
				
				
				
			}
			else
			{
				directionPointer.isTargetLocked=true;
				
				
			}
			
		}
		else
		{
			
			directionPointer.isTargetLocked=false;
			directionPointer.target = null;
			
			
		}
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
		isAttacking = animationSystem.attackKeyStickTime>0;
		
	}
    void Update()
    {
	    
		
		CheckLockTarget_Update();
		
	    
	    
	    
    }
}
