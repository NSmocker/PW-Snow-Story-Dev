using System.Collections;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEditor.Experimental.GraphView;
using UnityEngine;




// **************************************************/
//Скріпт відповідає за інпути локального гравця для контролю над обраним персонажем. 
/******************************************************/
public class PlayerController : MonoBehaviour
{
	public Character characterToControll;
	
	[Header("KeysBinding")]
	public Vector2 moveVector;
	public KeyCode defaultAttackKey = KeyCode.Mouse0;
	public KeyCode juggleryAttackKey = KeyCode.Mouse0;
	public KeyCode blockKey = KeyCode.Mouse1;
	 
	public KeyCode getSwordKey = KeyCode.V;

    
    public void BindPlayerSystems()
	{

	}



    void Update()
    {
		#region KeyReading
        moveVector.x = Input.GetAxis("Horizontal");
	    moveVector.y = Input.GetAxis("Vertical");
		if (Input.GetButtonDown("Jump")) characterToControll.movementSystem.MakeJump(); 
		if(Input.GetKeyDown(blockKey))
		{
			characterToControll.animationSystem.SetBlockingState(true);
		}
		if(Input.GetKeyUp(blockKey))
		{
			characterToControll.animationSystem.SetBlockingState(false);
		}






		#endregion



		characterToControll.HandleAnimation_Update(moveVector);

    }
	
	void FixedUpdate()
    {
        characterToControll.HandleMovement_FixedUpdate(moveVector);
    }



}
