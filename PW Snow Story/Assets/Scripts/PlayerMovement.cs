using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    //Animation Constants
    //Movement Magnitude f
    //Movement X f
    //Movement Y f
    //Hard Lock b

    public Animator animator;
    public CharacterController characterController;
    public Statuses statuses;
    public WeaponChanger weaponChanger;

    Vector2 playerMoveInput;
    float sprintValue;
    public KeyCode Sprintkey = KeyCode.LeftShift;
    public KeyCode hardLockKey = KeyCode.LeftShift;
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode attackKey = KeyCode.Mouse0;

    public LayerMask groundLayer;
    public bool isGrounded;
    private float groundCheckRadius; // Кешуємо радіус
    private float groundCheckOffset;
    [Header("Ground Check Settings")]
    public float groundCheckRadiusOffset = 0f; // Додатковий радіус для сфери перевірки землі
    [Tooltip("Додаткове зміщення по висоті для сфери перевірки землі")]
    public float checkSphereYOffset = 0f;
    public CameraDirectionPointer cameraDirectionPointer;
    public ClosestEnemyPointer closestEnemyPointer;
    


    public float customVerticalVelocity;
    public float groundedCustomVerticalVelocity=-5f;
    
    public float customHorizontalVelocity;
    public float jumpForce = 5f;
    public float flyForwardForce = 5f;
    
    public float velocityDelta = 0.1f; // Як швидко вертикальна швидкість змінюється до цілі
    public float graceTime;
    public float graceCD = 0.25f;
    public float attackSnapTime = 0.2f; // Час до снэпу атаки
    public float attackSnapTimeCD = 0.2f; // Час до снэпу атаки
    public Vector3 ClipVelocity; // Середня швидкість анімації за останній кадр
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        animator = GetComponent<Animator>();
        statuses = GetComponent<Statuses>();
        weaponChanger = GetComponent<WeaponChanger>();
        cameraDirectionPointer = transform.Find("Direction Pointer").GetComponent<CameraDirectionPointer>();

        // Кешуємо CharacterController і розраховуємо параметри для перевірки земли
        if (characterController == null) characterController = GetComponent<CharacterController>();
        if (characterController != null)
        {
            groundCheckRadius = characterController.radius;
            // Центр сфери буде нижче центра контролера
            groundCheckOffset = characterController.center.y - (characterController.height / 2f) + groundCheckRadius;
        }
    }

    public void UpdateRotateion(Vector2 input)
    {
        if (attackSnapTime > 0f &&  closestEnemyPointer.closestEnemy!=null)
        {
            transform.rotation = closestEnemyPointer.transform.rotation;
            
        }
        else
        {
            var magnitude = input.magnitude;
         
            if (magnitude > 0.01f)
            {
                // Отримати локальний напрямок вперед Direction Pointer
                Vector3 forward = cameraDirectionPointer.transform.forward;
                forward.y = 0; // ігнорувати вертикальну складову

                // Отримати напрямок руху гравця відносно інпуту
                Vector3 moveDir = new Vector3(input.x, 0, input.y);

                // Перетворити moveDir у світові координати через напрямок forward
                Quaternion cameraRotation = Quaternion.LookRotation(forward);
                Vector3 worldMoveDir = cameraRotation * moveDir;

                // Розвернути персонажа у напрямку руху
                if (worldMoveDir.sqrMagnitude > 0.001f)
                {
                transform.rotation = Quaternion.Slerp(transform.rotation,Quaternion.LookRotation(worldMoveDir),Time.deltaTime * 10f);
                }
            }
        }
        



    }
    

    
   
    void UpdateAnimatorVars()
    {
        animator.SetFloat("Movement Magnitude", playerMoveInput.magnitude);
        animator.SetFloat("Movement X", playerMoveInput.x);
        animator.SetFloat("Movement Y", playerMoveInput.y);
        animator.SetBool("Hard Lock", Input.GetKey(hardLockKey));
        animator.SetBool("Is Grounded", isGrounded);
        animator.SetBool("IsAttacking", attackSnapTime > 0f);
        animator.SetBool("Jump", Input.GetButton("Jump"));


        if (Input.GetButton("Attack"))
        {
            statuses.isAware = true;
            statuses.isAwareTimer = statuses.isAwareTimerCD;
            weaponChanger.EquipWeapon();
            attackSnapTime = attackSnapTimeCD;
            
        }
       
        /*if ( Input.GetButtonDown("Jump"))
        {
           
            if (isGrounded) JumpStart();
        }*/
        
    }

    void UpdateTimers()
    {
        if (graceTime > 0f)
        {
            graceTime -= Time.deltaTime;
        }
        if (attackSnapTime > 0f)
        {
            attackSnapTime -= Time.deltaTime;
        }

    }
    // Update is called once per frame
    void Update()
    {
        UpdateTimers();
        playerMoveInput.x = Input.GetAxis("Horizontal");
        playerMoveInput.y = Input.GetAxis("Vertical");
        UpdateRotateion(playerMoveInput);
        UpdateAnimatorVars();
        CalculateVerticalVelocity();

    }

    public void JumpStart()
    {

        graceTime = graceCD ;
        animator.applyRootMotion = false;
        customVerticalVelocity = jumpForce; // Початкова швидкість стрибка
        
    }


    public void Grounded()
    {
        animator.applyRootMotion = true;
        customVerticalVelocity = -1f;
    }
    void CalculateVerticalVelocity()
    {
        if (!animator.applyRootMotion)
        {
            customVerticalVelocity -= velocityDelta * Time.deltaTime;
            customHorizontalVelocity = Mathf.Lerp(0, flyForwardForce, playerMoveInput.magnitude);
            characterController.Move(new Vector3(0, customVerticalVelocity, 0) * Time.deltaTime);
            characterController.Move(characterController.transform.forward * customHorizontalVelocity * Time.deltaTime);

        }
        else
        {
            if (isGrounded) customVerticalVelocity = groundedCustomVerticalVelocity;
            else
            {
                customVerticalVelocity -= velocityDelta * Time.deltaTime;
                customHorizontalVelocity = Mathf.Lerp(0, flyForwardForce, playerMoveInput.magnitude);
                characterController.Move(new Vector3(0, customVerticalVelocity, 0) * Time.deltaTime);
                characterController.Move(characterController.transform.forward * customHorizontalVelocity * Time.deltaTime);
            }
        }
    }


    void FixedUpdate()
    {
        CheckGrounded();
  
    }

    void CheckGrounded()
    {
        if (characterController == null)
        {
            // Якщо контролер відсутній — намагаємося підхопити і вийти
            characterController = GetComponent<CharacterController>();
            if (characterController == null) return;
            groundCheckRadius = characterController.radius;
            groundCheckOffset = characterController.center.y - (characterController.height / 2f) + groundCheckRadius;
        }

        // Світовий центр контролера (включаючи offset)
        Vector3 worldCenter = transform.position + characterController.center;
        // Y беремо з мінімальної точки bounds + додатковий офсет, X/Z з центру контролера
        Vector3 sphereCenter = new Vector3(
            worldCenter.x, 
            characterController.bounds.min.y + checkSphereYOffset, 
            worldCenter.z);

        if(graceTime > 0f)
        {
            isGrounded = false;
        }
        else
        {
            // Використовуємо базовий радіус + додатковий офсет
            float totalRadius = groundCheckRadius + groundCheckRadiusOffset;
            isGrounded = Physics.CheckSphere(sphereCenter, totalRadius, groundLayer);
        }

        #if UNITY_EDITOR
            // Для візуалізації чексфери в редакторі
            Debug.DrawLine(sphereCenter, sphereCenter + Vector3.up * 0.1f, isGrounded ? Color.green : Color.red);
        #endif
    }

    #if UNITY_EDITOR
    void OnDrawGizmosSelected()
    {
        // Переконуємося, що є CharacterController
        if (characterController == null) characterController = GetComponent<CharacterController>();
        if (characterController == null) return;

        Vector3 worldCenter = transform.position + characterController.center;
        Vector3 sphereCenter = new Vector3(
            worldCenter.x, 
            characterController.bounds.min.y + checkSphereYOffset, 
            worldCenter.z);
        Gizmos.color = isGrounded ? Color.green : Color.red;
        // Використовуємо базовий радіус + додатковий офсет для відображення в редакторі
        float totalRadius = (characterController != null) ? characterController.radius + groundCheckRadiusOffset : 0.5f;
        Gizmos.DrawWireSphere(sphereCenter, totalRadius);
    }
    #endif
}
