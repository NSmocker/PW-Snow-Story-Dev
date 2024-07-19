using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TEMP_Subtitle_Logic : MonoBehaviour
{
	public float time_to_hide;
	public TextMeshProUGUI text_to_display;
	// Start is called before the first frame update
    void Start()
    {
        
    }

	public void ShowSubtitle(string text, float time)
	{
		text_to_display.text = text;
		time_to_hide = time;
	}
    // Update is called once per frame
    void Update()
    {
	    time_to_hide-=Time.deltaTime;
	    text_to_display.enabled = time_to_hide>0;
    }
}
