using SRV.BlastSnipe.Game;
using SRV.BlastSnipe.Player.Gun;
using UnityEngine;

namespace SRV.BlastSnipe.Player {
    public class WeaponSpawn : MonoBehaviour {
        public Weapon Weapon {
            get;
            set;
        }

        private void Awake() {
            var hand = GameObject.Find("mixamorig:RightHand");

            var spawned = Instantiate(Weapon, hand.transform);
            FindAnyObjectByType<GameHelper>().Weapon = spawned;
        }
    }
}