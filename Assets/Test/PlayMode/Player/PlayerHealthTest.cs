using System.Collections;
using Behaviours.Managers;
using Enums;
using NUnit.Framework;
using Test.Builders.Behaviours;
using Test.Builders.Behaviours.Player;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;
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
            new GameObjectBuilder<Slider>().Build().WithTag(Tag.Health.ToString());
            new GameObject { tag = Tag.DamageImage.ToString() };

            new PlayerHealthBuilder().Build();

            LogAssert.Expect(LogType.Exception, "ArgumentException: No damage image found");

            yield return null;
        }

        [UnityTest]
        public IEnumerator Awake_WhenNoPlayerMovementIsFound_ThrowsArgumentException()
        {
            new GameObjectBuilder<Slider>().Build().WithTag(Tag.Health.ToString());
            new GameObjectBuilder<Image>().Build().WithTag(Tag.DamageImage.ToString());

            new PlayerHealthBuilder().Build();

            LogAssert.Expect(LogType.Exception, "ArgumentException: No player movement found");

            yield return null;
        }

        [UnityTest]
        public IEnumerator Awake_WhenNoAudioSourceFound_ThrowsArgumentException()
        {
            new GameObjectBuilder<Slider>().Build().WithTag(Tag.Health.ToString());
            new GameObjectBuilder<Image>().Build().WithTag(Tag.DamageImage.ToString());
            new GameObject().AddComponent<VirtualInputInputManager>();
            var gameObject = new PlayerMovementBuilder().AddMainCamera().AddCharacterController()
                .AddAnimator().Build().GameObject;
            new PlayerHealthBuilder(gameObject).Build();

            LogAssert.Expect(LogType.Exception, "ArgumentException: No AudioSource found");

            yield return null;
        }

        [UnityTest]
        public IEnumerator Awake_WhenNoDeathAudioClipFound_ThrowsArgumentException()
        {
            new GameObjectBuilder<Slider>().Build().WithTag(Tag.Health.ToString());
            new GameObjectBuilder<Image>().Build().WithTag(Tag.DamageImage.ToString());
            new GameObject().AddComponent<VirtualInputInputManager>();
            var gameObject = new PlayerMovementBuilder().AddMainCamera().AddCharacterController()
                .AddAnimator().Build().GameObject;
            gameObject = new GameObjectBuilder<AudioSource>(gameObject).Build().GameObject;
            new PlayerHealthBuilder(gameObject).Build();

            LogAssert.Expect(LogType.Exception, "ArgumentException: No death audio clip found");

            yield return null;
        }

        [UnityTest]
        public IEnumerator Awake_WhenPlayerTakeDamaged_ThrowsArgumentException()
        {
            new GameObjectBuilder<Slider>().Build().WithTag(Tag.Health.ToString());
            new GameObjectBuilder<Image>().Build().WithTag(Tag.DamageImage.ToString());
            new GameObject().AddComponent<VirtualInputInputManager>();
            var gameObject = new PlayerMovementBuilder().AddMainCamera().AddCharacterController()
                .AddAnimator().Build().GameObject;
            gameObject = new GameObjectBuilder<AudioSource>(gameObject).Build().GameObject;
            gameObject.SetActive(false);
            var playerHealth = new PlayerHealthBuilder(gameObject).Build().Component;
            playerHealth.deathClip = AudioClip.Create("Death Clip", 1, 1, 1000, true, null, null);
            gameObject.SetActive(true);

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