using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public CharacterMovement movement_system;
	
	public Vector2 move_vector;
	public Vector2 camera_vector;
	
	
	
	// Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
	    move_vector.x = Input.GetAxis("Horizontal");
	    move_vector.y = Input.GetAxis("Vertical");
	    movement_system.MoveByCamera(move_vector);
    }
}
