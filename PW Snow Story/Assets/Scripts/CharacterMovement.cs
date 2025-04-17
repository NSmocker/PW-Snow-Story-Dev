using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.VFX;

public class CharacterMovement : MonoBehaviour
{

	public AudioClip jumpSound;
	public AudioClip secondJumpSound;
	public GameObject vfxJump;
	
	public AudioSource audioSource;
	public UnityEvent OnJump;
	public CharacterController characterController;
	public float moveSpeed;
	public float jumpForce=25f;
	public float rotationSpeed = 180f;

	public Transform directionPointerTransform;
	public Vector3 velocity;
	public float sprintMultipliyer;
	bool isSecondJumpMaked;

	public Vector3 gravity;
	public float gravityMultiplier; 

	public bool isFloating;
	public bool wasComboInFloat;

	[Header("Ground Checker")]
  	public float radius_offset = 0.1f;
    public LayerMask groundLayer;
    public bool isGrounded ;
	

	public void MoveByCamera(Vector2 input)
	{ 
		

		
		if(input.magnitude>0.5 && !isGrounded)
		{ characterController.Move(transform.forward*input.magnitude*moveSpeed*Time.fixedDeltaTime);
			/*
			var main_direction = new Vector3(input.x,0,input.y)*move_speed;
			var local_direction = direction_pointer_transform.TransformDirection(main_direction)*Time.deltaTime;
			character_controller.Move(local_direction*Time.deltaTime);*/
		} 
	}
	
	public void MakeJump()
	{
	
    if (isGrounded)
    {	wasComboInFloat=false;
        isSecondJumpMaked = false;
        velocity.y = jumpForce*Time.fixedDeltaTime;
		audioSource.PlayOneShot(jumpSound);
		OnJump.Invoke();

    }

    else if (!isSecondJumpMaked && !isGrounded)
    {
        // Start the second jump
        isSecondJumpMaked = true;
        velocity.y = jumpForce*Time.fixedDeltaTime;
		audioSource.PlayOneShot(secondJumpSound);
		var vfx = Instantiate(vfxJump, transform.position, Quaternion.identity);
		Destroy(vfx, 4f);
		OnJump.Invoke();
    }
		 
	}
	public void MakeGravity()
	{
		 
 		velocity += gravity * Time.fixedDeltaTime * gravityMultiplier/10 ;
		if ( isGrounded && velocity.y< 0f)
		{
		
		  velocity.y=0;
		}
		
		characterController.Move(velocity);
		
		
	}
	
	public void RotateCharacterByCamera(Vector2 input)
	{
    if (this.enabled == false) return;

    if (input.normalized.magnitude >= 0.05f)
    {
        Vector3 targetDirection = directionPointerTransform.TransformDirection(new Vector3(input.x, 0, input.y));
        targetDirection.y = 0; // Ігноруємо вертикальну складову
        targetDirection.Normalize(); // Нормалізуємо вектор

        Quaternion targetRotation = Quaternion.LookRotation(targetDirection, Vector3.up);
        characterController.transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}
    // Start is called before the first frame update
    void Start()
    {
		 
    }

	void FixedUpdate()
	{	
		isGrounded = Physics.CheckSphere(transform.position, characterController.radius+radius_offset, groundLayer);
		MakeGravity();
		if(isFloating)velocity.y = 0;
	}
    // Update is called once per frame
    void Update()
	{ 
		
		if (Input.GetButtonDown("Jump")) MakeJump(); 	
    }
	void OnDrawGizmos()
	{
        if (characterController != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, characterController.radius + radius_offset);
        }
    }

}
