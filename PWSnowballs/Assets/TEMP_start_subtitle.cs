using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEMP_start_subtitle : MonoBehaviour
{   
	public TEMP_Subtitle_Logic subs;
	public string start_text;
    // Start is called before the first frame update
    void Start()
    {
	    subs.ShowSubtitle(start_text,10f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
