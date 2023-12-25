using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationSystem : MonoBehaviour
{
	public Animator anim_link;
	public bool is_moving;
 
 
	public void AnimateByInput(Vector2 move_info)
	{
		is_moving = move_info!= Vector2.zero;
	}
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
	    anim_link.SetBool("is_moving",is_moving);
    }
}
