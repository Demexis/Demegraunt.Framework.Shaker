using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Demegraunt.Framework {
    [System.Serializable]
    public sealed class ShakeClip : PlayableAsset, ITimelineClipAsset {
        [field: SerializeField] public ShakeBehaviour ShakeBehaviour { get; set; } = new();
        
        public ClipCaps clipCaps => ClipCaps.Blending;

        public override Playable CreatePlayable(PlayableGraph graph, GameObject owner) {
            return ScriptPlayable<ShakeBehaviour>.Create(graph, ShakeBehaviour);
        }
    }
}