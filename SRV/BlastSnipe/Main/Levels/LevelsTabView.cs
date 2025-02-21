using SRV.BlastSnipe.Enemy;
using SRV.BlastSnipe.Game;
using SRV.BlastSnipe.UI;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace SRV.BlastSnipe.Main.Levels {
    [Serializable]
    public class Level {
        [SerializeField]
        private string _nameLevel;

        [SerializeField]
        private string _descriptionLevel;

        [SerializeField]
        private int _coinsLevel;

        [SerializeField]
        private int _bullets;

        [SerializeField]
        private int _damage;

        [SerializeField]
        private List<EnemyData> _enemy;

        [SerializeField]
        private List<SettingsBarrier> _settingsBarriers;

        public string NameLevel => _nameLevel;
        public string DescriptionLevel => _descriptionLevel;
        public int CoinsLevel => _coinsLevel;
        public int Bullets => _bullets;
        public int Damage => _damage;
        public List<EnemyData> Enemy => _enemy;
        public List<SettingsBarrier> SettingsBarriers => _settingsBarriers;
    }

    [Serializable]
    public class EnemyData {
        [SerializeField]
        private GameObject _enemy;

        [SerializeField]
        private Vector3 _position;

        [SerializeField]
        private Quaternion _rotation;

        [SerializeField]
        private string _triggerAnimator;

        public GameObject Enemy => _enemy;
        public Vector3 Position => _position;
        public Quaternion Rotation => _rotation;
        public string TriggerAnimator => _triggerAnimator;
    }

    [Serializable]
    public class SettingsBarrier {
        [SerializeField]
        private GameObject _barrier;

        [SerializeField]
        private Vector3 _position;

        [SerializeField]
        private Quaternion _rotation;

        public GameObject Barrier => _barrier;
        public Vector3 Position => _position;
        public Quaternion Rotation => _rotation;
    }

    public class LevelsTabView : TabView {
        [SerializeField]
        private LevelItemView _levelItemView;

        [SerializeField]
        private List<Level> _levels;

        [SerializeField]
        private GameHelper _gameHelper;

        [SerializeField]
        private MainButtonsView _mainButtonsView;

        private ViewPool<LevelItemView> _viewPool;

        public List<Level> Levels => _levels;

        protected override void OnInit() {
            if (PlayerPrefs.GetInt("CurrentLevel") == 0)
                PlayerPrefs.SetInt("CurrentLevel", 1);

            _levelItemView.Hide();
            if (_viewPool == null)
                _viewPool = new ViewPool<LevelItemView>(_levelItemView, _levels.Count);

            LevelItemView[] items = _viewPool.GetItems(_levels.Count);
            for(int i = 0; i < items.Length; i++) {
                items[i].SetUp(_levels[i].NameLevel, _levels[i].DescriptionLevel,
                    IsBlokedLevel(i),
                    _levels[i].CoinsLevel,
                    IsClaimedLevel(_levels[i].NameLevel),
                    LoadLevel,
                    _levels[i]);
                items[i].Show();
            }
        }

        public override void Show() {
            base.Show();
            OnInit();
        }

        private void LoadLevel(Level level) {
            _mainButtonsView.Hide();
            Hide();
            _gameHelper.LoadingLevel(level);
        }

        private bool IsClaimedLevel(string nameLevel) => PlayerPrefs.HasKey($"IsClaimed_{nameLevel}");

        private bool IsBlokedLevel(int value) => PlayerPrefs.GetInt("CurrentLevel") > value ? false : true;
    }
}