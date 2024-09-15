using UnityEngine;
using UnityEngine.Playables;

public class CutSceneController : MonoBehaviour
{
    public PlayableDirector []talking_cutscenes;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.T))
        {
            var random_talk = Random.Range(0, talking_cutscenes.Length);
            talking_cutscenes[random_talk].Play();
        }
    }
}
