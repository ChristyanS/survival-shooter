using UnityEngine;
using UnityEngine.SceneManagement;

namespace Behaviours.Managers
{
    public class MenuManager : MonoBehaviour
    {
        public void Play()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        public void Exit()
        {
            Application.Quit();
            Debug.Log("Quit");
        }
    }
}