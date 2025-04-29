using UnityEngine;

public class InventoryDisplaySystem : MonoBehaviour
{
    public GameObject displayItemPrefab;
    public InventorySystem inventorySystem;//System inside character
    public Transform itemsContainer; //Part of window to input display elements

    public GameObject mainInventoryWindow; //Parent window, to enable or disable vision of inventory


    void RefreshDisplay()
    {
        foreach (Transform child in itemsContainer)
        {
            Destroy(child.gameObject);
        }
         foreach (var item in inventorySystem.items)
        {
            var itemDisplay = Instantiate(displayItemPrefab, transform);
            itemDisplay.GetComponent<InventoryItemDisplayElement>().SetItemInfo(item);
            itemDisplay.transform.parent = itemsContainer;
        }
    }



    void UserInputBindUpdate()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            mainInventoryWindow.SetActive(true);
            RefreshDisplay();
            Time.timeScale = 0;
        }   
        if(Input.GetKeyUp(KeyCode.Tab))
        {
            Cursor.lockState = CursorLockMode.Locked;
            mainInventoryWindow.SetActive(false);
            Time.timeScale = 1;
        }   




    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UserInputBindUpdate();

     
       
    }
}
