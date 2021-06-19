using System;
using Behaviours.Managers;
using UnityEngine;
using UnityEngine.AI;

namespace Behaviours.Enemy
{
    public class EnemyHealth : MonoBehaviour
    {
        [SerializeField] [Range(1, 200)] private int startingHealth = 100;
        [SerializeField] [Range(1, 20)] private float sinkSpeed = 2.5f;
        [SerializeField] [Range(0, 5)] private float sinkTime = 2;
        [SerializeField] [Range(0, 200)] private int scoreValue = 10;
        [SerializeField] private AudioClip deathClip;
        private CapsuleCollider _capsuleCollider;
        private ParticleSystem _hitParticles;
        private int _currentHealth;
        private bool _isDead;
        private bool _isSinking;
        private NavMeshAgent _navMeshAgent;
        private EnemyDrop _enemyDrop;
        private AudioSource _audioSource;
        private Animator _animator;
        private static readonly int Die = Animator.StringToHash("Die");
        public bool IsAlive => _currentHealth > 0;


        private void Start()
        {
            _capsuleCollider = GetComponent<CapsuleCollider>();
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _enemyDrop = GetComponent<EnemyDrop>();
            _audioSource = GetComponent<AudioSource>();
            _hitParticles = GetComponentInChildren<ParticleSystem>();
            _animator = GetComponent<Animator>();
            _currentHealth = startingHealth;
            Validate();
        }

        private void Update()
        {
            if (_isSinking)
            {
                transform.Translate(-Vector3.up * (sinkSpeed * Time.deltaTime));
            }
        }

        private void Validate()
        {
            if (_capsuleCollider == null)
                throw new ArgumentException("No capsule collider is found");
            if (_navMeshAgent == null)
                throw new ArgumentException("No nav mesh agent is found");
            if (_enemyDrop == null)
                throw new ArgumentException("No enemy drop is found");
            if (_audioSource == null)
                throw new ArgumentException("No Audio Source is found");
            if (deathClip == null)
                throw new ArgumentException("No death clip is found");
            if (_hitParticles == null)
                throw new ArgumentException("No particle system is found");
        }

        public void TakeDamage(int amount)
        {
            if (!_isDead)
            {
                _audioSource.Play();

                _hitParticles.Play();
                if (GameManager.InstaKillEnable)
                    _currentHealth = 0;
                else
                    _currentHealth -= amount;
                if (!IsAlive)
                {
                    Death();
                }
            }
        }

        public void TakeDamage()
        {
            TakeDamage(_currentHealth);
        }

        private void Death()
        {
            _isDead = true;
            _capsuleCollider.isTrigger = true;
            _animator.SetTrigger(Die);
            _enemyDrop.Drop();
            _audioSource.clip = deathClip;
            _audioSource.Play();
            WaveManager.DeadEnemies++;
            if (WaveManager.AllEnemiesDie)
                WaveManager.NextWave();
        }

        // used in animation events
        public void StartSinking()
        {
            _navMeshAgent.enabled = false;
            _isSinking = true;
            if (GameManager.DoublePointsEnable)
                scoreValue *= 2;
            ScoreManager.Score += scoreValue;
            KillManager.Kills++;
            Destroy(gameObject, sinkTime);
        }
    }
}