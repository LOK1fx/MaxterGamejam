using com.LOK1game.recode;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.LOK1game.MaxterGamejam
{
    public class EnemyCrystal : MonoBehaviour
    {
        [SerializeField] private LayerMask _groundMask;
        [SerializeField] private GameObject _previewPrefab;
        [SerializeField] private GameObject _prefab;

        private Vector3[] _cheksDirections;
        private GameObject _currentPreview;

        private void Awake()
        {
            _cheksDirections = new Vector3[6]
            {
                Vector3.up,
                Vector3.down,
                Vector3.right,
                Vector3.left,
                Vector3.forward,
                Vector3.back
            };

            Destroy(gameObject, 8f);
        }

        public void Spawn(bool preview)
        {
            foreach (var dir in _cheksDirections)
            {
                if (Physics.Raycast(transform.position, dir, out RaycastHit hit, 4f, _groundMask, QueryTriggerInteraction.Ignore))
                {
                    transform.position = hit.point;

                    transform.LookAt(hit.point + hit.normal);
                    transform.SetParent(hit.transform, true);

                    if(preview)
                    {
                        _currentPreview = Instantiate(_previewPrefab, transform);
                    }
                    else
                    {
                        if(_currentPreview != null)
                        {
                            Destroy(_currentPreview);
                        }

                        Instantiate(_prefab, transform);
                    }
                }
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            
        }
    }
}