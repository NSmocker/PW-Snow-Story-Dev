using UnityEngine;

public class ObjectDestroyer : MonoBehaviour
{
    public void DestroyObject(GameObject object_to_destroy) 
    {
        Destroy(object_to_destroy);
    }
}
