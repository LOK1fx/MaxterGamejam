using System;
using System.Collections.Generic;
using UnityEngine;
using com.LOK1game.recode.Player;
using com.LOK1game.recode;

namespace com.LOK1game.MaxterGamejam
{
    public abstract class RaycastGun : Gun
    {
        public event Action<Hitbox.Part> OnHit;

        [SerializeField] private LineRenderer _trailPrefab;
        [SerializeField] protected Transform firePoint;

        private ControlsAction _input;

        private void Start()
        {
            _input = PlayerInput.GetInput();

            _input.Player.Fire.performed += ctx => Use(this);
        }

        public override void Use(object sender)
        {
            Shoot();
        }

        protected override void Shoot()
        {
            base.Shoot();

            var origin = MoveCamera.Instance.transform;

            if (Physics.Raycast(origin.position, origin.forward, out RaycastHit hit, 10000f, gun.HitMask, QueryTriggerInteraction.Ignore))
            {
                if(hit.distance < gun.MaxShootDistance)
                {
                    Impact(hit);
                }

                if(_trailPrefab != null && firePoint != null)
                {
                    var trail = Instantiate(_trailPrefab);

                    trail.SetPosition(0, firePoint.position);
                    trail.SetPosition(1, hit.point);

                    Destroy(trail.gameObject, 0.11f);
                }
            }
        }

        protected virtual void Impact(RaycastHit hit)
        {
            if (hit.collider.gameObject.TryGetComponent<IHitbox>(out var damagable))
            {
                var dir = (hit.collider.transform.position - transform.position).normalized;

                damagable.TakePointDamage(this, gun.Damage, dir);

                OnHit?.Invoke(damagable.GetBodyPart());

                switch (damagable.GetBodyPart())
                {
                    case Hitbox.Part.Head:
                        Hitmarker.Instance.HeadCrack();
                        //Hitmarker.Instance.BodyHit();
                        break;
                    case Hitbox.Part.Neck:
                        break;
                    case Hitbox.Part.UpperBody:
                        Hitmarker.Instance.BodyHit();
                        //Hitmarker.Instance.HeadCrack();
                        break;
                    case Hitbox.Part.LowerBody:
                        break;
                    case Hitbox.Part.Arms:
                        break;
                    case Hitbox.Part.Legs:
                        break;
                    default:
                        break;
                }

                Debug.Log(hit.collider.name);
            }
            else
            {
                if(gun.BulletHole)
                {
                    var bulletHole = Instantiate(gun.BulletHole, hit.point + hit.normal * 0.001f, Quaternion.identity, hit.transform);

                    bulletHole.transform.LookAt(hit.point + hit.normal);
                    bulletHole.transform.SetParent(hit.transform);

                    var lossyScale = bulletHole.transform.lossyScale;

                    bulletHole.transform.localScale = new Vector3(1f / lossyScale.x, 1f / lossyScale.y, 1f / lossyScale.z);
                }
            }
        }

        private void OnDestroy()
        {
            _input.Player.Fire.performed -= ctx => Use(this);
        }
    }
}