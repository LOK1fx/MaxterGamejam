using System;
using System.Collections.Generic;
using UnityEngine;

namespace com.LOK1game.MaxterGamejam
{
    public abstract class Gun : PlayerItem
    {
        public event Action OnShoot;

        [SerializeField] protected GunProperties gun;

        protected int ammoInClip;
        protected int ammo;

        protected virtual void Awake()
        {
            ammo = gun.StartAmmo;
            ammoInClip = gun.ClipSize;
        }

        protected virtual void Shoot()
        {
            if(!CanShoot()) { return; }

            OnShoot?.Invoke();
        }

        protected virtual bool CanShoot()
        {
            if (ammoInClip < 0)
            {
                return false;
            }

            return true;
        }

        public GunProperties GetGunProperties()
        {
            return gun;
        }
    }
}