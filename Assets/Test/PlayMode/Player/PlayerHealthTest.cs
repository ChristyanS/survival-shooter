using System.Collections;
using Enums;
using NUnit.Framework;
using Test.Builders.Behaviours.Player;
using UnityEngine;
using UnityEngine.TestTools;
using Utils;

namespace Test.PlayMode.Player
{
    public class PlayerHealthTest
    {
        [UnitySetUp]
        public IEnumerator SetUp()
        {
            yield return new EnterPlayMode();
            LogAssert.ignoreFailingMessages = true;
        }


        [UnityTest]
        public IEnumerator Awake_WhenNoSliderIsFound_ThrowsArgumentException()
        {
            new GameObject { tag = Tag.Health.ToString() };
            new GameObject { tag = Tag.DamageImage.ToString() };

            new PlayerHealthBuilder().Build();

            LogAssert.Expect(LogType.Exception, "ArgumentException: No Slider is found");

            yield return null;
        }

        [UnityTest]
        public IEnumerator Awake_WhenNoDamageImageFound_ThrowsArgumentException()
        {
            new GameObject { tag = Tag.DamageImage.ToString() };

            new PlayerHealthBuilder().AddSlider().Build();

            LogAssert.Expect(LogType.Exception, "ArgumentException: No damage image found");

            yield return null;
        }

        [UnityTest]
        public IEnumerator Awake_WhenNoPlayerMovementIsFound_ThrowsArgumentException()
        {
            new PlayerHealthBuilder().AddSlider().AddDamageImage().Build();

            LogAssert.Expect(LogType.Exception, "ArgumentException: No player movement found");

            yield return null;
        }

        [UnityTest]
        public IEnumerator Awake_WhenNoAudioSourceFound_ThrowsArgumentException()
        {
            new PlayerHealthBuilder().AddSlider().AddDamageImage().AddPlayerMovement().Build();

            LogAssert.Expect(LogType.Exception, "ArgumentException: No AudioSource found");

            yield return null;
        }

        [UnityTest]
        public IEnumerator Awake_WhenNoDeathAudioClipFound_ThrowsArgumentException()
        {
            new PlayerHealthBuilder().AddSlider().AddDamageImage().AddPlayerMovement().AddAudioSource().Build();

            LogAssert.Expect(LogType.Exception, "ArgumentException: No death audio clip found");

            yield return null;
        }

        [UnityTest]
        public IEnumerator Awake_WhenPlayerTakeDamaged_ThrowsArgumentException()
        {
            var playerHealth =
                (new PlayerHealthBuilder().AddSlider().AddDamageImage().AddPlayerMovement().AddAudioSource()
                        .SetActive(false).Build() as
                    PlayerHealthBuilder)?.WithAudioClip().SetActive(true).Component;


            yield return null;
            playerHealth.TakeDamage(1);
            Assert.AreEqual(99, playerHealth.CurrentHealth);
            Assert.AreEqual(100, playerHealth.StartingHealth);
            Assert.True(playerHealth.IsDamaged);
            // Assert.True(playerHealth.AudioSource.isPlaying);
            yield return null;
            Assert.AreEqual(playerHealth.FlashColour, playerHealth.DamageImage.color);
        }

        [UnityTearDown]
        public IEnumerator TearDown()
        {
            yield return new ExitPlayMode();
            ObjectUtils.DestroyAll<GameObject>();
        }
    }
}