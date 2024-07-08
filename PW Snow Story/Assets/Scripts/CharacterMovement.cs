using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterMovement : MonoBehaviour
{
	
	public UnityEvent on_jump;
	public CharacterController character_controller;
	public GroundChecker groundChecker;
	public float move_speed;
	public float jump_force=25f;
	public float rotationSpeed = 180f;
	public Transform character_origin;
	public Transform direction_pointer_transform;
	public Vector3 velocity;
	public float sprint_multipliyer;
	bool second_jump_maked;
	public Vector3 gravity;
	public Vector3 gravity_scaler = new Vector3(1f,1f,1f); 
	public float gravity_accelerator=0.5f; 
	

	public void MoveByCamera(Vector2 input)
	{/*
		if(input.magnitude>0.5)
		{
		var main_direction = new Vector3(input.x,0,input.y)*(move_speed+(sprint_multipliyer*10));
		var local_direction = direction_pointer_transform.TransformDirection(main_direction)*Time.deltaTime;
		character_controller.Move(local_direction);
		}*/
	}
	
	public void MakeJump()
	{	 

    if (groundChecker.isGrounded)
    {
        second_jump_maked = false;
        gravity.y = jump_force;
    }
    else if (!second_jump_maked && !groundChecker.isGrounded)
    {
        // Start the second jump
        second_jump_maked = true;
	    character_controller.SimpleMove(Vector3.zero);
        gravity.y = jump_force;
    }
		 
	}
	public void MakeGravity()
	{
		if (gravity.y>-20f)
		{
		
		  gravity.y -= 9.8f * Time.deltaTime;
		  character_controller.Move(gravity*gravity_accelerator);
		}
	}
	public void RotateCharacterByCamera(Vector2 input)
	{
	
		if(input.magnitude>0.4)
		{
			// Отримати напрямок камери
			Vector3 cameraForward = Camera.main.transform.forward;
			Vector3 cameraRight = Camera.main.transform.right;

			// Забезпечити, що рух відбувається плоско відносно землі
			cameraForward.y = 0f;
			cameraRight.y = 0f;
			cameraForward.Normalize();
			cameraRight.Normalize();
			
			// Визначити напрямок обертання
			Vector3 rotateDirection = input.x * cameraRight + input.y * cameraForward;

			// Обертати персонажа
			if (rotateDirection != Vector3.zero)
			{
				Quaternion toRotation = Quaternion.LookRotation(rotateDirection, Vector3.up);
				character_origin.rotation = Quaternion.RotateTowards(character_origin.rotation, toRotation, rotationSpeed * Time.deltaTime);
			}
		}

		
	
	}
    // Start is called before the first frame update
    void Start()
    {
        on_jump.AddListener(MakeJump);
		 
    }

    // Update is called once per frame
    void Update()
	{ 
		MakeGravity();
		/*sprint_multipliyer = Input.GetAxis("Sprint");*/
	    velocity = character_controller.velocity;
		
		if(Input.GetButtonDown("Jump"))on_jump.Invoke();
    }
}
