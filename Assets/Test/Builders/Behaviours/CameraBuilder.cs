using Enums;
using UnityEngine;

namespace Test.Builders.Behaviours
{
    public class CameraBuilder
    {
        private readonly Camera _camera;

        public CameraBuilder()
        {
            _camera = new GameObject().AddComponent<Camera>();
            Tag.MainCamera.ToString();
        }

        public CameraBuilder WithTag(string tag)
        {
            _camera.tag = tag;
            return this;
        }

        public Camera Build()
        {
            return _camera;
        }
    }
}