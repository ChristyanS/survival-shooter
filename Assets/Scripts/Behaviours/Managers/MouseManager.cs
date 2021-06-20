using UnityEngine;

namespace Behaviours.Managers
{
    public class MouseManager : Singleton<MouseManager>
    {
        [SerializeField] private Texture2D mouseTexture;

        private void Start()
        {
            Cursor.SetCursor(mouseTexture, Vector2.zero, CursorMode.ForceSoftware);
        }
    }
}