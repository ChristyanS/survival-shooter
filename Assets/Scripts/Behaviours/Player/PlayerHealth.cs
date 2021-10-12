using System;
using Enums;
using UnityEngine;
using UnityEngine.UI;

namespace Behaviours.Player
{
    public class PlayerHealth : MonoBehaviour
    {
        private static readonly int Die = Animator.StringToHash("Die");
        [SerializeField] [Range(1, 200)] private int startingHealth = 100;
        [SerializeField] [Range(1, 10)] private float flashSpeed = 5f;
        [SerializeField] private Color flashColour = new Color(1f, 0f, 0f, 0.1f);
        [SerializeField] public AudioClip deathClip;
        private Animator _animator;
        private AudioSource _audioSource;

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
            _audioSource = GetComponent<AudioSource>();
            _animator = GetComponent<Animator>();

            Validate();

            _healthSlider.maxValue = startingHealth;
        }

        private void Update()
        {
            if (_damageImage)
            {
                _damageImage.color = _isDamaged
                    ? flashColour
                    : Color.Lerp(_damageImage.color, Color.clear, flashSpeed * Time.deltaTime);

                _isDamaged = false;
            }
        }

        private void Validate()
        {
            if (_healthSlider == null)
                throw new ArgumentException("No Slider is found");
            if (_damageImage == null)
                throw new ArgumentException("No damage image found");
            if (_playerMovement == null)
                throw new ArgumentException("No player movement found");
            if (_audioSource == null)
                throw new ArgumentException("No AudioSource found");
            if (deathClip == null)
                throw new ArgumentException("No death audio clip found");
        }

        public void TakeDamage(int amount)
        {
            _isDamaged = true;
            _currentHealth -= amount;
            _healthSlider.value = _currentHealth;
            _audioSource.Play();

            if (!IsAlive && !_isDead) Death();
        }

        public void AddHealth(int health)
        {
            _currentHealth += health;
            if (_currentHealth > startingHealth)
                _currentHealth = startingHealth;
            _healthSlider.value = _currentHealth;
        }

        private void Death()
        {
            _isDead = true;
            _animator.SetTrigger(Die);
            _playerMovement.enabled = false;
            _audioSource.clip = deathClip;
            _audioSource.Play();
        }
    }
}