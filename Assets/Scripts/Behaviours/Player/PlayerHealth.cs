using System;
using Enums;
using UnityEngine;
using UnityEngine.UI;

namespace Behaviours.Player
{
    public class PlayerHealth : MonoBehaviour
    {
        [SerializeField] [Range(1, 200)] private int startingHealth = 100;
        [SerializeField] [Range(1, 10)] private float flashSpeed = 5f;
        [SerializeField] private Color flashColour = new Color(1f, 0f, 0f, 0.1f);

        private int _currentHealth;
        private Image _damageImage;
        private Slider _healthSlider;
        private bool _isDamaged;
        private bool _isDead;
        private PlayerMovement _playerMovement;

        public bool IsAlive => _currentHealth > 0;

        private void Start()
        {
            _currentHealth = startingHealth;
            _healthSlider = GameObject.FindGameObjectWithTag(Tag.Health.ToString()).GetComponent<Slider>();
            _damageImage = GameObject.FindGameObjectWithTag(Tag.DamageImage.ToString()).GetComponent<Image>();
            _playerMovement = GetComponent<PlayerMovement>();

            Validate();

            _healthSlider.maxValue = startingHealth;
        }

        private void Update()
        {
            _damageImage.color = _isDamaged
                ? flashColour
                : Color.Lerp(_damageImage.color, Color.clear, flashSpeed * Time.deltaTime);

            _isDamaged = false;
        }

        private void Validate()
        {
            if (_healthSlider == null)
                throw new ArgumentException("No Slider is found");
            if (_playerMovement == null)
                throw new ArgumentException("No player movement found");
            if (_damageImage == null)
                throw new ArgumentException("No damage image found");
        }

        public void TakeDamage(int amount)
        {
            _isDamaged = true;
            _currentHealth -= amount;
            _healthSlider.value = _currentHealth;

            if (_currentHealth <= 0 && !_isDead)
            {
                Death();
            }
        }

        private void Death()
        {
            _isDead = true;
            _playerMovement.enabled = false;
        }
    }
}