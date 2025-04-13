using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionPointer : MonoBehaviour
{

    GameObject cameraObject;
    // Start is called before the first frame update
    void Start()
    {
         cameraObject = Camera.main.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        var cameraAngles = cameraObject.transform.eulerAngles;
        cameraAngles.x = 0;
        cameraAngles.z = 0;
        transform.eulerAngles = cameraAngles;
	    
    }
}
