using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryItemAddNotification : MonoBehaviour
{
    CanvasGroup canvasGroup;
    RectTransform rectTransform;

    Animator animator;
    public TextMeshProUGUI itemNameText;
    public Image itemIconImage;




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        rectTransform = GetComponent<RectTransform>();
        animator = GetComponent<Animator>();
    }

    public void ShowNotification(string itemName, Sprite itemIcon)
    {
        itemIconImage.sprite = itemIcon;
        itemNameText.text = "+1 "+itemName;
        animator.SetTrigger("ShowNotification");
        
    }
    // Update is called once per frame
    void Update()
    {
        if (canvasGroup != null && rectTransform != null)
        {
            // Calculate alpha based on the y-position of the RectTransform
            float maxHeight = 100; // Adjust this value based on your UI layout
            float normalizedHeight = Mathf.Clamp01(rectTransform.anchoredPosition.y / maxHeight);
            canvasGroup.alpha = 1f - normalizedHeight; // Higher position = lower alpha

        }
    }
}
