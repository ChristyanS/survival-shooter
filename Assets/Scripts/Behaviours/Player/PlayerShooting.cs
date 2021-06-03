using System;
using System.Collections;
using Behaviours.Enemy;
using Enums;
using UnityEngine;

namespace Behaviours.Player
{
    public class PlayerShooting : MonoBehaviour
    {
        [SerializeField] [Range(0.1f, 200)] private int damagePerShot = 20;
        [SerializeField] [Range(0.1f, 50)] private float timeBetweenBullets = 0.15f;
        [SerializeField] [Range(1, 100)] private float range = 100f;
        private Light _gunLight;
        private LineRenderer _gunLine; // Reference to the line renderer.
        private bool _isShooting;
        private int _shootableMask;
        private RaycastHit _shootHit;
        private Ray _shootRay;
        private bool CanShooting => Input.GetButton(Axis.Fire1.ToString()) && !_isShooting;

        private void Start()
        {
            _shootableMask = LayerMask.GetMask(Layer.Shootable.ToString());
            _gunLine = GetComponent<LineRenderer>();
            _gunLight = GetComponent<Light>();

            Validate();
        }

        private void Update()
        {
            if (CanShooting)
            {
                StartCoroutine(Shoot());
            }
        }

        private IEnumerator Shoot()
        {
            Bullet();
            _isShooting = true;
            yield return new WaitForSeconds(timeBetweenBullets);
            _isShooting = false;
            EnableEffects(false);
        }

        public void EnableEffects(bool enable)
        {
            _gunLine.enabled = enable;
            _gunLight.enabled = enable;
        }

        private void Validate()
        {
            if (_shootableMask == (int) Layer.Default)
                throw new ArgumentException("No specific layer is found");

            if (_gunLine == null)
                throw new ArgumentException("No line renderer found");

            if (_gunLight == null)
                throw new ArgumentException("No light found");
        }

        void Bullet()
        {
            EnableEffects(true);

            var position = transform.position;
            _gunLine.SetPosition(0, position);

            _shootRay.origin = position;
            _shootRay.direction = transform.forward;

            if (Physics.Raycast(_shootRay, out _shootHit, range, _shootableMask))
            {
                var enemyHealth = _shootHit.collider.GetComponent<EnemyHealth>();

                if (enemyHealth != null)
                {
                    enemyHealth.TakeDamage(damagePerShot, _shootHit.point);
                }

                _gunLine.SetPosition(1, _shootHit.point);
            }
            else
            {
                _gunLine.SetPosition(1, _shootRay.origin + _shootRay.direction * range);
            }
        }
    }
}