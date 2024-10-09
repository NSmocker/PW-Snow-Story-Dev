using UnityEngine;

public class CameraFollowCinema : MonoBehaviour
{
    [SerializeField]Transform followTarget;
    [SerializeField]float rotationSpeed = 5f;
    [SerializeField]float bottomClamp = -40f;
    [SerializeField]float topClamp = 70f;

    float cinemachineTargetPitch;
    float cinemachineTargetYaw;

    void LateUpdate()
    {
        CameraLogic();
    }
    void CameraLogic()
    {
        float mouseX = GetMouseInput("Mouse X");
        float mouseY = GetMouseInput("Mouse Y");
        cinemachineTargetPitch= UpdateRotation(cinemachineTargetPitch, mouseY, bottomClamp, topClamp, true);
        cinemachineTargetYaw = UpdateRotation(cinemachineTargetYaw, mouseX, float.MinValue, float.MaxValue, false);

        ApplyRotation(cinemachineTargetPitch, cinemachineTargetYaw);

    }
    void ApplyRotation(float pitch, float yaw)
    {
        followTarget.rotation = Quaternion.Euler(pitch, yaw, followTarget.eulerAngles.z);
    }
    float UpdateRotation(float currentRotation,float input, float min, float max, bool isXAsis)
    {
        currentRotation += isXAsis ? -input : input;
        return Mathf.Clamp(currentRotation, min, max);
    }
    float GetMouseInput(string axis)
    {
        return Input.GetAxis(axis)*rotationSpeed*Time.deltaTime;
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
