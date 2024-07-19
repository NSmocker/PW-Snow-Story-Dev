using UnityEngine;

public class CameraOffsetEasier : MonoBehaviour
{
    public CharacterMovement movement_system;
    public CinemachineCameraOffset cinemachineCameraOffset;
    

    // ̳������� �� ����������� �������� �����
    public float minValue = -5f;
    public float maxValue = 0f;

    // �������� ���� ��������
    public float changeSpeed = 2f;

    // ������� �������� �����
    private float currentValue = 0f;

    // Target value (��������, �� ����� ������� ������ �������)
    private float targetValue = 0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!movement_system.groundChecker.isGrounded || Input.GetButton("Sprint"))
        {
            // ���� Shift ����������, ������� �������� - ��������
            targetValue = minValue;
        }
        else
        {
            // ���� Shift ���������, ������� �������� - �����������
            targetValue = maxValue;
        }

        // ������ ������� �������� �����
        currentValue = Mathf.Lerp(currentValue, targetValue, changeSpeed * Time.deltaTime);


         cinemachineCameraOffset.m_Offset=  new Vector3( 0.2f,0, currentValue);
        
       
    }
}
