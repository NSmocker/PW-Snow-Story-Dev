using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class InventorySystem : MonoBehaviour
{

    public GameObject inventory;
    GameObject inventoryItemsContainer;

     
    public GameObject inventoryItemDisplayPrefab;
    

    public List<InventoryItem> items;

    /*******TEMP ZONE**********/
    public InventoryItem wood;
    /********************/
    
    public void AddItem(InventoryItem insert_item)
    {   
        
        if(items.Any(item => item.itemID == insert_item.itemID))
        {
            var existing_item = items.First(item => item.itemID == insert_item.itemID);
            existing_item.itemCount += insert_item.itemCount;
        }
        else
        {
            var item = Instantiate(insert_item.gameObject,transform);
            items.Add(item.GetComponent<InventoryItem>());  
        }
        RefreshItemList();
        print("Added item " + insert_item.itemName + " to inventory");
         
    }
    public void RefreshItemList()
    {
        foreach (Transform child in inventoryItemsContainer.transform)
        {
            Destroy(child.gameObject);
        }
       
        
        foreach (var item in items)
        {
            var item_display = Instantiate(inventoryItemDisplayPrefab, inventoryItemsContainer.transform);
            item_display.GetComponent<InventoryItemDisplayElement>().SetItemInfo(item);
        }
        print("List is refreshed");
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inventoryItemsContainer = GameObject.Find("Inventory Item Container");
        inventory.SetActive(false);
        if(inventoryItemsContainer == null)Debug.LogError("Inventory Item Container not found");
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

        /*********TEMP ZONE**********/
        if(Input.GetKeyDown(KeyCode.B))
        {
            AddItem(wood);
        }




    }

}
