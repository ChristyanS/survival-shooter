using Behaviours.Player;
using Interfaces.Managers;
using UnityEngine;

namespace Test.Builders.Behaviours.Player
{
    public class PlayerMovementBuilder
    {
        private readonly GameObject _gameObject;
        private readonly PlayerMovement _playerMovement;

        public PlayerMovementBuilder()
        {
            _gameObject = new GameObject();
            _playerMovement = _gameObject.AddComponent<PlayerMovement>();
        }

        public PlayerMovementBuilder WithVirtualInputManager(IVirtualInputManager virtualInputManager)
        {
            _playerMovement.VirtualInputInputManager = virtualInputManager;
            return this;
        }

        public PlayerMovementBuilder WithCharacterController()
        {
            _gameObject.AddComponent<CharacterController>();
            return this;
        }


        public PlayerMovement Build()
        {
            return _playerMovement;
        }
    }
}