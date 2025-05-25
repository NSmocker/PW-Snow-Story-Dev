using UnityEngine;

public class MonsterMovement : MonoBehaviour
{
    public CharacterController characterController;

    public float moveSpeed = 5f; // Speed of the monster movement


    public float gravityAcceleration = -1f; // Gravity acceleration
    public Vector3 velocity;
    public Vector3 fadingVelocity;
    public float fadeSpeed = 0.1f; // Speed at which the monster fades out


    [Header("Ground Checker")]
  	public float radius_offset = 0.1f;
    public LayerMask groundLayer;
    public bool isGrounded;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        characterController = GetComponent<CharacterController>();

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PushIntoDirection(Vector3 direction)
    {


        // Apply fading effect
        fadingVelocity = direction;
    }
    public void ResetVeliocity()
    {
        velocity = Vector3.zero;
        fadingVelocity = Vector3.zero;
    }
    public void FadeVelocity_FixedUpdate()
    {
        if (Mathf.Abs(fadingVelocity.x) < 0.01)
        {
            fadingVelocity.x = 0;
        }
        else
        {
            fadingVelocity.x = Mathf.Lerp(fadingVelocity.x, 0, fadeSpeed * Time.fixedDeltaTime);
        }  
        
        if (Mathf.Abs(fadingVelocity.y) < 0.01)
        {
            fadingVelocity.y = 0;
        }
        else
        {
            fadingVelocity.y = Mathf.Lerp(fadingVelocity.y, 0, fadeSpeed * Time.fixedDeltaTime);
        } 
        if (Mathf.Abs(fadingVelocity.z) < 0.01)
        {
            fadingVelocity.z = 0;
        }
        else
        {
            fadingVelocity.z = Mathf.Lerp(fadingVelocity.z, 0, fadeSpeed * Time.fixedDeltaTime);
        } 
 
    }
    void CheckGround_FixedUpdate()
	{
		isGrounded = Physics.CheckSphere(transform.position, characterController.radius+radius_offset, groundLayer);
	}
    void OnDrawGizmos()
	{
        if (characterController != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, characterController.radius + radius_offset);
        }
    }
   
   void CalculateGravity_FixedUpdate()
    {
        if (isGrounded)
        {
            velocity.y = 0;
        }
        else
        {
            velocity.y -= gravityAcceleration * Time.fixedDeltaTime; // Apply gravity
        }
    }
    void FixedUpdate()
    {
       
        FadeVelocity_FixedUpdate();
        CheckGround_FixedUpdate();
        CalculateGravity_FixedUpdate();

        characterController.Move(velocity * Time.fixedDeltaTime);
        characterController.Move(fadingVelocity * Time.fixedDeltaTime);
    }    
}
