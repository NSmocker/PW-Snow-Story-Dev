using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
	public CharacterController character_controller;
	public float move_speed;
	public float rotationSpeed = 180f;
	public Transform character_origin;
	public Transform direction_pointer_transform;
	public Vector3 velocity;
	public float sprint_multipliyer;
	

	public void MoveByCamera(Vector2 input)
	{
		if(input.magnitude>0.5)
		{
			var main_direction = new Vector3(input.x,0,input.y)*(move_speed+(sprint_multipliyer*10));
		var local_direction = direction_pointer_transform.TransformDirection(main_direction)*Time.deltaTime;
		character_controller.Move(local_direction);
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
			print("Trying to rotatate");
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

    // Update is called once per frame
    void Update()
	{
		sprint_multipliyer = Input.GetAxis("Sprinting");
	    velocity = character_controller.velocity;
    }
}
