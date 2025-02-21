using SRV.BlastSnipe.Enemy;
using SRV.BlastSnipe.Game.UI;
using SRV.BlastSnipe.Game.UI.GameOver;
using SRV.BlastSnipe.Game.UI.Winner;
using SRV.BlastSnipe.Main;
using SRV.BlastSnipe.Main.Levels;
using SRV.BlastSnipe.Player;
using SRV.BlastSnipe.Player.Gun;
using SRV.BlastSnipe.UI;
using SRV.BlastSnipe.UI.Dialogs;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SRV.BlastSnipe.Game {
    // mega ponos code
    public class GameHelper : View {
        [SerializeField]
        private GameCounter _gameCounter;

        [SerializeField]
        private Animator _startGame;

        [SerializeField]
        private SplashScreen _splashScreen;

        [SerializeField]
        private GameOverView _gameOverView;

        [SerializeField]
        private WinTabView _winTabView;

        [SerializeField]
        private Transform _map;

        [SerializeField]
        private LevelsTabView _levelsTabView;

        [SerializeField]
        private MainButtonsView _mainButtons;

        [SerializeField]
        private PlayerSkin _playerSkin;

        [SerializeField]
        private Button _shoot;

        [SerializeField]
        private GameObject _background;

        [SerializeField]
        private Text _countKill;

        private Level _level;

        private string _time;

        private float _startTime;

        private List<GameObject> _enemy = new List<GameObject>();

        private List<GameObject> _barrier = new List<GameObject>();

        private GameObject _player;

        private Vector3 _defPositionPlayer;

        private Quaternion _defRotationPlayer;

        private int _killCount;

        private bool _isGame;

        private bool _isWin;

        private int _enemyCount;

        public Weapon Weapon {
            get;
            set;
        }

        // я уже не помню зачем писал Action, если они не будут юзатся 
        // значит я забыл про них, мб вспомню. 
        // p.s не вспомнил, и удалил их нахуй

        public void StartLastLevel() => LoadingLevel(_levelsTabView.Levels[PlayerPrefs.GetInt("CurrentLevel")]);

        public void LoadingLevel(Level level) {
            try {
                _level = level;

                _startTime = Time.time;

                _isGame = true;

                _playerSkin.Init();
                Weapon = _playerSkin.Weapon;
                _player = _playerSkin.Player;

                _splashScreen.HideHandler = StartGame;
                _splashScreen.Show();
                _splashScreen.IsGame = true;

                _gameCounter.Init(level.Bullets, 3);

                Weapon.Init(level.Bullets, level.Damage, GameOver, Shoot, SetScoreKill, SetScoreBarrier);

                _shoot.onClick.AddListener(Weapon.Shoot);

                var settingsBarrier = level.SettingsBarriers;
                for (int i = 0; i < settingsBarrier.Count; i++) {
                    var spawnedBarrier = Instantiate(settingsBarrier[i].Barrier, _map);
                    spawnedBarrier.transform.localPosition = settingsBarrier[i].Position;
                    spawnedBarrier.transform.localRotation = settingsBarrier[i].Rotation;
                    _barrier.Add(spawnedBarrier);
                    Debug.Log($"Position: {spawnedBarrier.transform.position}," +
                        $"Rotation: {spawnedBarrier.transform.rotation}");
                }

                var enemyData = level.Enemy;
                for (int i = 0; i < enemyData.Count; i++) {
                    var spawnedEnemy = Instantiate(enemyData[i].Enemy,
                        _map);
                    spawnedEnemy.transform.localPosition = enemyData[i].Position;
                    spawnedEnemy.transform.localRotation = enemyData[i].Rotation;
                    _enemy.Add(spawnedEnemy);
                    Debug.Log($"Position: {enemyData[i].Position}," +
                        $"Rotation: {enemyData[i].Rotation}");
                    _enemy[i].GetComponent<EnemyPonos>().Init(SetScoreKill, level.Enemy[i].TriggerAnimator);
                }

                _enemyCount = _enemy.Count;
                _enemyCount++;
                var pon = _enemyCount -= _killCount;
                _countKill.text = $"Alive: {pon} Killed: {_killCount}";
            }
            catch(Exception ex) {
                Debug.LogException(ex);
            }
        }

        private IEnumerator UpdateTime() {
            while (_isGame) {
                var currentTime = Time.time;
                var elapsedTime = currentTime - _startTime;
                var minutes = Mathf.Floor(elapsedTime / 60).ToString("00");
                var seconds = Mathf.Floor(elapsedTime % 60).ToString("00");
                _time = $"{minutes}:{seconds}";
                yield return new WaitForSeconds(1f);
            }
        }

        private void StartGame() {
            _background.SetActive(false);
            StartCoroutine(AnimShow());
            StartCoroutine(UpdateTime());
        }

        public IEnumerator AnimShow() {
            var aniumator = GetComponent<Animator>();
            aniumator.enabled = false;
            yield return new WaitForSeconds(0.5f);
            _startGame.gameObject.SetActive(true);
            _startGame.SetTrigger("StartGame");
            yield return new WaitForSeconds(2.5f);
            _startGame.gameObject.SetActive(false);
            aniumator.enabled = true;
            yield return null;
        }

        public void SetScoreKill() {
            _killCount++;
            var pon = _enemyCount -= _killCount;
            _countKill.text = $"Alive: {pon} Killed: {_killCount}";
            if (_killCount == _level.Enemy.Count)
                EndGame();
        }

        //КТО ДВИНЕТСЯ ТОТ ГЕЙ
        public void SetScoreBarrier() {
            _gameCounter.SetGray += 1;
            if(_gameCounter.SetGray == 3)
                GameOver();
        }

        public void Shoot() {
            if (_gameCounter.Bullets >= 0) {
                _player.GetComponent<Animator>().SetTrigger("Shoot");
                _gameCounter.Bullets -= 1;
            }
            else
                GameOver();
        }

        private void GameOver() {
            Hide();

            _isWin = false;

            _gameOverView.Show();
            _gameOverView.Init(_level.NameLevel, _time, 0, Exit, Retry);
            ResetAll();
        }

        public void Exit() {
            var buttonYes = new DialogButton("Yes");
            var buttonNo = new DialogButton("No");

            Dialog dialog = Dialogs.Create("Exit", "Are you sure you want to go to the main menu?", buttonNo, buttonYes);
            dialog.Show();
            dialog.Show(delegate (DialogButton button) {
                if (button == buttonYes) {
                    Hide();
                    ResetAll();
                    _background.SetActive(true);
                    _splashScreen.Show();

                    _splashScreen.HideHandler = delegate {
                        _mainButtons.Show();
                        _splashScreen.HideHandler = null;
                    };
                }
                if (button == buttonNo) {
                    _background.SetActive(false);
                    Show();
                    dialog.Hide();
                }
                dialog.Hide();
            });
        }

        private void ResetAll() {
            for(int i = 0; i< _enemy.Count; i++) {
                Destroy(_enemy[i]);
            }
            for (int i = 0; i < _barrier.Count; i++) {
                Destroy(_barrier[i]);
            }

            _enemy = new List<GameObject>();
            _barrier = new List<GameObject>();

            // НЕ УБИРАТЬ, я забыл зачем оно, и удалил, и проект по пизде пошел
            _player.SetActive(false); 

            StopCoroutine(UpdateTime());

            PlayerPrefs.SetInt("Coins", _isWin ? _level.CoinsLevel : 0);

            _startTime = 0f;
            _time = "";
        }

       public void Retry() {
            ResetAll();
            Hide();
            StartLastLevel();
       }

        private void NextLevel() => StartLastLevel();

        private void EndGame() {
            if(_level.NameLevel != "Level 5") {
                var currentLevel = PlayerPrefs.GetInt("CurrentLevel");
                currentLevel++;
                PlayerPrefs.SetInt("CurrentLevel", currentLevel);
            }
            PlayerPrefs.SetString(_level.NameLevel, _level.NameLevel);
            PlayerPrefs.SetString($"IsClaimed_{_level.NameLevel}", _level.NameLevel);

            _isWin = true;

            Hide();
            _winTabView.Show();
            _winTabView.Init(_level.NameLevel, _time, _level.CoinsLevel, Exit, NextLevel);
            ResetAll();
        }
    }
}