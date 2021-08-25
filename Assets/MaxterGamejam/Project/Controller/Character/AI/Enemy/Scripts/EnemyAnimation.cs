using com.LOK1game.recode.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace com.LOK1game.MaxterGamejam
{
    [RequireComponent(typeof(Animator), typeof(NavMeshAgent))]
    public class EnemyAnimation : MonoBehaviour
    {
        private Animator _animator;
        private NavMeshAgent _agent;

        private const string SPEED_ANIM_FLOAT = "Speed";

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _agent = GetComponent<NavMeshAgent>();
        }

        private void Update()
        {
            _animator.SetFloat(SPEED_ANIM_FLOAT, _agent.velocity.magnitude);
        }

        private void OnAnimatorIK(int layerIndex)
        {
            Player player = Player.LocalPlayerInstance;

            if (player == null) { return; }

            _animator.SetLookAtPosition(player.GetCameraTransform().position);
            _animator.SetLookAtWeight(0.8f, 0.4f, 1f, 0f);
        }
    }
}