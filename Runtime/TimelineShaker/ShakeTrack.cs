using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;

namespace Demegraunt.Framework {
    [TrackColor(0.8f, 0.2f, 0.2f)]
    [TrackClipType(typeof(ShakeClip))]
    [TrackBindingType(typeof(Transform))]
    public sealed class ShakeTrack : TrackAsset {
        public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount) {
            return ScriptPlayable<ShakeMixerBehaviour>.Create(graph, inputCount);
        }
    }
}