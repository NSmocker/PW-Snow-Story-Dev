using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationSystem : MonoBehaviour
{
	public CharacterMovement movement_system;
	public Animator anim_link;
	public float movement_magnitude;
	public float sprint;
 
 
	public void AnimateByInput(Vector2 move_info)
	{
		movement_magnitude = move_info.magnitude;
		//КОСТИЛЬ!
		sprint = Input.GetAxis("Sprinting");
		//КОСТИЛЬ!
	}
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
	    anim_link.SetFloat("movement_magnitude",movement_magnitude);
	    anim_link.SetFloat("sprint",sprint);
	    
    }
}
