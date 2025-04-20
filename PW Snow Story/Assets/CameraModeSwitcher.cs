using Unity.Cinemachine;
using UnityEngine;

public class CameraModeSwitcher : MonoBehaviour
{
    public CinemachineCamera orbitalCamera;
    public CinemachineCamera targetGroupCamera;

    public void ToTargetGroupCamera()
    {
        orbitalCamera.Priority = 0;
        targetGroupCamera.Priority = 1;
        
    }
    public void ToOrbitalCamera()
    {
         orbitalCamera.Priority = 1;
        targetGroupCamera.Priority = 0;
    }
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
