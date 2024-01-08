using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepsPlayer : MonoBehaviour
{
	public AudioClip[] footsteps;
	public int footstep_id;
	public AudioSource sound_source;
	
	public void PlayFootstepSound()
	{
		if(footstep_id>1)footstep_id=0;
		sound_source.PlayOneShot(footsteps[footstep_id]);
	}
	
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
