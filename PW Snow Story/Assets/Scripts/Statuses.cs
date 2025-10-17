using UnityEngine;

public class Statuses : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [Header("Inner Systems")]
    Animator animator;
 
    public float isAwareTimer;
    public float isAwareTimerCD = 5f;
    WeaponChanger weaponChanger;
    GamePlayModeManager gamePlayModeManager;

    [Header("Statuses")]
    public bool isAware;



    public int currentHealthPoints,maxHealthPoints;

    public int currentManaPoints,maxManaPoints;

    void Start()
    {
        animator = GetComponent<Animator>();
        weaponChanger = GetComponent<WeaponChanger>();
        gamePlayModeManager = GetComponent<GamePlayModeManager>();
        print(gamePlayModeManager.currentGamePlayMode);
    }

    

    // Update is called once per frame
    void Update()
    {
        if (gamePlayModeManager.currentGamePlayMode == GamePlayModes.City)
        {
            print("Returning from Statuses Update due to City Mode");
            isAware = false;
            return;
        }
        else
        {
            if (isAware)
            {
                isAwareTimer -= Time.deltaTime;
                if (isAwareTimer <= 0f) // якщо таймер вичерпано
                {
                    isAware = false;
                    isAwareTimer = 0f;
                    if (weaponChanger.currentWeaponState != WeaponState.Sheathed) //перевіряємо чи зброя не схована
                    weaponChanger.SheathWeapon(); //ховаємо зброю
                }
                else
                {
                     if (weaponChanger.currentWeaponState != WeaponState.Equipped) //перевіряємо чи зброя не в руках
                     weaponChanger.EquipWeapon(); //достаємо зброю
                }
            }
            animator.SetBool("isAware", isAware);    
        }
        
        
    }
}
