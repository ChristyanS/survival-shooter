using UnityEngine;

namespace Interfaces.Managers
{
    public interface IVirtualInputManager
    {
        public float HorizontalAxis { get; }
        public float VerticalAxis { get; }
        public Vector3 MousePosition { get; }
    }
}