using UnityEngine;

public class WeaponChanger : MonoBehaviour
{
    public GameObject swordInHand;
    public GameObject swordOnBack;
    public GameObject swordInHandBackGrip;



    public void EquipWeapon()
    {
        if (swordInHand != null) swordInHand.SetActive(true);
        if (swordOnBack != null) swordOnBack.SetActive(false);
        if (swordInHandBackGrip != null) swordInHandBackGrip.SetActive(false);
    }
    public void SheathWeapon()
    {
        if (swordInHand != null) swordInHand.SetActive(false);
        if (swordOnBack != null) swordOnBack.SetActive(true);
        if (swordInHandBackGrip != null) swordInHandBackGrip.SetActive(false);
    }

    public void BackGripWeapon()
    {
        if (swordInHand != null) swordInHand.SetActive(false);
        if (swordOnBack != null) swordOnBack.SetActive(false);
        if (swordInHandBackGrip != null) swordInHandBackGrip.SetActive(true);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SheathWeapon();
    }
}
