using SRV.BlastSnipe.Game;
using SRV.BlastSnipe.Player.Gun;
using StarterAssets;
using System.Linq;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

namespace SRV.BlastSnipe.Player {
    public class PlayerSkin : MonoBehaviour {
        [SerializeField]
        private Transform _spawn;

        [SerializeField]
        private FirstPersonController _fpc;

        private Animator _last;

        public Weapon Weapon {
            get;
            private set;
        }

        public GameObject Player {
            get;
            private set;
        }

        public void Init() {
            if (_last != null) {
                Destroy(_last.gameObject);
                Debug.Log("Delete");
            }

            var uzbek = Resources.Load<Animator>($"Character/{PlayerPrefs.GetString("SelectCharacter")}");
            _last = Instantiate(uzbek, _spawn);

            _fpc._uzbek = _last;

            var weapon = Resources.Load<Weapon>("m16");
            GameObject hand = null;
         //   transform.GetComponentsInChildren<Transform>()
                                         // .Select(t => t.gameObject)
                                         // .FirstOrDefault(go => go.name == "хЛЪ_ДНВЕПМЕЦН_НАЗЕЙРЮ");
            Transform[] children = _last.transform.GetComponentsInChildren<Transform>();
            foreach (Transform child in children) {
                if (child.name == "mixamorig:RightHand") {
                    hand = child.gameObject;
                }
            }

            Weapon = Instantiate(weapon, hand.transform);
            Player = _last.gameObject;
            Debug.LogError("охгдеж опнейр он охгде оньек, бяе охгдю");
            // дю ъ ф оньсрхк
        }
    }
}