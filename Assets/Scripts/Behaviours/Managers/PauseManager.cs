using UnityEngine;

namespace Behaviours.Managers
{
    public class PauseManager : MonoBehaviour
    {
        private bool _paused;

        private void Update()
        {
            if (VirtualInputInputManager.Instance.PressPauseButton)
                switch (_paused)
                {
                    case false:
                        Pause();
                        break;
                    default:
                        Resume();
                        break;
                }
        }

        private void Pause()
        {
            Time.timeScale = 0;
            _paused = true;
        }

        private void Resume()
        {
            Time.timeScale = 1;
            _paused = false;
        }
    }
}