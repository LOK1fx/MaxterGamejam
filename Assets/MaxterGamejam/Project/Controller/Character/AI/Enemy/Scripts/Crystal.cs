using System.Collections.Generic;
using UnityEngine;
using com.LOK1game.recode;

namespace com.LOK1game.MaxterGamejam
{
    public class Crystal : MonoBehaviour
    {
        [SerializeField] private GameObject _cells;

        private Hitbox _hitbox;
        private float _minForce = 20f;
        private float _maxForce = 36f;


        private void Start()
        {
            _hitbox = GetComponent<Hitbox>();

            _hitbox.OnHit += OnHit;
        }

        private void OnHit(object arg1, int arg2, object[] arg3)
        {
            Explode();
        }

        private void OnDestroy()
        {
            _hitbox.OnHit -= OnHit;
        }

        private void Explode()
        {
            var inCells = Instantiate(_cells, transform.position, transform.rotation);

            Destroy(inCells, 1f);

            foreach (Transform t in inCells.transform)
            {
                if (t.TryGetComponent<Rigidbody>(out var rb))
                {
                    rb.AddExplosionForce(Random.Range(_minForce, _maxForce), transform.position, 1f);
                    rb.AddTorque(rb.transform.forward * Random.Range(_minForce, _maxForce), ForceMode.Impulse);
                }
            }

            Destroy(gameObject);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if(collision.gameObject.TryGetComponent<IDamagable>(out var damagable))
            {
                damagable.TakeRadialDamage(this, 14);

                gameObject.layer = 13;
            }
        }
    }
}
