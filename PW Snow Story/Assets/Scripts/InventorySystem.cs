using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class InventorySystem : MonoBehaviour
{
    public List<InventoryItem> items;

    /*******TEMP ZONE**********/
    public GameObject hotKeyItemAddDebug;
    /********************/
    
    public void AddItem(GameObject insert_item)
    {   
        var itemBihaviour = insert_item.GetComponent<InventoryItem>();
        if ( itemBihaviour ==null) 
        {
            print("В об'єкта нема поведінки InventoryItem. Об'єкт не додано!");
            return;
        }
        if(items.Any(item => item.itemID == itemBihaviour.itemID))
        {
            var existing_item = items.First(item => item.itemID == itemBihaviour.itemID);
            existing_item.itemCount += itemBihaviour.itemCount;
        }
        else
        {
            itemBihaviour.gameObject.transform.parent = transform;
            items.Add(itemBihaviour);  
        }
        
        print("Added item " + itemBihaviour.itemName + " to inventory");
         
    }
 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {

      

        /*********TEMP ZONE**********/
        if(Input.GetKeyDown(KeyCode.B))
        {
            AddItem(hotKeyItemAddDebug);
        }




    }

}
