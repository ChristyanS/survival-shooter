using Behaviours.Player;
using Enums;
using Interfaces.Managers;
using UnityEngine;

namespace Test.Builders.Behaviours.Player
{
    public class PlayerMovementBuilder
    {
        private readonly GameObject _gameObject;
        private PlayerMovement _playerMovement;

        public PlayerMovementBuilder()
        {
            _gameObject = new GameObject();
        }

        public PlayerMovementBuilder WithVirtualInputManager(IVirtualInputManager virtualInputManager)
        {
            _playerMovement.VirtualInputInputManager = virtualInputManager;
            return this;
        }

        public PlayerMovementBuilder AddCharacterController()
        {
            _gameObject.AddComponent<CharacterController>();
            return this;
        }

        public PlayerMovementBuilder AddMainCamera()
        {
            new CameraBuilder().Build().WithTag(Tag.MainCamera.ToString());
            return this;
        }

        public PlayerMovementBuilder AddAnimator()
        {
            _gameObject.AddComponent<Animator>();
            return this;
        }


        public PlayerMovementBuilder Build()
        {
            _playerMovement = _gameObject.AddComponent<PlayerMovement>();
            return this;
        }
    }
}