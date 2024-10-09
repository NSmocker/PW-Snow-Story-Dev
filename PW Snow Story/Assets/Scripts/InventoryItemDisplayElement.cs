using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class InventoryItemDisplayElement : MonoBehaviour
{
    public Image itemIcon;
    public TextMeshProUGUI itemName;
    public TextMeshProUGUI itemCount;
    
    
    public void SetItemInfo(InventoryItem item) 
    {   
        itemIcon.sprite = item.itemSprite;
        itemName.text = item.itemName;
        itemCount.text = item.itemCount.ToString();
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
