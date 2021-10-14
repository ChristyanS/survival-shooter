using System;
using Enums;
using Interfaces.Managers;
using UnityEngine;

namespace Behaviours.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        private const float CamRayLenght = 100f;
        private static readonly int IsWalking = Animator.StringToHash("IsWalking");
        [SerializeField] [Range(1, 20)] private float speed = 6f;
        private Animator _animator;
        private UnityEngine.Camera _camera;
        private CharacterController _characterController;
        private int _floorLayerMaskValue;
        private Vector3 _movement;
        public float Speed => speed;
        public IVirtualInputManager VirtualInputInputManager { get; set; } //todo ver como usar esse cara private set

        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();
            _floorLayerMaskValue = LayerMask.GetMask(Layer.Ground.ToString());
            _animator = GetComponent<Animator>();
            _camera = UnityEngine.Camera.main;
            VirtualInputInputManager ??= Managers.VirtualInputInputManager.Instance;

            Validate();
        }

        private void Update()
        {
            var horizontal = VirtualInputInputManager.HorizontalAxis;
            var vertical = VirtualInputInputManager.VerticalAxis;
            Move(horizontal, vertical);
            Turning();
            Animating(horizontal, vertical);
        }

        private void Validate()
        {
            if (_floorLayerMaskValue == (int)Layer.Default)
                throw new ArgumentException("No specific layer is found");
            if (!(_camera is { }))
                throw new ArgumentException("No camera setup found to this scene");
            if (_characterController == null)
                throw new ArgumentException("No character controller found");
            if (_animator == null)
                throw new ArgumentException("No animator is found");
        }

        private void Move(float horizontalAxis, float verticalAxis)
        {
            _movement.Set(horizontalAxis, 0, verticalAxis);
            _movement = _movement.normalized * (speed * Time.deltaTime);

            _characterController.Move(_movement);
        }

        private void Turning()
        {
            var cameraRay = _camera.ScreenPointToRay(VirtualInputInputManager.MousePosition);

            if (Physics.Raycast(cameraRay, out var floorHit, CamRayLenght, _floorLayerMaskValue))
            {
                var playerToMouse = floorHit.point - transform.position;
                playerToMouse.y = 0;

                var newRotation = Quaternion.LookRotation(playerToMouse);
                transform.rotation = newRotation;
            }
        }

        private void Animating(float horizontal, float vertical)
        {
            var walking = horizontal != 0f || vertical != 0f;

            _animator.SetBool(IsWalking, walking);
        }
    }
}