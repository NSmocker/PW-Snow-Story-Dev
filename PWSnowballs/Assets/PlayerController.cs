using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public CharacterMovement movement_system;
	public AnimationSystem animation_system;
	
	public Vector2 move_vector;
	public Vector2 camera_vector;
	
	
	
	// Start is called before the first frame update
    void Start()
    {
        
    }
	void HandleMovement()
	{
		movement_system.MoveByCamera(move_vector);
		movement_system.RotateCharacterByCamera(move_vector);
	}
	void HandleAnimation()
	{
		animation_system.AnimateByInput(move_vector);
	}
    // Update is called once per frame
    void Update()
    {
	    move_vector.x = Input.GetAxis("Horizontal");
	    move_vector.y = Input.GetAxis("Vertical");
	    HandleMovement();
	    HandleAnimation();
	    
    }
}
