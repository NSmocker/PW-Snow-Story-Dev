using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionPointer : MonoBehaviour
{

    GameObject cameraObject;
    public bool isTargetLocked;
    public GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        cameraObject = Camera.main.gameObject;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(!isTargetLocked)
        {
            var cameraAngles = cameraObject.transform.eulerAngles;
            cameraAngles.x = 0;
            cameraAngles.z = 0;
            transform.eulerAngles = cameraAngles;
        }
        else
        {
            transform.LookAt(target.transform.position); 
            var pointerAngles = transform.eulerAngles;
            pointerAngles.x = 0;
            pointerAngles.z = 0;
            transform.eulerAngles = pointerAngles;   
        }
      
	    
    }
}
