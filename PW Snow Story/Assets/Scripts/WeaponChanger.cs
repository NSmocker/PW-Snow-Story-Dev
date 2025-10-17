using UnityEngine;

public enum WeaponState
{
    Sheathed,
    Equipped,
    BackGrip
}

public class WeaponChanger : MonoBehaviour
{
    public GameObject swordInHand;
    public GameObject swordOnBack;
    public GameObject swordInHandBackGrip;
    GamePlayModeManager gamePlayModeManager;

    [Header("Поточний стан зброї")]
    public WeaponState currentWeaponState = WeaponState.Sheathed;

    public void EquipWeapon()
    {
        if (swordInHand != null) swordInHand.SetActive(true);
        if (swordOnBack != null) swordOnBack.SetActive(false);
        if (swordInHandBackGrip != null) swordInHandBackGrip.SetActive(false);
        currentWeaponState = WeaponState.Equipped;
    }
    public void SheathWeapon()
    {
        if (swordInHand != null) swordInHand.SetActive(false);
        if (swordOnBack != null) swordOnBack.SetActive(true);
        if (swordInHandBackGrip != null) swordInHandBackGrip.SetActive(false);
        currentWeaponState = WeaponState.Sheathed;
    }

    public void BackGripWeapon()
    {
        if (swordInHand != null) swordInHand.SetActive(false);
        if (swordOnBack != null) swordOnBack.SetActive(false);
        if (swordInHandBackGrip != null) swordInHandBackGrip.SetActive(true);
        currentWeaponState = WeaponState.BackGrip;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gamePlayModeManager = GetComponent<GamePlayModeManager>();
        SheathWeapon();
    }
    void Update()
    {
        if (gamePlayModeManager.currentGamePlayMode == GamePlayModes.City && currentWeaponState != WeaponState.Sheathed)
        {
            SheathWeapon();
        }
    }
}
