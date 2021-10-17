using System.Collections;
using Behaviours.Actions;
using Behaviours.Managers;
using Enums;
using NUnit.Framework;
using Test.Builders.Behaviours;
using Test.Builders.Behaviours.Actions;
using UnityEngine;
using UnityEngine.TestTools;
using Utils;

namespace Test.PlayMode.Actions
{
    public class InstaKillActionTest
    {
        [UnitySetUp]
        public IEnumerator SetUp()
        {
            yield return new EnterPlayMode();
            LogAssert.ignoreFailingMessages = true;
            new GameObject { name = Tag.PowerUpPanel.ToString(), tag = Tag.PowerUpPanel.ToString() };
            new GameObjectBuilder<GameManager>(nameof(GameManager)).Build();
        }

        [UnityTest]
        public IEnumerator Execute_WhenAnActionIsTrigger_ShouldBeEnableInstaKillAction()
        {
            var installKillActionBuilder = new InstaKillActionBuilder(nameof(InstaKillAction)).Build();

            var instaKillAction = ((InstaKillActionBuilder)installKillActionBuilder)
                .WithPowerUpImage()
                .WithTime(float.MinValue)
                .Component;

            instaKillAction.Execute();

            Assert.AreEqual(1, instaKillAction.PowerUpPanel.transform.childCount);
            Assert.AreEqual("PowerUpImage(Clone)", instaKillAction.PowerUpPanel.transform.GetChild(0).name);
            Assert.IsTrue(GameManager.Instance.InstaKillEnable);

            yield return new WaitForSeconds(instaKillAction.Time);
            Assert.IsFalse(GameManager.Instance.InstaKillEnable);

            yield return null;

            Assert.True(instaKillAction == null);
        }

        [UnityTearDown]
        public IEnumerator TearDown()
        {
            yield return new ExitPlayMode();
            ObjectUtils.DestroyAll<GameObject>();
        }
    }
}