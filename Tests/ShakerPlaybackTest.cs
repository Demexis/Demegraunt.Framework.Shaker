#if UNITY_EDITOR
using NUnit.Framework;
using System.Diagnostics.Contracts;
using UnityEngine;

namespace Demegraunt.Framework.Editor {
    [TestFixture]
    public sealed class ShakerPlaybackTest {
        [Pure]
        public static bool Approximately(float a, float b, float threshold = 0.001f) {
            return Mathf.Abs(a - b) < threshold;
        }

        [Pure]
        public static bool Approximately(Vector2 vecA, Vector2 vecB, float threshold = 0.001f) {
            return Approximately(vecA.x, vecB.x, threshold)
                && Approximately(vecA.y, vecB.y, threshold);
        }

        [Pure]
        public static bool Approximately(Vector3 vecA, Vector3 vecB, float threshold = 0.001f) {
            return Approximately(vecA.x, vecB.x, threshold)
                && Approximately(vecA.y, vecB.y, threshold)
                && Approximately(vecA.z, vecB.z, threshold);
        }
        
        [Test]
        public void ShakerSetsStartPosOnFinishTest() {
            const float shakeTime = 3f;
            const float dT = 0.05f;
            const float shakesDelay = 0.1f;
            const float distance = 1.5f;
            const float shakeTimeThreshold = 0.05f;
            
            var lastPos = Vector3.one;

            var centerShaker = new ShakerPlayback(() => Vector3.zero, vector3 => lastPos = vector3, shakeTime, shakesDelay, distance,
                Shaker.ShakeAlgorithm.AroundCenter, Shaker.ShakeDimension.TwoDimensional);

            for (var i = 0f; i < shakeTime + shakeTimeThreshold; i += dT) {
                centerShaker.Update(dT);
            }
            
            Assert.IsTrue(centerShaker.IsElapsed);
            Assert.AreEqual(Vector3.zero, lastPos);
            
            var snakeShaker = new ShakerPlayback(() => Vector3.zero, vector3 => lastPos = vector3, shakeTime, shakesDelay, distance,
                Shaker.ShakeAlgorithm.Snake, Shaker.ShakeDimension.TwoDimensional);

            for (var i = 0f; i < shakeTime + shakeTimeThreshold; i += dT) {
                snakeShaker.Update(dT);
            }
            
            Assert.IsTrue(centerShaker.IsElapsed);
            Assert.AreEqual(Vector3.zero, lastPos);
        }
        
        [Test]
        public void ShakerGetsChangedStartPosTest() {
            const float shakeTime = 3f;
            const float shakesDelay = 0f;
            const float distance = 0.0001f;

            var startPos = Vector3.one;
            var lastPos = Vector3.zero;
            
            // ReSharper disable once AccessToModifiedClosure
            var shaker = new ShakerPlayback(() => startPos, vector3 => lastPos = vector3, shakeTime, shakesDelay, distance,
                Shaker.ShakeAlgorithm.AroundCenter, Shaker.ShakeDimension.TwoDimensional);

            shaker.Update(1);
            Assert.IsTrue(Approximately(lastPos, startPos));

            startPos = Vector3.one * 10;
            
            shaker.Update(1);
            Assert.IsTrue(Approximately(lastPos, startPos));
        }
    }
}
#endif