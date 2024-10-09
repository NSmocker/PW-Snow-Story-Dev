using UnityEngine;

public class SwordSystem : MonoBehaviour
{

    public bool isOn;
    public Transform currentParent;
    public Transform spine, arm;
    public Animator swordAnimator;

    public KeyCode swordSwitchKey = KeyCode.V;
     
    AudioSource swordAudioSource;

    public AudioClip[] slashSound;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {   swordAudioSource = GetComponent<AudioSource>();
        transform.parent = spine;
        currentParent = transform.parent;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(swordSwitchKey))isOn=!isOn;
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
