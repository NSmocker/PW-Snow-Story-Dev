using UnityEngine;

public class CameraOffsetEasier : MonoBehaviour
{
    public CharacterMovement movement_system;
    public CinemachineCameraOffset cinemachineCameraOffset;
    

    // Мінімальне та максимальне значення змінної
    public float minValue = -5f;
    public float maxValue = 0f;

    // Швидкість зміни значення
    public float changeSpeed = 2f;

    // Поточне значення змінної
    private float currentValue = 0f;

    // Target value (значення, до якого потрібно плавно перейти)
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
            // Якщо Shift натиснутий, цільове значення - мінімальне
            targetValue = minValue;
        }
        else
        {
            // Якщо Shift відпущений, цільове значення - максимальне
            targetValue = maxValue;
        }

        // Плавно змінюємо значення змінної
        currentValue = Mathf.Lerp(currentValue, targetValue, changeSpeed * Time.deltaTime);


         cinemachineCameraOffset.m_Offset=  new Vector3( 0.2f,0, currentValue);
        
       
    }
}
