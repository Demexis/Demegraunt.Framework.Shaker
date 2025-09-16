using System;
using UnityEngine;

namespace Demegraunt.Framework {
    public sealed class ShakerPlayback {
        public event Action Elapsed = delegate { };
        
        public bool IsElapsed { get; private set; }

        public float ShakesDelay { get; set; }
        public float Distance { get; set; }
        public Shaker.ShakeAlgorithm ShakeAlgorithm { get; set; }
        public Shaker.ShakeDimension ShakeDimension { get; set; }
        
        private float timer;
        private float delayTimer;
        
        private readonly Shaker shaker;

        public ShakerPlayback(Func<Vector3> getStartPosCallback, Action<Vector3> setPosCallback, float time, float shakesDelay,
            float distance, Shaker.ShakeAlgorithm shakeAlgorithm, Shaker.ShakeDimension shakeDimension) {
            timer = time;

            ShakesDelay = shakesDelay;
            Distance = distance;
            ShakeAlgorithm = shakeAlgorithm;
            ShakeDimension = shakeDimension;
            
            shaker = new Shaker(getStartPosCallback, setPosCallback);
        }

        public void Update(float deltaTime) {
            if (timer < 0) {
                if (!IsElapsed) {
                    Reset();
                    IsElapsed = true;
                    Elapsed.Invoke();
                }
                return;
            }

            timer -= deltaTime;

            if (delayTimer > 0) {
                delayTimer -= deltaTime;
                return;
            }

            delayTimer = ShakesDelay;
            
            shaker.Shake(Distance, ShakeAlgorithm, ShakeDimension);
        }
        
        public void Reset() {
            shaker.Reset();
        }
    }
}