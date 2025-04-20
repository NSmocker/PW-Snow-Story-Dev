using System.Collections;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public CharacterMovement movementSystem;
	public AnimationSystem animationSystem;
	public InventorySystem inventorySystem;
	public SwordSystem swordSystem;
	public DirectionPointer directionPointer;

	public CameraModeSwitcher cameraModeSwitcher;
	
	
	public Vector2 moveVector;
	public Vector2 cameraVector;
	
	
	
	// Start is called before the first frame update
    void Start()
    {
       
    }
	void HandleMovement_FixedUpdate()
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
	void HandleAnimation_Update()
	{
		animationSystem.AnimateByInput(moveVector);
	}

	
	public void CheckLockTarget_Update()
	{
		if(animationSystem.isBlocking)
		{
			GetSwordInArmBackGrip();
			if(directionPointer.target!=null)
			{
				directionPointer.isTargetLocked=true;
				cameraModeSwitcher.ToTargetGroupCamera();
				
				
			}
			else
			{
				directionPointer.isTargetLocked=true;
				
				
			}
			
		}
		else
		{
			cameraModeSwitcher.ToOrbitalCamera();
			directionPointer.isTargetLocked=false;
			directionPointer.target = null;
			
			
		}
	}
	public void GetSwordInArm()
	{
		swordSystem.SetOnArm();
		
	}
	public void GetSwordInArmBackGrip()
	{
		swordSystem.SetOnArmBackGrip();
		
	}
	public void GetSwordInBack()
	{
		if(!animationSystem.isBlocking) swordSystem.SetOnSpine();
		
	}
	public void RequestToSpineSwordFromAnimation()
	{
		if(!animationSystem.isBlocking) animationSystem.AnimateSwordOnSpine();
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
	void MakeAttack()
	{
		
	}


	void UserInput_Update()
	{
		moveVector.x = Input.GetAxis("Horizontal");
	    moveVector.y = Input.GetAxis("Vertical");
		
	}

	void FixedUpdate()
	{
		
		HandleMovement_FixedUpdate();
	}
    void Update()
    {
	    
		HandleAnimation_Update();
		CheckLockTarget_Update();
		UserInput_Update();
	    
	    
	    
    }
}
