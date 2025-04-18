using UnityEngine;

public class SwordSystem : MonoBehaviour
{

    public bool isOn;
    public Transform spine, arm, armBackGrip;
    public Animator swordAnimator;
 

    //public KeyCode swordSwitchKey = KeyCode.V;
     
    AudioSource swordAudioSource;

    public AudioClip[] slashSound;
    float mute=2f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {   
        swordAudioSource = GetComponent<AudioSource>();
        SetOnSpine();
    }

   public  void SetOnSpine()
    {
        transform.parent = spine;
        transform.localPosition = Vector3.zero;
        transform.localEulerAngles = Vector3.zero;
    }
    public void SetOnArm()
    {
        transform.parent = arm;
        transform.localPosition = Vector3.zero;
        transform.localEulerAngles = Vector3.zero;
    }
    public void SetOnArmBackGrip()
    {
        transform.parent = armBackGrip;
        transform.localPosition = Vector3.zero;
        transform.localEulerAngles = Vector3.zero;
    }
    // Update is called once per frame
    void Update()
    {
        //if(Input.GetKeyDown(swordSwitchKey))isOn=!isOn;
        if(mute>0)mute-=Time.deltaTime;
        if(mute<0)swordAudioSource.enabled=true;
        
        if(isOn)
        {
            swordAnimator.SetBool("Show",true);
            
        }
        else
        {
            swordAnimator.SetBool("Show",false);
            
        }


    }
}
