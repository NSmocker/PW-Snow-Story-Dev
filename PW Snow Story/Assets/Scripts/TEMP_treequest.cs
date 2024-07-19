using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TEMP_treequest : MonoBehaviour
{
	public int tree_count;
	public TextMeshProUGUI tree_count_display;
	
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
	    tree_count_display.text = "Хворост: "+ tree_count.ToString()+"/10";
    }
}
