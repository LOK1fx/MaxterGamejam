using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.LOK1game.MaxterGamejam
{
    public class Glock : RaycastGun
    {
        private Animator _animator;

        protected override void Awake()
        {
            base.Awake();

            _animator = GetComponent<Animator>();
        }

        protected override void Shoot()
        {
            base.Shoot();

            transform.Rotate(Vector3.right, -15f);
            _animator.Play("Shoot");
        }
    }
}