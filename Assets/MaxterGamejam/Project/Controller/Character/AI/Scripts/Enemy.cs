using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using com.LOK1game.recode;
using com.LOK1game.recode.AI;

namespace com.LOK1game.MaxterGamejam
{
    public class Enemy : MonoBehaviour
    {
        public int Health { get; private set; }

        [SerializeField] private EnemyMask _mask;
        [SerializeField] private int _maxHealth;

        [Header("Other")]
        [SerializeField] private EnemyCrystal _crystalAtack;

        private Ragdoll _ragdoll;
        private AiAgent _agent;

        private Hitbox[] _hitboxes;

        private bool _dead;

        private void Start()
        {
            _ragdoll = GetComponent<Ragdoll>();
            _agent = GetComponent<AiAgent>();

            _hitboxes = new Hitbox[_ragdoll.Colliders.Length + 1];

            for (int i = 0; i < _ragdoll.Colliders.Length; i++)
            {
                _hitboxes[i] = _ragdoll.Colliders[i].gameObject.GetComponent<Hitbox>();
            }

            var maskIndex = _hitboxes.Length - 1;

            _hitboxes[maskIndex] = _mask.GetComponent<Hitbox>();
            _hitboxes[maskIndex].OnHit += OnTakeDamage;
             
            foreach (var hitbox in _hitboxes)
            {
                hitbox.OnHit += OnTakeDamage;
            }

            Health = _maxHealth;
        }

        public void CreateCrystal(Vector3 pos)
        {
            var crystal = Instantiate(_crystalAtack, pos + Vector3.up, Quaternion.identity);

            crystal.Spawn(false);
        }

        public void AddHealth(int value)
        {
            Health += value;

            Health = Mathf.Clamp(Health, 0, _maxHealth);
        }

        private void OnTakeDamage(object arg1, int arg2, object[] arg3)
        {
            if(Health > 0)
            {
                Health -= arg2;

                if(Health <= 0 && !_dead)
                {
                    _agent.Death();

                    Death((Vector3)arg3[0], (Hitbox.Part)arg3[1]);
                }
            }
        }

        private void Death(Vector3 dir, Hitbox.Part part)
        {
            float force;

            if(part == Hitbox.Part.Head)
            {
                force = 100f;
            }
            else
            {
                force = 30f;

                Hitmarker.Instance.GetKill();
            }

            _ragdoll.Velocity = dir * force;
            _ragdoll.TurnOn();


            _mask?.Explode();

            _dead = true;

            Destroy(gameObject, 5f);
        }
    }
}