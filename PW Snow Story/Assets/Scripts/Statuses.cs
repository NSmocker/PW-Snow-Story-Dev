using UnityEngine;

public class Statuses : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [Header("Inner Systems")]
    Animator animator;
 
    public float isAwareTimer;
    public float isAwareTimerCD = 5f;
    WeaponChanger weaponChanger;

    [Header("Statuses")]
    public bool isAware;



    public int currentHealthPoints,maxHealthPoints;

    public int currentManaPoints,maxManaPoints;

    void Start()
    {
        animator = GetComponent<Animator>();
        weaponChanger = GetComponent<WeaponChanger>();
    }

    

    // Update is called once per frame
    void Update()
    {
        if (isAware)
        {
            isAwareTimer -= Time.deltaTime;
            if (isAwareTimer <= 0f)
            {
                isAware = false;
                isAwareTimer = 0f;
              if(weaponChanger != null)  weaponChanger.SheathWeapon();
            }
        }
        animator.SetBool("isAware", isAware);
    }
}
