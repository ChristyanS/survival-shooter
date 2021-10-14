using Enums;
using Interfaces.Managers;
using UnityEngine;

namespace Behaviours.Managers
{
    public class VirtualInputInputManager : Singleton<VirtualInputInputManager>, IVirtualInputManager
    {
        public float HorizontalAxis => Input.GetAxisRaw(Axis.Horizontal.ToString());
        public float VerticalAxis => Input.GetAxisRaw(Axis.Vertical.ToString());
        public Vector3 MousePosition => Input.mousePosition;
    }
}