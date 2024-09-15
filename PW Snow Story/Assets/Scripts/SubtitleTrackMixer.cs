using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class SubtitleTrackMixer : PlayableBehaviour
{
    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        var text = playerData as TextMesh;
        string current_text = "";
        float current_alpha = 0f;

       if(!text){return;}
       int InputCount= playable.GetInputCount();
       for(int i=0;i<InputCount;i++)
       {
        float inputWeight = playable.GetInputWeight(i);
        if(inputWeight > 0f)
        {
            ScriptPlayable<SubtitleBihaviour> inputPlayable = (ScriptPlayable<SubtitleBihaviour>)playable.GetInput(i);
            SubtitleBihaviour input = inputPlayable.GetBehaviour();
            current_text = input.subtitleText;
            current_alpha = inputWeight;
        }
       }
        text.text = current_text;
        text.color = new Color(1f, 1f, 1f, current_alpha);
    } 
}
