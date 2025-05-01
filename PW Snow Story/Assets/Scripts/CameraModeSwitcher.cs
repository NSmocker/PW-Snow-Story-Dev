using Unity.Cinemachine;
using UnityEngine;

public class CameraModeSwitcher : MonoBehaviour
{
    
    [Header("Camera Switch Mode")]
    public CinemachineCamera orbitalCamera;
  
    


    
    [Header("Camera Offset")]  
    public CinemachineCameraOffset cameraOffset;
    public bool cameraOffseted;
    float offsetDistanceValue=0;
	public float offsetDistanceSpeed=0.1f;
    

    

  

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void FixedUpdate()
    {
          if(cameraOffseted)
		{
			offsetDistanceValue = Mathf.Lerp(offsetDistanceValue, -2, Time.fixedDeltaTime * offsetDistanceSpeed);
			
		}
		else
		{
			if(offsetDistanceValue!=0)offsetDistanceValue = Mathf.Lerp(offsetDistanceValue, 0, Time.fixedDeltaTime * offsetDistanceSpeed);
			 
		}
		cameraOffset.Offset.z = offsetDistanceValue;
    }
}
