using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAnimationsSystem : MonoBehaviour
{
	public Animator anim;
	
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    	if(Input.GetKeyDown(KeyCode.Alpha1))anim.SetTrigger("F");
    	if(Input.GetKeyDown(KeyCode.Alpha2))anim.SetTrigger("S");
	    if(Input.GetKeyDown(KeyCode.Alpha3))anim.SetTrigger("T");
    		
    }
}
