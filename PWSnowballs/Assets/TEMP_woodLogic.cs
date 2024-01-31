using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEMP_woodLogic : MonoBehaviour
{
	public AudioClip take_effect;
	public AudioClip phrase;
	public string prase_text;
	
	void OnTriggerEnter(Collider col)
	{
		if(col.tag =="Player")
		{
			GameObject.Find("Canvas").GetComponent<TEMP_treequest>().tree_count++;
			col.gameObject.GetComponent<AudioSource>().PlayOneShot(take_effect);
			col.gameObject.GetComponent<AudioSource>().PlayOneShot(phrase);
			GameObject.Find("Subtitles").GetComponent<TEMP_Subtitle_Logic>().ShowSubtitle(prase_text,phrase.length);
			Destroy(gameObject);
			
		}
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
