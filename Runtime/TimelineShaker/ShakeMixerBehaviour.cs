using UnityEngine;
using UnityEngine.Playables;

namespace Demegraunt.Framework {
    public sealed class ShakeMixerBehaviour : PlayableBehaviour {
        public override void ProcessFrame(Playable playable, FrameData info, object playerData) {
            var trackBinding = playerData as Transform;

            if (trackBinding == null) {
                return;
            }

            // int inputCount = playable.GetInputCount();

            // for (int i = 0; i < inputCount; i++) {
            //     float inputWeight = playable.GetInputWeight(i);
            //     ScriptPlayable<ShakeBehaviour> inputPlayable = (ScriptPlayable<ShakeBehaviour>)playable.GetInput(i);
            //     ShakeBehaviour input = inputPlayable.GetBehaviour();
            //
            //     // // Обработка весов микширования
            //     // input.intensity *= inputWeight;
            //     // input.frequency *= inputWeight;
            // }
        }
    }
}