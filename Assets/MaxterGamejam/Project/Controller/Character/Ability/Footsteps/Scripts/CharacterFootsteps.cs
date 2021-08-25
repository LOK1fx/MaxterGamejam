using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.LOK1game.recode.Player
{
    public class CharacterFootsteps : PlayerAbilityBase
    {
        [SerializeField] private GameObject _footstepPrefab;

        private Player _player;

        private Vector3 _lastFootstepPos;
        private Vector3 _lastCameraShakeDir;

        private void Start()
        {
            _player = GetComponent<Player>();

            _player.OnLand += OnPlayerLand;

            _lastFootstepPos = transform.position;
        }

        private void Update()
        {
            if(Vector3.Distance(transform.position, _lastFootstepPos) > 2.5f && PlayerState.onGround && !PlayerState.sliding)
            {
                Footstep();
            }
        }

        private void Footstep()
        {
            _lastFootstepPos = transform.position;

            var pos = _lastFootstepPos += transform.right * Random.Range(-0.5f, 0.5f);

            //Create footstep
            var newStep = Instantiate(_footstepPrefab, pos, transform.rotation, null);

            ShakeCamera();

            Destroy(newStep.gameObject, 1);
        }

        private void ShakeCamera()
        {
            var rightOffset = Random.Range(-1, 1);

            if(rightOffset == 0)
            {
                rightOffset = 1;
            }

            if (Vector3.right * rightOffset == _lastCameraShakeDir)
            {
                rightOffset = -rightOffset;
            }

            _lastCameraShakeDir = Vector3.right * rightOffset;

            MoveCamera.Instance.lerpOffset += Vector3.down * -0.08f;
            MoveCamera.Instance.lerpOffset += transform.right * (rightOffset * 0.08f);
        }

        private void OnPlayerLand()
        {
            Footstep();
        }

        private void OnDestroy()
        {
            _player.OnLand -= OnPlayerLand;
        }
    }
}