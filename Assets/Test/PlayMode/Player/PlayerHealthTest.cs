using System.Collections;
using Behaviours.Player;
using Enums;
using NUnit.Framework;
using Test.Builders.Behaviours;
using Test.Builders.Behaviours.Player;
using UnityEngine;
using UnityEngine.TestTools;
using Utils;

namespace Test.PlayMode.Player
{
    public class PlayerHealthTest
    {
        private PlayerHealth _playerHealth;

        [UnitySetUp]
        public IEnumerator SetUp()
        {
            yield return new EnterPlayMode();
            LogAssert.ignoreFailingMessages = true;
            new GameObjectBuilder<AudioListener>().Build();
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
        public IEnumerator Update_WhenPlayerTakeDamaged_ShouldBeTakeDamage()
        {
            Setup();

            yield return null;

            _playerHealth.TakeDamage(1);

            LifeAsserts(99);
            Assert.True(_playerHealth.IsDamaged);
            Assert.True(_playerHealth.AudioSource.isPlaying);
            Assert.False(_playerHealth.IsDead);
            Assert.True(_playerHealth.IsAlive);


            yield return null;

            Assert.AreEqual(_playerHealth.FlashColour, _playerHealth.DamageImage.color);
        }

        [UnityTest]
        public IEnumerator Update_WhenPlayerTakeDamagedToDie_ShouldBeDie()
        {
            Setup();

            yield return null;

            _playerHealth.TakeDamage(_playerHealth.StartingHealth);

            LifeAsserts(0);
            Assert.AreEqual(_playerHealth.deathClip, _playerHealth.AudioSource.clip);
            Assert.True(_playerHealth.AudioSource.isPlaying);
            Assert.True(_playerHealth.IsDead);
            Assert.False(_playerHealth.IsAlive);
            Assert.False(_playerHealth.PlayerMovement.enabled);

            yield return null;

            Assert.AreEqual(_playerHealth.FlashColour, _playerHealth.DamageImage.color);
        }

        [UnityTest]
        public IEnumerator Update_WhenPlayerAddHealthWithFullLife_ShouldBeWithTheSameLife()
        {
            Setup();

            yield return null;

            _playerHealth.AddHealth(1);

            LifeAsserts(100);
        }

        [UnityTest]
        public IEnumerator Update_WhenPlayerAddHealth_ShouldBeAddLife()
        {
            Setup();

            yield return null;

            _playerHealth.TakeDamage(10);
            _playerHealth.AddHealth(5);

            LifeAsserts(95);
        }

        private void Setup()
        {
            var playerHealthBuilder = new PlayerHealthBuilder()
                .AddSlider()
                .AddDamageImage()
                .AddPlayerMovement()
                .AddAudioSource()
                .SetActive(false)
                .Build() as PlayerHealthBuilder;

            _playerHealth = playerHealthBuilder?.WithAudioClip().SetActive(true).Component;
        }

        private void LifeAsserts(int currentLife)
        {
            Assert.AreEqual(currentLife, _playerHealth.CurrentHealth);
            Assert.AreEqual(currentLife, _playerHealth.HealthSlider.value);
            Assert.AreEqual(100, _playerHealth.StartingHealth);
        }

        [UnityTearDown]
        public IEnumerator TearDown()
        {
            yield return new ExitPlayMode();
            ObjectUtils.DestroyAll<GameObject>();
        }
    }
}