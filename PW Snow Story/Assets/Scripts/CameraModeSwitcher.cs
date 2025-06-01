using Unity.Cinemachine;
using UnityEngine;

public class CameraModeSwitcher : MonoBehaviour
{
    
    [Header("Camera Switch Mode")]
    public CinemachineCamera orbitalCamera;
  
    


    
    [Header("Camera Offset")]  
    public CinemachineOrbitalFollow cameraOrbit;
   
    public bool cameraOffseted;
 
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
			cameraOrbit.RadialAxis.Value = Mathf.Lerp(cameraOrbit.RadialAxis.Value, 2, Time.fixedDeltaTime * offsetDistanceSpeed);
			
		}
		else
		{
			if(cameraOrbit.RadialAxis.Value!=1)cameraOrbit.RadialAxis.Value = Mathf.Lerp(cameraOrbit.RadialAxis.Value, 1, Time.fixedDeltaTime * offsetDistanceSpeed);
			 
		}
		
    }
}
