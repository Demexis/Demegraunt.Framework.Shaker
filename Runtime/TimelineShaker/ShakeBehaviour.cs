using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Playables;

namespace Demegraunt.Framework {
    [System.Serializable]
    public sealed class ShakeBehaviour : PlayableBehaviour {
        [field: SerializeField] public float ShakesDelay { get; set; } = 0.1f;
        [field: SerializeField] public float Distance { get; set; } = 0.02f;
        [field: SerializeField] public Shaker.ShakeAlgorithm ShakeAlgorithm { get; set; } = Shaker.ShakeAlgorithm.AroundCenter;
        [field: SerializeField] public Shaker.ShakeDimension ShakeDimension { get; set; } = Shaker.ShakeDimension.TwoDimensional;

        private float? PreviousTime { get; set; }
        private float TotalDeltaTime { get; set; }

        private Shaker Shaker {
            get => shaker ??= new Shaker(GetStartPositionCallback, SetPositionCallback); 
            set => shaker = value;
        }
        [CanBeNull] private Shaker shaker;
        
        private Vector3 originalPosition;
        private Transform targetTransform;
        private bool isPlaying;

        private Vector3 GetStartPositionCallback() {
            return originalPosition;
        }

        private void SetPositionCallback(Vector3 position) {
            if (targetTransform == null) {
                Debug.LogWarning("TargetTransform is not set for track.");
                return;
            }
            targetTransform.localPosition = position;
        }

        public override void OnPlayableCreate(Playable playable) {
            isPlaying = false;
            PreviousTime = null;
            shaker = null;
            TotalDeltaTime = 0;
            shaker?.Reset();
        }

        public override void ProcessFrame(Playable playable, FrameData info, object playerData) {
            var trackBinding = playerData as Transform;

            if (trackBinding == null) {
                return;
            }

            if (!isPlaying) {
                originalPosition = trackBinding.localPosition;
                targetTransform = trackBinding;
                isPlaying = true;
            }

            var time = (float)playable.GetTime();

            if (PreviousTime == null) {
                PreviousTime = time;
            }
            
            var deltaTime = Mathf.Abs(time - PreviousTime.Value);
            TotalDeltaTime += deltaTime;
            
            PreviousTime = time;
            
            if (TotalDeltaTime < ShakesDelay) {
                return;
            }
            
            TotalDeltaTime -= ShakesDelay;

            var distance = Distance * info.weight;
            
            Shaker.Shake(distance, ShakeAlgorithm, ShakeDimension);
        }

        public override void OnPlayableDestroy(Playable playable) {
            if (targetTransform != null && isPlaying) {
                targetTransform.localPosition = originalPosition;
            }
            PreviousTime = null;
            shaker?.Reset();
            shaker = null;
            TotalDeltaTime = 0;
        }

        public override void OnBehaviourPause(Playable playable, FrameData info) {
            shaker?.Reset();
        }
    }
}