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
    public class DoublePointsActionTest
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
        public IEnumerator Execute_WhenAnActionIsTrigger_ShouldBeEnableDoublePoints()
        {
            var doublePointsActionBuilder = new DoublePointsActionBuilder(nameof(DoublePointsAction)).Build();

            var doublePointsAction = ((DoublePointsActionBuilder)doublePointsActionBuilder)
                .WithPowerUpImage()
                .WithTime(float.MinValue)
                .Component;

            doublePointsAction.Execute();

            Assert.AreEqual(1, doublePointsAction.PowerUpPanel.transform.childCount);
            Assert.AreEqual("PowerUpImage(Clone)", doublePointsAction.PowerUpPanel.transform.GetChild(0).name);
            Assert.IsTrue(GameManager.Instance.DoublePointsEnable);

            yield return new WaitForSeconds(doublePointsAction.Time);
            Assert.IsFalse(GameManager.Instance.DoublePointsEnable);

            yield return null;

            Assert.True(doublePointsAction == null);
        }

        [UnityTearDown]
        public IEnumerator TearDown()
        {
            yield return new ExitPlayMode();
            ObjectUtils.DestroyAll<GameObject>();
        }
    }
}