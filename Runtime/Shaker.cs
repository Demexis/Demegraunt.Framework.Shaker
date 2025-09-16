using System;
using UnityEngine;

namespace Demegraunt.Framework {
    public sealed class Shaker {
        public enum ShakeAlgorithm {
            AroundCenter,
            Snake
        }
        
        public enum ShakeDimension {
            TwoDimensional,
            ThreeDimensional
        }

        private Vector3 shakerPos;

        private readonly Func<Vector3> getStartPosCallback;
        private readonly Action<Vector3> setPosCallback;

        public Shaker(Func<Vector3> getStartPosCallback, Action<Vector3> setPosCallback) {
            this.getStartPosCallback = getStartPosCallback;
            this.setPosCallback = setPosCallback;

            shakerPos = this.getStartPosCallback.Invoke();
        }

        public void Reset() {
            setPosCallback.Invoke(getStartPosCallback.Invoke());
        }

        public void Shake(float distance, ShakeAlgorithm shakeAlgorithm, ShakeDimension shakeDimension) {
            switch (shakeAlgorithm) {
                case ShakeAlgorithm.AroundCenter:
                    shakerPos = getStartPosCallback.Invoke() + GetShakingOffset(distance, shakeDimension);

                    break;
                case ShakeAlgorithm.Snake:
                    shakerPos += GetShakingOffset(distance, shakeDimension);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(shakeAlgorithm), shakeAlgorithm, null);
            }

            setPosCallback.Invoke(shakerPos);
        }

        private Vector3 GetShakingOffset(float distance, ShakeDimension shakeDimension) {
            return shakeDimension switch {
                ShakeDimension.TwoDimensional => GetShakingOffset2D(distance),
                ShakeDimension.ThreeDimensional => GetShakingOffset3D(distance),
                _ => throw new ArgumentOutOfRangeException(nameof(shakeDimension), shakeDimension, null)
            };
        }

        private Vector2 GetShakingOffset2D(float distance) {
            return UnityEngine.Random.insideUnitCircle * distance;
        }
        
        private Vector3 GetShakingOffset3D(float distance) {
            return UnityEngine.Random.insideUnitSphere * distance;
        }
    }
}