using System;
using UnityEngine;

namespace com.LOK1game.recode
{
    public class Projectile : MonoBehaviour
    {
        public event Action<RaycastHit> OnHit;
        public event Action<RaycastHit, Hitbox.Part> OnHitboxHit;

        [SerializeField] private LayerMask _hitableMask;
        [SerializeField] private GameObject _bulletImpact;

        private int _damage;
        private float _speed;

        private Vector3 _previusPosition; //Сохранение позиции в прошлом кадре для 
        private Rigidbody _rb;

        public Projectile(int damage, float speed)
        {
            SetProjectileParameters(damage, speed);
        }

        private void Start()
        {
            _rb = GetComponent<Rigidbody>();
        }

        private void OnEnable()
        {
            ShootProjectile();
        }

        /// <summary>
        /// Подготовка снаряда к выстрелу
        /// </summary>
        /// <param name="damage">Базовый урон</param>
        /// <param name="speed">Начальная скорость снаряда</param>
        public void SetProjectileParameters(int damage, float speed)
        {
            _damage = damage;
            _speed = speed;
        }

        private void ShootProjectile()
        {
            _rb.AddForce(transform.forward * _speed, ForceMode.Impulse);
        }

        private void LateUpdate()
        {
            //Проверка на пролёт снаряда сквозь объекты.
            //Если скорость снаряда слишком большая, он может просто пролететь мимо его.
            //Делается рейкаст с позиции снаряда в прошлом кадре в текущую.
            if(Physics.Raycast(_previusPosition, transform.forward, out RaycastHit hit, Vector3.Distance(transform.position, _previusPosition), _hitableMask, QueryTriggerInteraction.Ignore))
            {
                Impact(hit);

                Destroy(gameObject);

                return;
            }

            Debug.DrawLine(_previusPosition, transform.position, Color.red);

            _previusPosition = transform.position;
        }

        private void Impact(RaycastHit hit)
        {
            OnHit?.Invoke(hit);

            var damagable = hit.transform.GetComponent<IDamagable>();

            if (damagable != null)
            {
                Vector3 dir = (transform.position - hit.point).normalized;

                var hitbox = GetComponent<IHitbox>();

                if (hitbox != null)
                {
                    OnHitboxHit?.Invoke(hit, hitbox.GetBodyPart());
                }

                damagable.TakePointDamage(this, _damage, dir);

                return;
            }

            var rb = hit.transform.GetComponent<Rigidbody>();

            rb.AddForceAtPosition(-hit.normal * _damage, hit.point, ForceMode.Impulse);

            if(_bulletImpact != null)
            {
                var bulletHole = Instantiate(_bulletImpact, hit.point + hit.normal * 0.001f, Quaternion.identity, hit.transform);

                bulletHole.transform.LookAt(hit.point + hit.normal);
                bulletHole.transform.SetParent(hit.transform);
            }
        }
    }
}