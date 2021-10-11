using System.Collections;
using Behaviours.Managers;
using Test.Builders.Behaviours.Player;
using UnityEngine;
using UnityEngine.TestTools;
using Utils;

namespace Test.PlayMode.Player
{
    public class PlayerMovementTest
    {
        [UnitySetUp]
        public IEnumerator SetUp()
        {
            yield return new EnterPlayMode();
            LogAssert.ignoreFailingMessages = true;
            new GameObject().AddComponent<VirtualInputInputManager>();
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
            new PlayerMovementBuilder().AddMainCamera().Build();

            LogAssert.Expect(LogType.Exception, "ArgumentException: No character controller found");

            yield return null;
        }

        [UnityTest]
        public IEnumerator Awake_WhenNoAnimatorIsFound_ThrowsArgumentException()
        {
            new PlayerMovementBuilder().AddMainCamera().AddCharacterController().Build();

            LogAssert.Expect(LogType.Exception, "ArgumentException: No animator is found");

            yield return null;
        }

        [UnityTest]
        public IEnumerator Awake_WhenCharacterMoveVertically_ShouldMoveCharacter()
        {
            new PlayerMovementBuilder().AddMainCamera().AddCharacterController().AddAnimator().Build();

            yield return null;
        }

        [UnityTearDown]
        public IEnumerator TearDown()
        {
            yield return new ExitPlayMode();
            ObjectUtils.DestroyAll<GameObject>();
        }
    }
}