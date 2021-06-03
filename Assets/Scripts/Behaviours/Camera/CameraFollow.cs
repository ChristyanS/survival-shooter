using Enums;
using UnityEngine;

namespace Behaviours.Camera
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] [Range(0, 10)] private float smoothing = 5f;
        private Vector3 _offset;
        private Transform _playerTransform;

        private void Start()
        {
            _playerTransform = GameObject.FindGameObjectWithTag(Tag.Player.ToString()).transform;
            _offset = transform.position - _playerTransform.position;
        }

        void Update()
        {
            Vector3 targetCamPos = _playerTransform.position + _offset;

            transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
        }
    }
}