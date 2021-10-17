using System.Collections;
using Behaviours.Actions;
using Behaviours.Player;
using Enums;
using NUnit.Framework;
using Test.Builders.Behaviours;
using Test.Builders.Behaviours.Actions;
using Test.Builders.Behaviours.Player;
using UnityEngine;
using UnityEngine.TestTools;
using Utils;

namespace Test.PlayMode.Actions
{
    public class HealthActionTest
    {
        private PlayerHealth _playerHealth;

        [UnitySetUp]
        public IEnumerator SetUp()
        {
            yield return new EnterPlayMode();
            LogAssert.ignoreFailingMessages = true;
            new GameObjectBuilder<AudioListener>().Build();
            new GameObject { name = Tag.PowerUpPanel.ToString(), tag = Tag.PowerUpPanel.ToString() };
        }

        [UnityTest]
        public IEnumerator Execute_WhenAnActionIsTrigger_ShouldBeEnableHealthAction()
        {
            var healthActionBuilder = new HealthActionBuilder(nameof(HealthAction)).Build();

            var healthAction = ((HealthActionBuilder)healthActionBuilder)
                .Component;

            var playerHealthBuilder = new PlayerHealthBuilder()
                .AddSlider()
                .AddDamageImage()
                .AddPlayerMovement()
                .AddAudioSource()
                .SetActive(false)
                .Build() as PlayerHealthBuilder;

            _playerHealth = playerHealthBuilder?.WithAudioClip().WithTag(Tag.Player.ToString()).SetActive(true)
                .Component;


            yield return null;

            _playerHealth.TakeDamage(healthAction.Health);

            Assert.AreEqual(_playerHealth.StartingHealth - healthAction.Health, _playerHealth.CurrentHealth);

            healthAction.Execute();

            yield return null;
            Assert.AreEqual(_playerHealth.StartingHealth, _playerHealth.CurrentHealth);
        }

        [UnityTearDown]
        public IEnumerator TearDown()
        {
            yield return new ExitPlayMode();
            ObjectUtils.DestroyAll<GameObject>();
        }
    }
}