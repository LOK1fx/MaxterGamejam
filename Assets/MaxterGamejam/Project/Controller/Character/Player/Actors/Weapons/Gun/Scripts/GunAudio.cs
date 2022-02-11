using LOK1game.Game.Events;
using UnityEngine;

namespace com.LOK1game.MaxterGamejam
{
    [RequireComponent(typeof(AudioSource))]
    public class GunAudio : MonoBehaviour
    {
        private AudioSource _audio;

        private GunProperties _gunData;
        private Gun _gun;

        private void Start()
        {
            _audio = GetComponent<AudioSource>();
            _gun = GetComponent<Gun>();

            _gun.OnShoot += OnGunShoot;

            _gunData = _gun.GetGunProperties();

            EventManager.AddListener<OnLevelStartChangeEvent>(UnbindEvents);
        }

        private void OnGunShoot()
        {
            var pitch = Random.Range(0.9f, 1.1f);

            _audio.pitch = pitch;
            _audio.PlayOneShot(_gunData.ShootSound);
        }

        private void OnDestroy()
        {
            _gun.OnShoot -= OnGunShoot;
        }

        private void UnbindEvents(OnLevelStartChangeEvent evt)
        {
            _gun.OnShoot -= OnGunShoot;
        }
    }
}