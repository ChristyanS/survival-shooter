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

        public Slider HealthSlider { get; private set; }

        public bool IsDead { get; private set; }

        public PlayerMovement PlayerMovement { get; private set; }

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
            HealthSlider = GameObject.FindGameObjectWithTag(Tag.Health.ToString()).GetComponent<Slider>();
            DamageImage = GameObject.FindGameObjectWithTag(Tag.DamageImage.ToString()).GetComponent<Image>();
            PlayerMovement = GetComponent<PlayerMovement>();
            AudioSource = GetComponent<AudioSource>();
            _animator = GetComponent<Animator>();

            Validate();

            HealthSlider.maxValue = startingHealth;
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
            if (HealthSlider == null)
                throw new ArgumentException("No Slider is found");
            if (DamageImage == null)
                throw new ArgumentException("No damage image found");
            if (PlayerMovement == null)
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
            HealthSlider.value = CurrentHealth;
            AudioSource.Play();

            if (!IsAlive && !IsDead) Death();
        }

        public void AddHealth(int health)
        {
            CurrentHealth += health;
            if (CurrentHealth > startingHealth)
                CurrentHealth = startingHealth;
            HealthSlider.value = CurrentHealth;
        }

        private void Death()
        {
            IsDead = true;
            _animator.SetTrigger(Die);
            PlayerMovement.enabled = false;
            AudioSource.clip = deathClip;
            AudioSource.Play();
        }
    }
}