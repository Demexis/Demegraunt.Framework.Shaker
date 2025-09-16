#if UNITY_EDITOR
using Demegraunt.Framework.Utils;
using NUnit.Framework;
using UnityEngine;

namespace Demegraunt.Framework.Editor {
    [TestFixture]
    public sealed class ShakerPlaybackTest {
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
            Assert.IsTrue(MathUtils.Approximately(lastPos, startPos));

            startPos = Vector3.one * 10;
            
            shaker.Update(1);
            Assert.IsTrue(MathUtils.Approximately(lastPos, startPos));
        }
    }
}
#endif