using UnityEngine;

public class InventorySystem : MonoBehaviour
{

    public GameObject inventory;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetKeyDown(KeyCode.Tab))
		{
			inventory.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0f;
            
		} 
        if(Input.GetKeyUp(KeyCode.Tab))
		{
            Cursor.lockState = CursorLockMode.Locked;
			inventory.SetActive(false);
            Time.timeScale = 1f;
		}
    }

}
