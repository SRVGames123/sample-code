using System.Collections.Generic;
using SRV.BlastSnipe.Main;
using UnityEngine.UI;
using UnityEngine;
using System;

namespace SRV.BlastSnipe.Game.UI.Winner {
    public class WinTabView : TabView {
        [Header("Winner")]
        [SerializeField]
        private List<Sprite> _winners;

        [SerializeField]
        private Image _title;

        [SerializeField]
        private Text _levelName;

        [SerializeField]
        private Text _coins;

        [SerializeField]
        private Text _time;

        [SerializeField]
        private Button _exit;

        [SerializeField]
        private Button _nextLevel;

        public Action ExitHandler {
            get;
            private set;
        }

        public Action NextLevelHandler {
            get;
            private set;
        }

        protected override void OnInit() {
            _exit.onClick.AddListener(delegate {
                Hide();
                ExitHandler.Invoke();
            });
            _nextLevel.onClick.AddListener(delegate {
                Hide();
                NextLevelHandler.Invoke();
            });
        }

        public void Init(string level, string time, int coins, Action exit, Action nextLevel) {
            _levelName.text = $"Level: {level}";
            _time.text = $"Time: {time}";
            _coins.text = $"Coins: {coins}";

            ExitHandler = exit;
            NextLevelHandler = nextLevel;

            _title.sprite = _winners[UnityEngine.Random.Range(0, _winners.Count)];  
        }
    }
}
/*
 ⠺⠻⡄⢸⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠟⠻⠆⠀⠀⠀⢆
⡆⢀⠀⢸⡿⠯⢉⣴⣶⣌⣻⣿⣿⣿⣥⣾⣷⠀⠀⠀⡀⠈
⡇⢸⠀⢸⣿⣿⣿⣿⣿⣿⣿⣿⣿⡙⣿⣿⣿⠀⠀⢠⣧⠀⠀
⠸⣼⠀⠈⢿⣿⣿⣿⣿⣿⠿⠛⠛⠛⣿⣿⡿⠀⠀⣾⡿⢀⣼
⠀⢹⣆⠀⠀⠙⢿⣿⣿⣧⡘⠿⠟⣰⣿⠟⠁⠀⠐⣿⣿⣿⣿
⠀⠸⠿⣦⠀⢀⣶⣍⡛⠿⢿⣷⣾⠟⠉⠀⠀⠀⠀⠙⢿⣿⣿
⠀⠀⠁⠸⠀⢸⣿⣿⣿⣿⠇⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠘⢿
⠔⠃⠀⠀⠀⢸⣿⣿⣿⣿⣿⡇⠀⠠⣴⣶⣶⣦⣄⠀⠀⠀⡡⠘
⣿⣿⣿⣿⡇⠀⣿⣿⣿⣿⣿⣿⣿⣷⣆⠈⠻⣿⣿⡄⠀⠀⠀⣿
⣿⣿⣿⣿⣷⠀⠘⣿⣿⣿⣿⣿⣿⣿⣿⣷⡀⠈⠻⣇⠀⢀⣴⣿
⣿⣿⣿⣿⣿⣇⠀⠹⣿⣿⣿⣿⣿⣿⣿⣿⣷⣄⠀⠙⠀⣸⣿⣿
⣿⣿⣿⣿⣿⡏⡄⠀⠹⣿⣿⣿⣷⣜⢿⣿⣿⣿⣷⠀⠀⠙⢿⣿
⣿⣿⣿⣿⣿⣇⠿⠀⠀⠙⠛⠛⠿⣿⣧⡹⣿⣿⠃⠀⠀⠀⠀⠙
⠈⢿⣿⣿⣿⣿⣿⡄⠀⠀⠀⠀⠀⠀⠀⠙⠳⠸
*/