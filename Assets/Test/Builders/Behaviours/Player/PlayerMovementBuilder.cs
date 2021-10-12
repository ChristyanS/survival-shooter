using Behaviours.Managers;
using Behaviours.Player;
using Enums;
using Interfaces.Managers;
using UnityEngine;

namespace Test.Builders.Behaviours.Player
{
    public class PlayerMovementBuilder : GameObjectBuilder<PlayerMovement>
    {
        public PlayerMovementBuilder()
        {
        }

        public PlayerMovementBuilder(GameObject gameObject) : base(gameObject)
        {
        }

        public PlayerMovementBuilder WithVirtualInputManager(IVirtualInputManager virtualInputManager)
        {
            Component.VirtualInputInputManager = virtualInputManager;
            return this;
        }

        public PlayerMovementBuilder AddCharacterController()
        {
            GameObject.AddComponent<CharacterController>();
            return this;
        }

        public PlayerMovementBuilder AddVirtualInputManager()
        {
            new GameObject().AddComponent<VirtualInputInputManager>();
            return this;
        }

        public PlayerMovementBuilder AddMainCamera()
        {
            new GameObjectBuilder<Camera>().Build().WithTag(Tag.MainCamera.ToString());
            return this;
        }

        public PlayerMovementBuilder AddAnimator()
        {
            GameObject.AddComponent<Animator>();
            return this;
        }
    }
}