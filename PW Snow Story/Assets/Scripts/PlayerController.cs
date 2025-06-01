using System.Collections;
using System.Collections.Generic;
using Unity.Cinemachine;

using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;




// **************************************************/
//Скріпт відповідає за інпути локального гравця для контролю над обраним персонажем. 
/******************************************************/
public class PlayerController : MonoBehaviour
{
	public Character characterToControll;
	
	public DirectionPointer directionPointer;
	public CameraModeSwitcher cameraSwitcher;
	public Volume volume;
	MotionBlur motionBlur;



	

	

	[Header("KeysBinding")]
	public Vector2 moveVector;
	public KeyCode defaultAttackKey = KeyCode.Mouse0;
	public KeyCode juggleryAttackKey = KeyCode.Mouse0;
	public KeyCode blockKey = KeyCode.Mouse1;
	public string sprintButton = "Sprint";
	public KeyCode getSwordKey = KeyCode.V;



    
	void Start()
	{
		Cursor.lockState = CursorLockMode.Locked;
		BindPlayerSystems();
		volume.profile.TryGet<MotionBlur>(out motionBlur);
	}

	public void BindPlayerSystems()
	{
		directionPointer = characterToControll.directionPointer;
		cameraSwitcher.orbitalCamera.Target.TrackingTarget = characterToControll.lookAtPoint;
		cameraSwitcher.orbitalCamera.Target.LookAtTarget = characterToControll.lookAtPoint;
		
		
		

	
	
	}





    void Update()
    {

		if (Time.timeScale == 0) return;	
		#region KeyReading
        moveVector.x = Input.GetAxis("Horizontal");
	    moveVector.y = Input.GetAxis("Vertical");
		if (Input.GetButtonDown("Jump")) characterToControll.movementSystem.MakeJump(); 
	    characterToControll.animationSystem.SetSprintState(Input.GetAxis("Sprint"));
		if (Input.GetKeyDown(defaultAttackKey)) characterToControll.animationSystem.MakeAttack_Click();
		if(Input.GetKeyDown(juggleryAttackKey))characterToControll.animationSystem.MakeAttackJugglery_Click();
		#endregion


		motionBlur.active = Input.GetAxis("Sprint")!=0;


		
		characterToControll.HandleAnimation_Update(moveVector);

    }
	
	void FixedUpdate()
    {
        characterToControll.HandleMovement_FixedUpdate(moveVector);

		cameraSwitcher.cameraOffseted = !characterToControll.isGrounded || characterToControll.isAttacking;
	

    }



}
