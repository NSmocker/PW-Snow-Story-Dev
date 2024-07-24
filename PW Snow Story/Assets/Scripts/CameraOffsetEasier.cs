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





    public float top_rig_start_height_value;
    public float bottom_rig_start_height_value;
    public float middle_rig_start_height_value;

    public float top_rig_max_height_value;
    public float bottom_rig_max_height_value;
    public float middle_rig_max_height_value;


    

    // ̳�������� �� ����������� �������� �����
   
    // �������� ���� ��������
    public float changeSpeed = 2f;

    // ������� �������� �����
    
    // Target value (��������, �� ����� ������� ������ �������)
    private float top_targetValue = 0f;
    private float middle_targetValue = 0f;
    private float bottom_targetValue = 0f;

    private float top_height_targetValue = 0f;
    private float middle_height_targetValue = 0f;
    private float bottom_height_targetValue = 0f;


    public float top_radius_offset = 0f;
    public float middle_radius_offset = 0f;
    public float bottom_radius_offset = 0f;

    public float top_height_offset = 0f;
    public float middle_height_offset = 0f;
    public float bottom_height_offset = 0f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        top_rig_start_value = freeLook.m_Orbits[0].m_Radius;
        middle_rig_start_value = freeLook.m_Orbits[1].m_Radius;
        bottom_rig_start_value = freeLook.m_Orbits[2].m_Radius;


        top_rig_start_height_value = freeLook.m_Orbits[0].m_Height;
        middle_rig_start_height_value = freeLook.m_Orbits[1].m_Height;
        bottom_rig_start_height_value  = freeLook.m_Orbits[2].m_Height;;



        /*
        top_rig_max_value = top_rig_start_value + top_radius_offset;
        middle_rig_max_value = middle_rig_start_value + middle_radius_offset;
        bottom_rig_max_value = bottom_rig_start_value + bottom_radius_offset;
         
        top_rig_max_height_value = top_rig_start_height_value + top_height_offset;
        middle_rig_max_height_value = middle_rig_start_height_value + middle_height_offset;
        bottom_rig_max_height_value = bottom_rig_start_height_value + bottom_height_offset;
        */




    }

    // Update is called once per frame
    void Update()
    {

        top_rig_max_value = top_rig_start_value + top_radius_offset;
        middle_rig_max_value = middle_rig_start_value + middle_radius_offset;
        bottom_rig_max_value = bottom_rig_start_value + bottom_radius_offset;
         
        top_rig_max_height_value = top_rig_start_height_value + top_height_offset;
        middle_rig_max_height_value = middle_rig_start_height_value + middle_height_offset;
        bottom_rig_max_height_value = bottom_rig_start_height_value + bottom_height_offset;
        




        if (!movement_system.groundChecker.isGrounded || Input.GetButton("Sprint"))
        {
            // ���� Shift ����������, ������� �������� - ���������
            top_targetValue = top_rig_max_value;
            middle_targetValue = middle_rig_max_value;
            bottom_targetValue = bottom_rig_max_value;


            top_height_targetValue = top_rig_max_height_value;
            middle_height_targetValue = middle_rig_max_height_value;
            bottom_height_targetValue = bottom_rig_max_height_value;
        }
        else
        {
            // ���� Shift ���������, ������� �������� - �����������
            top_targetValue = top_rig_start_value;
            middle_targetValue = middle_rig_start_value;
            bottom_targetValue = bottom_rig_start_value;


            top_height_targetValue = top_rig_start_height_value;
            middle_height_targetValue = middle_rig_start_height_value;
            bottom_height_targetValue = bottom_rig_start_height_value;
        }

        // ������ ������� �������� �����
        freeLook.m_Orbits[0].m_Radius = Mathf.Lerp(freeLook.m_Orbits[0].m_Radius, top_targetValue, changeSpeed * Time.deltaTime);
        freeLook.m_Orbits[1].m_Radius = Mathf.Lerp(freeLook.m_Orbits[1].m_Radius, middle_targetValue, changeSpeed * Time.deltaTime);
        freeLook.m_Orbits[2].m_Radius = Mathf.Lerp(freeLook.m_Orbits[2].m_Radius, bottom_targetValue, changeSpeed * Time.deltaTime);
         
        freeLook.m_Orbits[0].m_Height = Mathf.Lerp(freeLook.m_Orbits[0].m_Height, top_height_targetValue, changeSpeed * Time.deltaTime);
        freeLook.m_Orbits[1].m_Height = Mathf.Lerp(freeLook.m_Orbits[1].m_Height, middle_height_targetValue, changeSpeed * Time.deltaTime);
        freeLook.m_Orbits[2].m_Height = Mathf.Lerp(freeLook.m_Orbits[2].m_Height, bottom_height_targetValue, changeSpeed * Time.deltaTime);
       
    }
}
