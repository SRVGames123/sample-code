using SRV.BlastSnipe.Core;
using UnityEngine;
// ммм, зачем я написал этот скрипт
namespace SRV.BlastSnipe.Sound {
    public class SoundService : Singleton<SoundService> {
        private float _volumeMusic;

        private float _volumeSound;

        public AudioSource Music {
            get;
            set;
        }
        public AudioSource Sound {
            get;
            set;
        }

        protected override void OnInit() {
            
        }
    }
}