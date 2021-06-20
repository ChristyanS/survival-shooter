using System;
using Behaviours.Player;
using Enums;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Behaviours.Managers
{
    public class GameOverManager : Singleton<GameOverManager>
    {
        private static readonly int GameOver = Animator.StringToHash("GameOver");
        private Animator _animator;
        private PlayerHealth _playerHealth;

        private void Start()
        {
            _playerHealth = GameObject.FindGameObjectWithTag(Tag.Player.ToString()).GetComponent<PlayerHealth>();
            _animator = GetComponent<Animator>();
            Validate();
        }

        private void Update()
        {
            if (!_playerHealth.IsAlive)
            {
                _animator.SetTrigger(GameOver);
                if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.R) ||
                    Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.KeypadEnter))
                    SceneManager.LoadScene("level");
            }
        }

        private void Validate()
        {
            if (_playerHealth == null)
                throw new ArgumentException("No player health found");
            if (_animator == null)
                throw new ArgumentException("No animator found");
        }
    }
}