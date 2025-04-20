using System.Collections;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public CharacterMovement movementSystem;
	public AnimationSystem animationSystem;
	public InventorySystem inventorySystem;
	public SwordSystem swordSystem;

	public GameObject freeLookCameraMode;
	public GameObject targetCameraMode;
	
	
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
			if(movementSystem.directionPointer.target!=null)movementSystem.RotateCharacterToDirectionPointer();
			
		}
		else
		{
			movementSystem.RotateCharacterByCamera(moveVector);
		}
	}
	void HandleAnimation_Update()
	{
		animationSystem.AnimateByInput(moveVector);
	}

	void AlignFreeLookToCurrentView()
	{
		var brain = Camera.main.GetComponent<CinemachineBrain>();
		if (brain == null) return;

		var freeLook = freeLookCameraMode.GetComponent<CinemachineFreeLook>();
		if (freeLook == null) return;

		Transform brainTransform = brain.OutputCamera.transform;

		// Копіюємо позицію та напрямок
		freeLook.transform.position = brainTransform.position;
		freeLook.transform.rotation = brainTransform.rotation;

		// Прив'язуємо осі (можна покращити з більш точним обрахунком кута)
		Vector3 forward = brainTransform.forward;
		float pitch = Vector3.SignedAngle(Vector3.ProjectOnPlane(forward, Vector3.right), Vector3.up, Vector3.right);
		freeLook.m_YAxis.Value = Mathf.InverseLerp(-30f, 70f, pitch); // поправ залежно від твоїх налаштувань
	}
	public void CheckLockTarget_Update()
	{
		if(animationSystem.isBlocking)
		{
			GetSwordInArmBackGrip();
			if(movementSystem.directionPointer.target!=null)
			{
				movementSystem.directionPointer.isTargetLocked=true;
				freeLookCameraMode.SetActive(false);
				targetCameraMode.SetActive(true);
				freeLookCameraMode.GetComponent<CinemachineInputAxisController>().enabled=false;
			}
			else
			{
				movementSystem.directionPointer.isTargetLocked=true;
				freeLookCameraMode.GetComponent<CinemachineInputAxisController>().enabled=false;
				
			}
			
		}
		else
		{
			AlignFreeLookToCurrentView();
			freeLookCameraMode.SetActive(true);
			targetCameraMode.SetActive(false);
			movementSystem.directionPointer.isTargetLocked=false;
			movementSystem.directionPointer.target = null;
			freeLookCameraMode.GetComponent<CinemachineInputAxisController>().enabled=true;
			
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
