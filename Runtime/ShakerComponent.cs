using System;
using UnityEngine;

namespace Demegraunt.Framework {
    public sealed class ShakerComponent : MonoBehaviour {
        public enum ShakeComponentType {
            Transform,
            Rigidbody2D
        }

        [field: SerializeField] public ShakeComponentType ComponentType { get; set; } =  ShakeComponentType.Transform;
        [field: SerializeField] public Shaker.ShakeAlgorithm ShakeAlgorithm { get; set; } = Shaker.ShakeAlgorithm.AroundCenter;
        [field: SerializeField] public Shaker.ShakeDimension ShakeDimension { get; set; } = Shaker.ShakeDimension.TwoDimensional;

        [field: SerializeField] public float ShakeDistance { get; set; } = 0.02f;
        [field: SerializeField] public float DelayBetweenShakes { get; set; } = 0.1f;
        [field: SerializeField] public bool ShakeAlways { get; set; }

        private ShakerPlayback shakerPlayback;
        private Rigidbody2D rb2D;

        private const float SHAKE_ALWAYS_TIME = 10F;
        private float shakeAlwaysUpdateTime;

        private void Awake() {
            TryGetComponent(out rb2D);
        }

        private void Update() {
            if (shakerPlayback != null && ComponentType == ShakeComponentType.Transform) {
                shakerPlayback.Update(Time.deltaTime);
            }

            AutoUpdateCheck();
        }

        private void FixedUpdate() {
            if (shakerPlayback != null && ComponentType == ShakeComponentType.Rigidbody2D) {
                shakerPlayback.Update(Time.fixedDeltaTime);
            }
        }

        private void AutoUpdateCheck() {
            if (!ShakeAlways) {
                return;
            }
            
            if (shakeAlwaysUpdateTime > 0) {
                shakeAlwaysUpdateTime -= Time.deltaTime;
                return;
            }
            
            shakeAlwaysUpdateTime = SHAKE_ALWAYS_TIME;
            Shake(SHAKE_ALWAYS_TIME);
        }

        public void Shake(float time) {
            shakerPlayback?.Reset();
            var startPos = transform.position;
            shakerPlayback = new ShakerPlayback(() => startPos, shakePos => {
                switch (ComponentType) {
                    case ShakeComponentType.Transform:
                        transform.position = shakePos;
                        break;
                    case ShakeComponentType.Rigidbody2D:
                        rb2D.MovePosition(shakePos);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }, time, DelayBetweenShakes, ShakeDistance, ShakeAlgorithm, ShakeDimension);
        }
    }
}