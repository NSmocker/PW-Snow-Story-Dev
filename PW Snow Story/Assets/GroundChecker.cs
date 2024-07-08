using UnityEngine;

public class GroundChecker : MonoBehaviour
{
  
    public CharacterController character_controller;
    public float radius_offset = 0.1f;
    public LayerMask groundLayer;
    public bool isGrounded ;
    void Update()
    {
         isGrounded = Physics.CheckSphere(transform.position, character_controller.radius+radius_offset, groundLayer);
       
    }
}
