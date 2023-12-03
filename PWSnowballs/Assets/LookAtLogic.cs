using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtLogic : MonoBehaviour
{	
	public Transform target;
	public Transform character;
	public float distance_to_target;
	
	
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
	    if(Input.GetButton("Fire2"))
	    {
	    	distance_to_target = Vector3.Distance(character.position, target.position);
	    	var direction_vector = (target.position-character.position).normalized;
	    	
	    	transform.position = character.position+(direction_vector*(distance_to_target/2f));
	    }
	    else
	    {
	    	transform.localPosition = new Vector3(0,0,0);   	
	    }
    }
}
