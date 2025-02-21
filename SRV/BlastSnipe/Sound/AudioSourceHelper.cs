using SRV.BlastSnipe.Core;
using UnityEngine;

namespace SRV.BlastSnipe.Sound {
    [RequireComponent(typeof(AudioSource))]
    public class AudioSourceHelper : MonoBehaviour {
        [SerializeField]
        private bool _isMusic;

        private AudioSource _audioSource;

        public AudioSource AudioSource => _audioSource;

        private void Awake() {
            _audioSource = GetComponent<AudioSource>();

            if (!Singleton<SoundService>.IsInitialized())
                Singleton<SoundService>.Init();

            if (_isMusic)
                Singleton<SoundService>.Instance.Music = _audioSource;
            else if (!_isMusic)
                Singleton<SoundService>.Instance.Sound = _audioSource;
        }
    }
}