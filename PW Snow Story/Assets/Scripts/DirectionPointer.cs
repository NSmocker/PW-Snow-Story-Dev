using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionPointer : MonoBehaviour
{

    public GameObject cameraObject;
    public bool isTargetLocked;
    public GameObject target;

    Transform parentTransform;

    // Start is called before the first frame update
    void Start()
    {   
        cameraObject = GameObject.FindGameObjectWithTag("MainCamera");
        parentTransform = transform.parent;
        //берем назву нашого батька
        name = transform.parent.name+ " " + name;
        //Відв'язуємось, щоб не залежити від оберту батьківських об'єктів
        transform.parent=null; 
         
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = cameraObject.transform.position;

       
        if(!isTargetLocked)
        {
            var cameraAngles = cameraObject.transform.eulerAngles;
            cameraAngles.x = 0;
            cameraAngles.z = 0;
            transform.eulerAngles = cameraAngles;
        }
        else
        { 
            if (target == null) return;
            transform.LookAt(target.transform.position); 
            var pointerAngles = transform.eulerAngles;
            pointerAngles.x = 0;
            pointerAngles.z = 0;
            transform.eulerAngles = pointerAngles;   
        }
      
	    
    }
}
