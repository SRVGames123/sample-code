using SRV.BlastSnipe.UI;
using UnityEngine;
using UnityEngine.UI;

namespace SRV.BlastSnipe.Game.UI {
    public class GameCounter : View {
        [SerializeField]
        private Text _bullets;

        [Header("Heart")]
        [SerializeField]
        private Image _heart1, _heart2, _heart3;

        [SerializeField]
        private Sprite _iconLife;

        [SerializeField]
        private Sprite _iconGray;

        private int _bulletsCount;
        private int _grayHeart;

        public int Bullets {
            set {
                _bulletsCount = value;
                _bullets.text = $"Bullets: {value}";
            }
            get => _bulletsCount;
        }

        public int SetLife {
            set {
                switch (value) {
                    case 1:
                        _heart1.sprite = _iconLife;
                        break;
                    case 2:
                        _heart2.sprite = _iconLife;
                        break;
                    case 3:
                        _heart3.sprite = _iconLife;
                        break;
                }
            }
        }

        public int SetGray {
            set {
                _grayHeart = value;
                switch (value) {
                    case 1:
                        _heart3.sprite = _iconGray;
                        break;
                    case 2:
                        _heart2.sprite = _iconGray;
                        break;
                    case 3:
                        _heart1.sprite = _iconGray;
                        break;
                }
            }
            get => _grayHeart;
        }

        public void Init(int bullets, int health) {
            Bullets = bullets;
            for (int i = 1; i <= health; i++) {
                SetLife = i;
            }
        }
    }
}