using UnityEngine;

public class CameraOffsetEasier : MonoBehaviour
{
    public CharacterMovement movement_system;
    public CinemachineCameraOffset cinemachineCameraOffset;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
 
        if(!movement_system.groundChecker.isGrounded ) cinemachineCameraOffset.m_Offset=  new Vector3( 0.2f,0,-5);
        else cinemachineCameraOffset.m_Offset=  new Vector3( 0.2f,0,0);
       
    }
}
