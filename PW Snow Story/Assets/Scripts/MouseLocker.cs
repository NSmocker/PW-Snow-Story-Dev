using Cinemachine;
using UnityEngine;

public class MouseLocker : MonoBehaviour
{

    public CinemachineBrain cinemachineBrain;
    public bool lock_mouse;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        if(lock_mouse)Cursor.lockState = CursorLockMode.Locked;
        else Cursor.lockState = CursorLockMode.None;
    }

    // Update is called once per frame
    void Update()
    {
        if(Cursor.lockState==CursorLockMode.Locked)cinemachineBrain.enabled = true;
        else cinemachineBrain.enabled = false;
    }
}
