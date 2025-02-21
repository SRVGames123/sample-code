using System;
using UnityEngine;
using UnityEngine.UI;

namespace SRV.BlastSnipe.Enemy {
    // понос
    public class EnemyHealth : MonoBehaviour {
        private int _health;

        private Image _image;

        public int Health => _health;

        public Action Die {
            get;
            set;
        }

        public void Init(int health, Action kill, Image image) {
            _health = health;
            Die = kill;
            _image = image;
            _image.fillAmount = 1;
            _image.color = Color.green;
        }

        public void TakeDamage(int damage) {
            _health -= damage;
            if (_health <= 0) {
                Die.Invoke();
               Destroy(gameObject);
            }
            var maxHealth = 100f; 
            var fillAmount = Mathf.Clamp01(_health / maxHealth); 
            _image.fillAmount = fillAmount;

            _image.color = _health >= 100f ? Color.green : (_health >= 60f ? Color.yellow : Color.red);
            Debug.Log($"Нанесено: {damage}, осталось: {Health}"); // пенис
        }
    }
}