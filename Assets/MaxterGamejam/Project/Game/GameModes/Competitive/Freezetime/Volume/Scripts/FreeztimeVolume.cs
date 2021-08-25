using UnityEngine;
using UnityEngine.Rendering;

namespace com.LOK1game.recode
{
    /// <summary>
    /// Пост процессинг до начала раунда(freezetime)
    /// </summary>
    public class FreeztimeVolume : MonoBehaviour, IRoundActor
    {
        //private Volume _volume; //only on HDRP

        private bool _active;

        private void Start()
        {
            //_volume = GetComponent<Volume>();
            _active = false;
        }

        private void Update()
        {
            if(!_active) { return; }

            //_volume.weight = Mathf.Lerp(_volume.weight, 0f, Time.deltaTime * 0.1f);
        }

        public void OnFreezeTimeEnd()
        {
            SetVolumeActive(false);
        }

        public void OnRoundEnd()
        {
            
        }

        public void OnRoundStart()
        {
            SetVolumeActive(true);

            //_volume.weight = 1f;
        }

        private void SetVolumeActive(bool active)
        {
            gameObject.SetActive(active);
            _active = active;
        }
    }
}