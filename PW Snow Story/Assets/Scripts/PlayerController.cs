using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public CharacterMovement movementSystem;
	public AnimationSystem animationSystem;
	public InventorySystem inventorySystem;
	public SwordSystem swordSystem;
	
	public Vector2 moveVector;
	public Vector2 cameraVector;
	
	
	
	// Start is called before the first frame update
    void Start()
    {
        
    }
	void HandleMovement()
	{
		movementSystem.MoveByCamera(moveVector);
		movementSystem.RotateCharacterByCamera(moveVector);
	}
	void HandleAnimationFixedUpdate()
	{
		animationSystem.AnimateByInput(moveVector);
	}

	public void GetSwordInArm()
	{
		swordSystem.SetOnArm();
	}
	public void GetSwordInBack()
	{
		swordSystem.SetOnSpine();
	}
	
    // Update is called once per frame
	void MakeAttack()
	{
		
	}


	void UserInputUpdate()
	{
		moveVector.x = Input.GetAxis("Horizontal");
	    moveVector.y = Input.GetAxis("Vertical");
		
	}

	void FixedUpdate()
	{
		HandleAnimationFixedUpdate();
		HandleMovement();
	}
    void Update()
    {
	    

		UserInputUpdate();
	    
	    
	    
    }
}
