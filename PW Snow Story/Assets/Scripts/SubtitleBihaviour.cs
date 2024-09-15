using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;
public class SubtitleBihaviour : PlayableBehaviour
{
    public string subtitleText;

    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        var subtitle = playerData as TextMeshProUGUI;
        subtitle.text = subtitleText;
        subtitle.color = new Color(1f, 1f, 1f, info.weight);
    }
}
