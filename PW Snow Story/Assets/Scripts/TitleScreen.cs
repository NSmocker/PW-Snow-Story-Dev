using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class TitleScreen : MonoBehaviour
{
    public PlayableDirector playableDirector;
 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    public void ActivateTip()
    {

    }
    public void PauseDirector()
    {
        playableDirector.playableGraph.GetRootPlayable(0).SetSpeed(0);
    }
    //TODO 
    // Going dark
    // Load Gameplay scene




    // Update is called once per frame
    void Update()
    {
        if(Input.anyKeyDown)
       // if(Input.GetKeyDown(KeyCode.Space))
        {
            playableDirector.playableGraph.GetRootPlayable(0).SetSpeed(1);
            //playableDirector.Play();
            
        }        
    }
}
