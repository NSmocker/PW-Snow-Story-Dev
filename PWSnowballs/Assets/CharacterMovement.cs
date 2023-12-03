using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
	public CharacterController character_controller;
	public float move_speed;
	public Transform direction_pointer_transform;

	public void MoveByCamera(Vector2 input)
	{
		var main_direction = new Vector3(input.x,0,input.y)*move_speed;
		var local_direction = direction_pointer_transform.TransformDirection(main_direction)*Time.deltaTime;
		character_controller.Move(local_direction);
	}
	
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
