using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionPointer : MonoBehaviour
{
	
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
	    var angles = Camera.main.transform.eulerAngles;
	    angles.x=0;
	    angles.z=0;
	    transform.eulerAngles = angles;
    }
}
