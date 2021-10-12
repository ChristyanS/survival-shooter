using System.Collections;
using Behaviours.Managers;
using Interfaces.Managers;
using Moq;
using NUnit.Framework;
using Test.Builders.Behaviours.Player;
using UnityEngine;
using UnityEngine.TestTools;
using Utils;

namespace Test.PlayMode.Player
{
    public class PlayerMovementTest
    {
        private Mock<IVirtualInputManager> _virtualInputManager;

        [UnitySetUp]
        public IEnumerator SetUp()
        {
            yield return new EnterPlayMode();
            LogAssert.ignoreFailingMessages = true;
            new GameObject().AddComponent<VirtualInputInputManager>();
            _virtualInputManager = new Mock<IVirtualInputManager>();
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
        public IEnumerator Update_WhenCharacterMove_ShouldMoveCharacter()
        {
            var playerMovementBuilder = new PlayerMovementBuilder().AddMainCamera().AddCharacterController()
                .AddAnimator().Build() as PlayerMovementBuilder;

            playerMovementBuilder?.WithVirtualInputManager(_virtualInputManager.Object);

            const int horizontalAxis = 1;
            const int verticalAxis = 1;
            _virtualInputManager.SetupGet(virtualInput => virtualInput.HorizontalAxis).Returns(horizontalAxis);
            _virtualInputManager.SetupGet(virtualInput => virtualInput.VerticalAxis).Returns(verticalAxis);

            yield return null;

            var transform = new Vector3(horizontalAxis, 0, verticalAxis).normalized *
                            (playerMovementBuilder?.Component.Speed * Time.deltaTime);

            Assert.AreEqual(transform, playerMovementBuilder?.GameObject.transform.position);
        }
        //todo analyze this test for Raycast to work 
        // [UnityTest]
        // public IEnumerator Update_WhenRotateCharacter_ShouldRotateCharacter()
        // {
        //     var playerMovementBuilder = new PlayerMovementBuilder().AddMainCamera().AddCharacterController()
        //         .AddAnimator().Build()
        //         .WithVirtualInputManager(_virtualInputManager.Object);
        //     var ground = new GameObject
        //     {
        //         layer = LayerMask.NameToLayer(Layer.Ground.ToString()),
        //         transform =
        //         {
        //             position = Vector3.down
        //         }
        //     };
        //
        //     ground.AddComponent<MeshCollider>();
        //
        //     _virtualInputManager.SetupGet(virtualInput => virtualInput.MousePosition).Returns(Vector3.zero);
        //
        //     yield return null;
        //
        //     Assert.AreEqual(Vector3.zero, playerMovementBuilder.GameObject.transform.position);
        // }

        [UnityTearDown]
        public IEnumerator TearDown()
        {
            yield return new ExitPlayMode();
            ObjectUtils.DestroyAll<GameObject>();
        }
    }
}