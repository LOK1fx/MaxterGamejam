using System;
using UnityEngine;

namespace com.LOK1game.recode.Player
{
    public class PlayerVaulting : PlayerAbilityBase
    {
        [SerializeField] private LayerMask _groundMask;

        private Player _player;

        private void Start()
        {
            _player = GetComponent<Player>();
        }

        private void OnCollisionEnter(Collision collision)
        {
            var normal = collision.contacts[0].normal;

            if (IsWall(normal) && _player != null && !PlayerState.wallruning && _player.GetInputMoveAxis().normalized != null)
            {
                Vault();
            }
        }

        private void Vault()
        {  
            var dir = _player.GetDirection(_player.GetInputMoveAxis()).normalized;
            var maxVaultPos = transform.position + Vector3.up * _player.playerHeight;

            if (Physics.Raycast(maxVaultPos, dir, 1.5f, _groundMask))
            {
                return;
            }

            var hoverPos = maxVaultPos + dir;

            if (!Physics.Raycast(hoverPos, Vector3.down, out RaycastHit hit, 1.5f, _groundMask))
            {
                return;
            }

            var landPos = hit.point;
            var prevCameraPos = MoveCamera.Instance.transform.position;

            MoveCamera.Instance.vaultOffset += transform.position - landPos;

            Debug.DrawLine(prevCameraPos, MoveCamera.Instance.transform.position, Color.yellow, 1f);

            transform.position = landPos;

            Debug.DrawRay(landPos, Vector3.up * _player.playerHeight, Color.cyan, 1f);
        }

        private bool IsWall(Vector3 normal)
        {
            return Math.Abs(90f - Vector3.Angle(Vector3.up, normal)) < 0.1f;
        }
    }
}