using UnityEngine;

public class WaterfallMesh : MonoBehaviour
{
    [SerializeField] private Material material; // Матеріал, у якому буде прокручуватись оффсет
    [SerializeField] private float scrollSpeed = 0.1f; // Швидкість прокрутки

    private float offsetY = 0f;

    // Update is called once per frame
    void Update()
    {
        if (material != null)
        {
            offsetY += scrollSpeed * Time.deltaTime;
            material.mainTextureOffset = new Vector2(material.mainTextureOffset.x, offsetY);
        }
    }
}
