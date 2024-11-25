using UnityEngine;

public class InventoryDisplaySystem : MonoBehaviour
{
    public GameObject displayItemPrefab;
    public InventorySystem inventorySystem;
    public Transform itemsContainer;


    void OnEnable()
    {
        foreach (var item in inventorySystem.items)
        {
            var itemDisplay = Instantiate(displayItemPrefab, transform);
            itemDisplay.GetComponent<InventoryItemDisplayElement>().SetItemInfo(item);
            itemDisplay.transform.parent = itemsContainer;
        }
    }
    void OnDisable()
    {
        foreach (Transform child in itemsContainer)
        {
            Destroy(child.gameObject);
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
