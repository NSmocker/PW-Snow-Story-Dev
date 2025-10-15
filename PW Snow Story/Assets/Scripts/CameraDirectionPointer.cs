using UnityEngine;

public class CameraDirectionPointer : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var customElerAngles = Camera.main.transform.eulerAngles;
        customElerAngles.x = 0f;
        customElerAngles.z = 0f;
        transform.eulerAngles = customElerAngles;
    }
}
