using UnityEngine;
using UnityEngine.Playables;

public class SubtitleClip : PlayableAsset
{
    public string subtitleText;

    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        var playable = ScriptPlayable<SubtitleBihaviour>.Create(graph);
        var behaviour = playable.GetBehaviour();
        behaviour.subtitleText = subtitleText;
        return playable;
    }
}
