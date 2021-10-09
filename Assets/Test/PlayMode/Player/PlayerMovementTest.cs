using System.Collections;
using Enums;
using Test.Builders.Behaviours;
using Test.Builders.Behaviours.Player;
using UnityEngine;
using UnityEngine.TestTools;

namespace Test.PlayMode.Player
{
    public class PlayerMovementTest
    {
        [UnitySetUp]
        public IEnumerator SetUp()
        {
            yield return new EnterPlayMode();
            LogAssert.ignoreFailingMessages = true;
        }

        [UnityTest]
        public IEnumerator Awake_WhenNoCameraSetupIsFound_ThrowsArgumentException()
        {
            new PlayerMovementBuilder().Build();

            LogAssert.Expect(LogType.Exception, "ArgumentException: No camera setup found to this scene");

            yield return null;
        }

        [UnityTest]
        public IEnumerator Awake_WhenNoCharacterControllerIsFound_ThrowsArgumentException()
        {
            new CameraBuilder().WithTag(Tag.MainCamera.ToString()).Build();
            new PlayerMovementBuilder().Build();

            LogAssert.Expect(LogType.Exception, "ArgumentException: No character controller found");

            yield return null;
        }

        // [UnityTest]
        // public IEnumerator Awake_WhenNoCharacjterControllerIsFound_ThrowsArgumentException()
        // {
        //     new CameraBuilder().WithTag(Tag.MainCamera.ToString()).Build();
        //
        //     var mockVirtualInputManager = new Mock<IVirtualInputManager>();
        //     new PlayerMovementBuilder().WithVirtualInputManager(mockVirtualInputManager.Object).Build();
        //
        //     mockVirtualInputManager.SetupGet(x => x.HorizontalAxis).Returns(1);
        //
        //     yield return null;
        // }
    }
}