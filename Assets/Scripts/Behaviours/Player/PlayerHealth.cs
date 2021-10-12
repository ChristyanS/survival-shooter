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

        private Slider _healthSlider;

        private bool _isDead;
        private PlayerMovement _playerMovement;
        public int StartingHealth => startingHealth;
        public Color FlashColour => flashColour;
        public AudioSource AudioSource { get; private set; }

        public int CurrentHealth { get; private set; }

        public Image DamageImage { get; private set; }
        public bool IsDamaged { get; private set; }
        public bool IsAlive => CurrentHealth > 0;

        private void Start()
        {
            CurrentHealth = startingHealth;
            _healthSlider = GameObject.FindGameObjectWithTag(Tag.Health.ToString()).GetComponent<Slider>();
            DamageImage = GameObject.FindGameObjectWithTag(Tag.DamageImage.ToString()).GetComponent<Image>();
            _playerMovement = GetComponent<PlayerMovement>();
            AudioSource = GetComponent<AudioSource>();
            _animator = GetComponent<Animator>();

            Validate();

            _healthSlider.maxValue = startingHealth;
        }

        private void Update()
        {
            if (DamageImage)
            {
                DamageImage.color = IsDamaged
                    ? flashColour
                    : Color.Lerp(DamageImage.color, Color.clear, flashSpeed * Time.deltaTime);

                IsDamaged = false;
            }
        }

        private void Validate()
        {
            if (_healthSlider == null)
                throw new ArgumentException("No Slider is found");
            if (DamageImage == null)
                throw new ArgumentException("No damage image found");
            if (_playerMovement == null)
                throw new ArgumentException("No player movement found");
            if (AudioSource == null)
                throw new ArgumentException("No AudioSource found");
            if (deathClip == null)
                throw new ArgumentException("No death audio clip found");
        }

        public void TakeDamage(int amount)
        {
            IsDamaged = true;
            CurrentHealth -= amount;
            _healthSlider.value = CurrentHealth;
            AudioSource.Play();

            if (!IsAlive && !_isDead) Death();
        }

        public void AddHealth(int health)
        {
            CurrentHealth += health;
            if (CurrentHealth > startingHealth)
                CurrentHealth = startingHealth;
            _healthSlider.value = CurrentHealth;
        }

        private void Death()
        {
            _isDead = true;
            _animator.SetTrigger(Die);
            _playerMovement.enabled = false;
            AudioSource.clip = deathClip;
            AudioSource.Play();
        }
    }
}