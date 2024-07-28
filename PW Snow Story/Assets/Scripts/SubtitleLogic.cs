using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SubtitleLogic : MonoBehaviour
{
	public TextMeshProUGUI text_to_display;


	// Start is called before the first frame update
    void Start()
    {
        
    }

	public void ShowSubtitle(string text )
	{
		text_to_display.text = text;
		text_to_display.enabled  = true;
	}
	public void HideSubtitle(string text)
	{
		text_to_display.text = text;
		text_to_display.enabled  = false;
	}
	
    // Update is called once per frame
    void Update()
    {
	    
    }
}
