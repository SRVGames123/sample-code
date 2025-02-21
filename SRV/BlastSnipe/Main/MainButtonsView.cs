using SRV.BlastSnipe.Game;
using SRV.BlastSnipe.Main.Levels;
using SRV.BlastSnipe.Main.Settings;
using SRV.BlastSnipe.Main.Shop;
using SRV.BlastSnipe.UI;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

namespace SRV.BlastSnipe.Main {
    public class MainButtonsView : View {
        [SerializeField]
        private Button _start;

        [SerializeField]
        private Button _levels;

        [SerializeField]
        private Button _shop;

        [SerializeField]
        private Button _settings;

        [SerializeField]
        private LevelsTabView _levelsTabView;

        [SerializeField]
        private ShopTabView _shopTabView;

        [SerializeField]
        private SettingsTabView _settingsTabView;

        [SerializeField]
        private GameHelper _gameHelper;

        public override void Show() {
            base.Show();
            _gameHelper.Hide();
        }

        private void Awake() {
            _start.onClick.AddListener(delegate {
                _gameHelper.StartLastLevel();
                Hide();
            });

            _levels.onClick.AddListener(_levelsTabView.Show);
            _shop.onClick.AddListener(_shopTabView.Show);
            _settings.onClick.AddListener(_settingsTabView.Show);
        }
    }
}