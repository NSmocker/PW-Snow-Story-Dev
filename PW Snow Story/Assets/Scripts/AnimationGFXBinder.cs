using UnityEngine;

public class AnimationGFXBinder : MonoBehaviour
{


    public AnimationSoundBinder animationSoundBinder;
    public GameObject jumpGFXPrefab;
    public void SpawnSecondJumpGFX()
    {
        animationSoundBinder.PlaySecondJumpSound();
        Instantiate(jumpGFXPrefab, transform.position, Quaternion.identity);
        // Тут має бути логіка для створення ефекту другого стрибка
        Debug.Log("Second Jump GFX Spawned");
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animationSoundBinder = GetComponent<AnimationSoundBinder>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
