using UnityEngine;

namespace Test.Builders.Behaviours
{
    public class CameraBuilder
    {
        private Camera _camera;

        public CameraBuilder Build()
        {
            _camera = new GameObject().AddComponent<Camera>();
            return this;
        }

        public CameraBuilder WithTag(string tag)
        {
            _camera.tag = tag;
            return this;
        }
    }
}