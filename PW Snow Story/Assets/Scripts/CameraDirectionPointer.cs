using UnityEngine;

public class CameraDirectionPointer : MonoBehaviour
{

    public GameObject hardLockTarget;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (hardLockTarget != null)
        {
            transform.LookAt(hardLockTarget.transform.position);
            transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
        }
        else
        {
            var customElerAngles = Camera.main.transform.eulerAngles;
            customElerAngles.x = 0f;
            customElerAngles.z = 0f;
            transform.eulerAngles = customElerAngles;
        }
        
    }
}
