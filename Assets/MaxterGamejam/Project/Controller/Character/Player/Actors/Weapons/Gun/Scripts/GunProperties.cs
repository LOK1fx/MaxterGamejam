using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.LOK1game.MaxterGamejam
{
    [CreateAssetMenu(fileName = "new GunData", menuName = "GunData")]
    public class GunProperties : ScriptableObject
    {
        [Header("Base")]
        public int Damage = 10;
        public float FireRate = 0.2f;

        [Header("Distances")]
        public float ShootDistance = 50f;
        public float MaxShootDistance = 100f;

        [Header("Ammo")]
        public int ClipSize = 10;
        public int StartAmmo = 20;

        [Header("Sounds")]
        public AudioClip ShootSound;

        [Header("Other")]
        public LayerMask HitMask;
        public GameObject BulletHole;
    }
}