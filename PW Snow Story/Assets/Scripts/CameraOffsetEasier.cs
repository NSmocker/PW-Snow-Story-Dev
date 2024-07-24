using Cinemachine;
using UnityEngine;

public class CameraOffsetEasier : MonoBehaviour
{
    public CharacterMovement movement_system;
    public CinemachineCameraOffset cinemachineCameraOffset;
    public CinemachineFreeLook freeLook;



    public float top_rig_start_value;
    public float bottom_rig_start_value;
    public float middle_rig_start_value;


    
    public float top_rig_max_value;
    public float bottom_rig_max_value;
    public float middle_rig_max_value;

    

    // ̳�������� �� ����������� �������� �����
    public float minValue = -5f;
    public float maxValue = 0f;

    // �������� ���� ��������
    public float changeSpeed = 2f;

    // ������� �������� �����
    private float currentValue = 0f;

    // Target value (��������, �� ����� ������� ������ �������)
    private float top_targetValue = 0f;
    private float middle_targetValue = 0f;
    private float bottom_targetValue = 0f;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        top_rig_start_value = freeLook.m_Orbits[0].m_Radius;
        middle_rig_start_value = freeLook.m_Orbits[1].m_Radius;
        bottom_rig_start_value = freeLook.m_Orbits[2].m_Radius;
        
        top_rig_max_value = top_rig_start_value + 5f;
        middle_rig_max_value = middle_rig_start_value + 5f;
        bottom_rig_max_value = bottom_rig_start_value + 5f;
         




    }

    // Update is called once per frame
    void Update()
    {
        if (!movement_system.groundChecker.isGrounded || Input.GetButton("Sprint"))
        {
            // ���� Shift ����������, ������� �������� - ���������
            top_targetValue = top_rig_max_value;
            middle_targetValue = middle_rig_max_value;
            bottom_targetValue = bottom_rig_max_value;
        }
        else
        {
            // ���� Shift ���������, ������� �������� - �����������
            top_targetValue = top_rig_start_value;
            middle_targetValue = middle_rig_start_value;
            bottom_targetValue = bottom_rig_start_value;
        }

        // ������ ������� �������� �����
        freeLook.m_Orbits[0].m_Radius = Mathf.Lerp(freeLook.m_Orbits[0].m_Radius, top_targetValue, changeSpeed * Time.deltaTime);
        freeLook.m_Orbits[1].m_Radius = Mathf.Lerp(freeLook.m_Orbits[1].m_Radius, middle_targetValue, changeSpeed * Time.deltaTime);
        freeLook.m_Orbits[2].m_Radius = Mathf.Lerp(freeLook.m_Orbits[2].m_Radius, bottom_targetValue, changeSpeed * Time.deltaTime);
         
        
       
    }
}
