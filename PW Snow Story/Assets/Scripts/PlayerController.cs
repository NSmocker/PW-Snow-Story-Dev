using System.Collections;
using System.Collections.Generic;
using Unity.Cinemachine;

using UnityEngine;




// **************************************************/
//Скріпт відповідає за інпути локального гравця для контролю над обраним персонажем. 
/******************************************************/
public class PlayerController : MonoBehaviour
{
	public Character characterToControll;
	
	public DirectionPointer directionPointer;
	public CameraModeSwitcher cameraSwitcher;

	

	

	[Header("KeysBinding")]
	public Vector2 moveVector;
	public KeyCode defaultAttackKey = KeyCode.Mouse0;
	public KeyCode juggleryAttackKey = KeyCode.Mouse0;
	public KeyCode blockKey = KeyCode.Mouse1;
	 
	public KeyCode getSwordKey = KeyCode.V;



    
	void Start()
	{
		Cursor.lockState = CursorLockMode.Locked;
		BindPlayerSystems();
	}

    public void BindPlayerSystems()
	{
		directionPointer = characterToControll.directionPointer;
		

	
	
	}





    void Update()
    {
		#region KeyReading
        moveVector.x = Input.GetAxis("Horizontal");
	    moveVector.y = Input.GetAxis("Vertical");
		if (Input.GetButtonDown("Jump")) characterToControll.movementSystem.MakeJump(); 
		
		characterToControll.animationSystem.SetBlockingState(Input.GetButton("Block"),Input.GetAxis("BlockAxis"));
		


		if(Input.GetKeyDown(defaultAttackKey))characterToControll.animationSystem.MakeAttack_Click();
		if(Input.GetKeyDown(juggleryAttackKey))characterToControll.animationSystem.MakeAttackJugglery_Click();





		#endregion



		characterToControll.HandleAnimation_Update(moveVector);

    }
	
	void FixedUpdate()
    {
        characterToControll.HandleMovement_FixedUpdate(moveVector);

		cameraSwitcher.cameraOffseted = !characterToControll.isGrounded || characterToControll.isAttacking;
	

    }



}
