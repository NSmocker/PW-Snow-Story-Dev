using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.VFX;

public class CharacterMovement : MonoBehaviour
{

	public AudioClip jump_sound;
	public AudioClip second_jump_sound;
	public GameObject VFX_jump;
	
	public AudioSource audio_source;
	public UnityEvent OnJump;
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
	public float gravity_multiplier; 

	

	public void MoveByCamera(Vector2 input)
	{ 
		

		
		if(input.magnitude>0.5 && !groundChecker.isGrounded)
		{ character_controller.Move(transform.forward*input.magnitude*move_speed*Time.deltaTime);
			/*
			var main_direction = new Vector3(input.x,0,input.y)*move_speed;
			var local_direction = direction_pointer_transform.TransformDirection(main_direction)*Time.deltaTime;
			character_controller.Move(local_direction*Time.deltaTime);*/
		} 
	}
	
	public void MakeJump()
	{
	
    if (groundChecker.isGrounded)
    {
        second_jump_maked = false;
        velocity.y = jump_force;
		audio_source.PlayOneShot(jump_sound);
		OnJump.Invoke();

    }

    else if (!second_jump_maked && !groundChecker.isGrounded)
    {
        // Start the second jump
        second_jump_maked = true;
        velocity.y = jump_force;
		audio_source.PlayOneShot(second_jump_sound);
		var vfx = Instantiate(VFX_jump, transform.position, Quaternion.identity);
		Destroy(vfx, 4f);
		OnJump.Invoke();
    }
		 
	}
	public void MakeGravity()
	{
		 

		if ( groundChecker.isGrounded && gravity.y< 0f)
		{
		
		  gravity.y = -1 ;
		}
		else
		{
		  velocity += gravity * Time.deltaTime * gravity_multiplier ;
		}
		 
		character_controller.Move(velocity*Time.deltaTime);
		
		
	}
	public void RotateCharacterByCamera(Vector2 input)
	{
	
		if(input.magnitude>0.2)
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
		 
    }

	void FixedUpdate()
	{
		
	}
    // Update is called once per frame
    void Update()
	{ 
		MakeGravity();
		if (Input.GetButtonDown("Jump")) MakeJump(); 	
    }
}
