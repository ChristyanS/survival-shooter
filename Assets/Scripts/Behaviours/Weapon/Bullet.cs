using Behaviours.Enemy;
using Enums;
using UnityEngine;

namespace Behaviours.Gun
{
    public class Bullet : MonoBehaviour
    {
        private int _damage;
        private Vector3 _shootDirection;
        private float _speed;

        private void Update()
        {
            transform.position += _shootDirection * (_speed * Time.deltaTime);
        }


        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag(Tag.Enemy.ToString()) && !other.isTrigger)
            {
                var enemyHealth = other.gameObject.GetComponent<EnemyHealth>();

                if (enemyHealth != null)
                {
                    enemyHealth.TakeDamage(_damage);
                }
            }

            if (other.gameObject.layer == LayerMask.NameToLayer(Layer.Shootable.ToString()) && !other.isTrigger)
            {
                Destroy(gameObject);
            }
        }

        public void Setup(Vector3 shootDirection, float speed, int damage)
        {
            _shootDirection = shootDirection;
            _speed = speed;
            _damage = damage;
        }
    }
}