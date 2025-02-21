using UnityEngine;

namespace SRV.BlastSnipe.Player.Gun {
    [CreateAssetMenu(fileName = "Gun", menuName ="SRV/Gun", order = 1)]
    public class GunImprovement : ScriptableObject {
        [SerializeField]
        private int _ammo;

        [SerializeField]
        private int _damage;

        public int Ammo => _ammo;
        public int Damage => _damage;
    }
}