using UnityEngine;
using Unity.Cinemachine;

public class FreeSpaceCameraDistanceAdapter : MonoBehaviour
{


    [Header("Cinemachine")]
    public CinemachineCamera virtualCamera;
    public CinemachineOrbitalFollow orbitalFollow;
    public float defaultOrbitalRadius;
    public float targetOrbitalRadius;
    public float currentOrbitalRadius;

    public float jumpOrbitalRadius = 7f;
    public float awareOribtalRadius = 6f;


    [Header("Радіус налаштування")]
    public float lerpSpeed = 5f;
    public bool isCloseSpace;

    [Header("External Links")]
    public PlayerMovement playerMovement;
    public Statuses statuses;



    void Start()
    {
        // Отримуємо компоненти
        virtualCamera = GetComponent<CinemachineCamera>();
        if (virtualCamera != null)
        {
            orbitalFollow = GetComponent<CinemachineOrbitalFollow>();
            if (orbitalFollow != null)
            {
                // Зберігаємо початковий радіус орбіти
                defaultOrbitalRadius = orbitalFollow.Radius;
                currentOrbitalRadius = defaultOrbitalRadius;
                targetOrbitalRadius = defaultOrbitalRadius;
            }
            else
            {
                Debug.LogWarning("CinemachineOrbitalFollow component not found on the virtual camera!");
            }
        }
        else
        {
            Debug.LogWarning("CinemachineCamera component not found!");
        }
    }

    void Update()
    {
        SmoothChangeRadius();
        ChangeTargetRadiusByFactors();
    }
    public void ChangeTargetRadiusByFactors()
    {
        if (playerMovement.isGrounded == false)
        {
            targetOrbitalRadius = jumpOrbitalRadius;
        }
        else
        {
            if (statuses.isAware)
            {
                targetOrbitalRadius = awareOribtalRadius;
            }
            else
            targetOrbitalRadius = defaultOrbitalRadius;

        }



    }
    public void SmoothChangeRadius()
    {
        if (orbitalFollow != null && !Mathf.Approximately(currentOrbitalRadius, targetOrbitalRadius))
        {
            // Плавно змінюємо поточний радіус до цільового
            currentOrbitalRadius = Mathf.Lerp(currentOrbitalRadius, targetOrbitalRadius, Time.deltaTime * lerpSpeed);

            // Застосовуємо новий радіус
            orbitalFollow.Radius = currentOrbitalRadius;

            // Якщо різниця дуже мала, встановлюємо точне значення
            if (Mathf.Abs(currentOrbitalRadius - targetOrbitalRadius) < 0.01f)
            {
                currentOrbitalRadius = targetOrbitalRadius;
                orbitalFollow.Radius = targetOrbitalRadius;
            }
        }
    }
}
