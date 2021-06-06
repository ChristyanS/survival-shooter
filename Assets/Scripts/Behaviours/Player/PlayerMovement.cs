using System;
using Enums;
using UnityEngine;

namespace Behaviours.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        private const float CamRayLenght = 100f;
        [SerializeField] [Range(1, 20)] private float speed = 6f;
        private UnityEngine.Camera _camera;
        private CharacterController _characterController;
        private int _floorLayerMaskValue;

        private Vector3 _movement;


        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();
            _floorLayerMaskValue = LayerMask.GetMask(Layer.Ground.ToString());
            _camera = UnityEngine.Camera.main;

            Validate();
        }

        private void Update()
        {
            Move(Input.GetAxisRaw(Axis.Horizontal.ToString()), Input.GetAxisRaw(Axis.Vertical.ToString()));
            Turning();
        }

        private void Validate()
        {
            if (_floorLayerMaskValue == (int) Layer.Default)
                throw new ArgumentException("No specific layer is found");
            if (!(_camera is { }))
                throw new ArgumentException("No camera setup found to this scene");
            if (_characterController == null)
                throw new ArgumentException("No character controller found");
        }

        private void Move(float horizontalAxis, float verticalAxis)
        {
            _movement.Set(horizontalAxis, 0, verticalAxis);

            _movement = _movement.normalized * (speed * Time.deltaTime);

            _characterController.Move(_movement);
        }

        private void Turning()
        {
            var cameraRay = _camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(cameraRay, out var floorHit, CamRayLenght, _floorLayerMaskValue))
            {
                var playerToMouse = floorHit.point - transform.position;
                playerToMouse.y = 0;

                var newRotation = Quaternion.LookRotation(playerToMouse);
                transform.rotation = newRotation;
            }
        }
    }
}