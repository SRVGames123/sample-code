using System;
using UnityEngine;
using UnityEngine.UI;

namespace SRV.BlastSnipe.Enemy {
    public class EnemyPonos : MonoBehaviour {
        [SerializeField]
        private EnemyHealth _enemyHealth;

        [SerializeField]
        private Animator _enemy;

        [SerializeField]
        private Image _health;

        public void Init(Action kill, string trigger) {
            _enemyHealth = gameObject.AddComponent<EnemyHealth>();
            _enemyHealth.Init(100, kill, _health);
            _enemy.SetTrigger(trigger);
        }
    }
}