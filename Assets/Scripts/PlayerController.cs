using System;
using Enums;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private const float CamRayLenght = 100f;
    [SerializeField] private float speed;
    private Camera _camera;
    private CharacterController _characterController;
    private int _floorLayerMaskValue;

    private Vector3 _movement;


    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _floorLayerMaskValue = LayerMask.GetMask(Layers.Ground.ToString());
        _camera = Camera.main;

        Validate();
    }

    private void Update()
    {
        Move(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Turning();
    }

    private void Validate()
    {
        if (_floorLayerMaskValue == (int) Layers.Default)
            throw new ArgumentException("No specific layer is found");
        if (!(_camera is { }))
            throw new ArgumentException("No camera setup found to this scene");
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