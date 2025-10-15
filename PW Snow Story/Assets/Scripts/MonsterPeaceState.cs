using UnityEngine;

public class MonsterPeaceState : MonoBehaviour
{
    private enum SubState
    {
        Idle,
        Idle2,
        Movement
    }

    [Header("Стани та таймери")]
    private SubState currentSubState = SubState.Idle;
    private Animator animator;
    public float minStateTime = 3f;
    public float maxStateTime = 7f;
    private float stateTimer = 0f;

    [Header("Рух")]
    public float moveSpeed = 1f;
    public float maxRadius = 5f;
    public float minMoveDistance = 2f;
    public float maxMoveDistance = 4f;

    [Header("Фізика")]
    public float gravity = -9.81f;
    private Vector3 velocity;
    private bool isGrounded;
    public float groundedOffset = 0.14f;
    public float groundedRadius = 0.5f;
    public LayerMask groundLayers = -1;

    [Header("Компоненти")]
    private CharacterController characterController;
    private Vector3 spawnPoint;
    private Vector3 targetPosition;
    private bool isMoving = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        spawnPoint = transform.position;
        SetRandomStateTime();
    }

    void Update()
    {
        UpdateCurrentState();
        
        if (isMoving)
        {
            MoveToTarget();
        }
    }

    private void UpdateCurrentState()
    {
        stateTimer -= Time.deltaTime;

        if (stateTimer <= 0)
        {
            ChangeState();
        }

        switch (currentSubState)
        {
            case SubState.Idle:
                HandleIdleState();
                break;
            case SubState.Idle2:
                HandleIdle2State();
                break;
            case SubState.Movement:
                HandleMovementState();
                break;
        }
    }

    private void HandleIdleState()
    {
        animator.SetBool("IsIdle", true);
        animator.SetBool("IsIdle2", false);
        animator.SetBool("IsMoving", false);
    }

    private void HandleIdle2State()
    {
        animator.SetBool("IsIdle", false);
        animator.SetBool("IsIdle2", true);
        animator.SetBool("IsMoving", false);
    }

    private void HandleMovementState()
    {
        if (!isMoving)
        {
            // Знаходимо випадкову точку для руху в межах радіусу (тільки по X та Z осях)
            float randomAngle = Random.Range(0f, 360f);
            float randomDistance = Random.Range(minMoveDistance, maxMoveDistance);
            
            Vector3 randomDirection = new Vector3(
                Mathf.Cos(randomAngle * Mathf.Deg2Rad),
                0f,
                Mathf.Sin(randomAngle * Mathf.Deg2Rad)
            ) * randomDistance;
            
            Vector3 potentialTarget = spawnPoint + randomDirection;

            // Перевіряємо, чи нова точка не виходить за межі максимального радіусу
            if (Vector3.Distance(new Vector3(potentialTarget.x, spawnPoint.y, potentialTarget.z), 
                               new Vector3(spawnPoint.x, spawnPoint.y, spawnPoint.z)) <= maxRadius)
            {
                targetPosition = new Vector3(potentialTarget.x, transform.position.y, potentialTarget.z);
                isMoving = true;
                animator.SetBool("IsIdle", false);
                animator.SetBool("IsIdle2", false);
                animator.SetBool("IsMoving", true);
            }
        }
    }

    private void MoveToTarget()
    {
        // Перевіряємо чи персонаж на землі
        GroundCheck();

        if (Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z), 
                           new Vector3(targetPosition.x, 0, targetPosition.z)) > 0.1f)
        {
            // Отримуємо напрямок руху тільки по X та Z осях
            Vector3 moveDirection = (targetPosition - transform.position);
            moveDirection.y = 0; // Обнуляємо Y компонент для руху тільки по площині
            moveDirection.Normalize();

            // Повертаємо персонажа тільки навколо осі Y
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 360f * Time.deltaTime);

            // Застосовуємо гравітацію
            if (isGrounded && velocity.y < 0)
            {
                velocity.y = -2f; // Невелика постійна сила вниз для кращого утримання на землі
            }
            velocity.y += gravity * Time.deltaTime;

            // Рухаємо персонажа з урахуванням як горизонтального руху, так і гравітації
            Vector3 move = moveDirection * moveSpeed;
            move.y = velocity.y; // Додаємо вертикальну швидкість до руху
            characterController.Move(move * Time.deltaTime);
        }
        else
        {
            isMoving = false;
            ChangeState();
        }
    }

    private void GroundCheck()
    {
        // Визначаємо позицію для перевірки землі
        Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - groundedOffset, transform.position.z);
        
        // Перевіряємо чи є земля під персонажем
        isGrounded = Physics.CheckSphere(spherePosition, groundedRadius, groundLayers, QueryTriggerInteraction.Ignore);
    }

    private void ChangeState()
    {
        SubState newState;
        do
        {
            newState = (SubState)Random.Range(0, System.Enum.GetValues(typeof(SubState)).Length);
        } while (newState == currentSubState);

        currentSubState = newState;
        SetRandomStateTime();
    }

    private void SetRandomStateTime()
    {
        stateTimer = Random.Range(minStateTime, maxStateTime);
    }
}
