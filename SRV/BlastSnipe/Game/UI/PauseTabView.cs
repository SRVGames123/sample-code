using SRV.BlastSnipe.Main;
using SRV.BlastSnipe.Main.Settings;
using SRV.BlastSnipe.UI.Dialogs;
using UnityEngine;
using UnityEngine.UI;

namespace SRV.BlastSnipe.Game.UI {
    public class PauseTabView : TabView {
        [Header("Pause")]
        [SerializeField]
        private Button _continue;

        [SerializeField]
        private Button _retry;

        [SerializeField]
        private Button _settings;

        [SerializeField]
        private Button _exit;

        [SerializeField]
        private SettingsTabView _settingsTabView;

        [SerializeField]
        private GameHelper _gameHelper;

        protected override void OnInit() {
            _continue.onClick.AddListener(Hide);
            _retry.onClick.AddListener(delegate {
                Hide();
                var buttonYes = new DialogButton("Yes");
                var buttonNo = new DialogButton("No");
                Dialog dialog = Dialogs.Create("Exit", "Are you sure you want to go out? all your achievements will not be saved", buttonNo, buttonYes);
                dialog.Show();
                dialog.Show(delegate (DialogButton button) {
                    if (button == buttonYes) {
                        Hide();
                        _gameHelper.Retry();
                    }
                    if (button == buttonNo) {
                        Show();
                        dialog.Hide();
                    }
                    dialog.Hide();
                });
            });
            _settings.onClick.AddListener(delegate {
                Hide();
                _settingsTabView.Show();
                _settingsTabView.HideHandler = Show;
            });
            _exit.onClick.AddListener(delegate {
                Hide();
                _gameHelper.Exit();
            });
        }

       /* public override void Show() {
            base.Show();
            Time.timeScale = 0f; animation pridet pizdec
        }*/
    }
}