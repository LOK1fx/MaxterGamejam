using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using com.LOK1game.recode;

namespace com.LOK1game.MaxterGamejam
{
    public class EnemyMask : MonoBehaviour
    {
        [SerializeField] private float _minForce = 25;
        [SerializeField] private float _maxForce = 45;
        [SerializeField] private GameObject _destroyedMask;

        private void Start()
        {
            var hitbox = GetComponent<Hitbox>();

            hitbox.OnHit += OnHit;
        }

        private void OnHit(object arg1, int arg2, object[] arg3)
        {
            Explode();
        }

        public void Explode()
        {
            var mask = Instantiate(_destroyedMask, transform.position, transform.rotation);

            Destroy(mask, 1f);

            var explodePos = transform.position + transform.forward * 0.12f + Vector3.down * 0.14f;

            foreach (Transform t in mask.transform)
            {
                if (t.TryGetComponent<Rigidbody>(out var rb))
                {


                    rb.AddExplosionForce(Random.Range(_minForce, _maxForce), explodePos, 1f);
                    rb.AddTorque(rb.transform.forward * Random.Range(_minForce, _maxForce), ForceMode.Impulse);
                }
            }

            gameObject.SetActive(false);
        }
    }
}