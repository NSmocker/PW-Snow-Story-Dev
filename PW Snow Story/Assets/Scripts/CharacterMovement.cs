using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.VFX;

public class CharacterMovement : MonoBehaviour
{

	public Character masterCharacter;
	[Header("Audio Bindings")]
	public AudioSource audioSource;
	public AudioClip jumpSound;
	public AudioClip secondJumpSound;
	public GameObject vfxJump;
	
	[Header("System Vars")]
	public UnityEvent OnJump;
	public CharacterController characterController;
	public float moveSpeed;
	public float jumpForce=25f;
	public float rotationSpeed = 180f;

	
	public Vector3 velocity;
	public float sprintMultipliyer;
	public Vector3 gravity;
	public float gravityMultiplier; 


	[Header("States")]
	public bool isFloating;
	public bool wasComboInFloat;
	public bool isSecondJumpMaked;

	[Header("Ground Checker")]
  	public float radius_offset = 0.1f;
    public LayerMask groundLayer;
    public bool isGrounded ;
	
	public float animatorMoveSpeed; // Поточна швидкість від аніматора
    private bool wasGroundedLastFrame = true;

    // Викликається з AnimationSystem.OnAnimatorMove
    public void UpdateAnimatorMoveSpeed(Vector3 deltaPosition)
    {
        // Швидкість у площині XZ
        animatorMoveSpeed = new Vector2(deltaPosition.x, deltaPosition.z).magnitude / Time.deltaTime;
    }

    //Використовується коли персонаж в повітрі
    public void MoveByCamera(Vector2 input)
	{ 
		if(input.magnitude>0.5 && !isGrounded)
		{
			if(!masterCharacter.isAttacking)
			{
				// Використовуємо animatorMoveSpeed для плавності переходу
				characterController.Move(transform.forward*input.magnitude*moveSpeed*Time.fixedDeltaTime);
			}
		} 
	}
	public void MakeJump()
	{
	
		if (isGrounded)
		{
			moveSpeed = animatorMoveSpeed; 
			wasComboInFloat =false;
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
	public void MakeGravity_FixedUpdate()
	{	 
 		velocity += gravity * Time.fixedDeltaTime * gravityMultiplier/10 ;
		if ( isGrounded && velocity.y< 0f)
		{

			velocity.y = -3f;
		}
		characterController.Move(velocity);
		if(isFloating)velocity.y = 0;
	}
	
	public void RotateCharacterByCamera(Vector2 input,Transform directionPointer)
	{
		if (this.enabled == false) return;
		if (input.normalized.magnitude >= 0.05f)
		{
			Vector3 targetDirection = directionPointer.TransformDirection(new Vector3(input.x, 0, input.y));
			targetDirection.y = 0; // Ігноруємо вертикальну складову
			targetDirection.Normalize(); // Нормалізуємо вектор

			Quaternion targetRotation = Quaternion.LookRotation(targetDirection, Vector3.up);
			characterController.transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
		}
	}
	public void RotateCharacterToDirectionPointer(Transform directionPointer)
	{
		characterController.transform.rotation = directionPointer.rotation;
		print("Character rotation to direction pointer");
	}
	void CheckGround_FixedUpdate()
	{
		isGrounded = Physics.CheckSphere(transform.position, characterController.radius+radius_offset, groundLayer);
	}
  
	public void FastLandDown()
	{
	  isFloating=false;
	  velocity.y=-0.5f;
	}


  /********* EVENTS *********/
    void Start()
    {
		 
    }

	void FixedUpdate()
	{	
		/*// Відстежуємо перехід із grounded у повітря
        if (wasGroundedLastFrame && !isGrounded)
        {
            // При стрибку moveSpeed = animatorMoveSpeed
            moveSpeed = animatorMoveSpeed;
        }*/
		
        wasGroundedLastFrame = isGrounded;
		CheckGround_FixedUpdate();
		MakeGravity_FixedUpdate();
		// Додаємо ручний рух у повітрі
		if (!isGrounded && masterCharacter != null)
		{
			MoveByCamera(masterCharacter.lastMoveInput);
		}
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
