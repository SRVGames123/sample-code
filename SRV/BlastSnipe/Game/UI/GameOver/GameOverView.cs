using SRV.BlastSnipe.Main;
using UnityEngine.UI;
using UnityEngine;
using System;

namespace SRV.BlastSnipe.Game.UI.GameOver {
    public class GameOverView : TabView {
        [Header("GameOver")]
        [SerializeField]
        private Text _levelName;

        [SerializeField]
        private Text _coins;

        [SerializeField]
        private Text _time;

        [SerializeField]
        private Button _exit;

        [SerializeField]
        private Button _retry;

        public Action ExitHandler {
            get;
            private set;
        }

        public Action RetryHandler {
            get;
            private set;
        }

        protected override void OnInit() {
            _exit.onClick.AddListener(delegate {
                Hide();
                ExitHandler.Invoke();
            });
            _retry.onClick.AddListener(delegate {
                Hide();
                RetryHandler.Invoke();
            });
        }

        public void Init(string level, string time, int coins, Action exit, Action retry) {
            _levelName.text = $"Level: {level}";
            _time.text = $"Time: {time}";
            _coins.text = $"Coins {coins}";

            ExitHandler = exit;
            RetryHandler = retry;
        }
    }
}