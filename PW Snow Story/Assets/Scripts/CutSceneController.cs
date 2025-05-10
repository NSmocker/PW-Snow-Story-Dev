using UnityEngine;
using UnityEngine.Playables;

public class CutSceneController : MonoBehaviour
{
    public PlayableDirector []talking_cutscenes;




    public void PlayForced(PlayableDirector playableDirector)
    {
        StopAllCutScenes();
        playableDirector.Play();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    void StopAllCutScenes()
    {
        foreach (var cutscene in talking_cutscenes)
        {
            cutscene.Stop();
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.T))
        {   
            StopAllCutScenes();
            var random_talk = Random.Range(0, talking_cutscenes.Length);
            talking_cutscenes[random_talk].Play();
        }
    }
}
