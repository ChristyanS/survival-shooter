using Behaviours.Player;
using Enums;
using Interfaces.Managers;
using UnityEngine;

namespace Test.Builders.Behaviours.Player
{
    public class PlayerMovementBuilder
    {
        public PlayerMovementBuilder()
        {
            GameObject = new GameObject();
        }

        public GameObject GameObject { get; }
        public PlayerMovement PlayerMovement { get; private set; }

        public PlayerMovementBuilder WithVirtualInputManager(IVirtualInputManager virtualInputManager)
        {
            PlayerMovement.VirtualInputInputManager = virtualInputManager;
            return this;
        }

        public PlayerMovementBuilder AddCharacterController()
        {
            GameObject.AddComponent<CharacterController>();
            return this;
        }

        public PlayerMovementBuilder AddMainCamera()
        {
            new CameraBuilder().Build().WithTag(Tag.MainCamera.ToString());
            return this;
        }

        public PlayerMovementBuilder AddAnimator()
        {
            GameObject.AddComponent<Animator>();
            return this;
        }


        public PlayerMovementBuilder Build()
        {
            PlayerMovement = GameObject.AddComponent<PlayerMovement>();
            return this;
        }
    }
}