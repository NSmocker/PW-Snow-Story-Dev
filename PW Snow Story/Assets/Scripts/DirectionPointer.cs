using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionPointer : MonoBehaviour
{
	public Transform point;
    public GameObject camera;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!point)
        {
            var cameraObject = Camera.main;
            if(!cameraObject)
            {
                print("Main camera not founded, return");
                return;
            } 
            var angles = Camera.main.transform.eulerAngles;
	        angles.x=0;
	        angles.z=0;
	        transform.eulerAngles = angles;
        }
        else
        {
            transform.LookAt( point.position);
        }
	    
    }
}
