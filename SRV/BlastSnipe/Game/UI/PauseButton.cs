using UnityEngine;
using UnityEngine.UI;

namespace SRV.BlastSnipe.Game.UI {
    [RequireComponent(typeof(Button))]
    public class PauseButton : MonoBehaviour {
        [SerializeField]
        private PauseTabView _pauseTabView;

        private void Awake() {
            var button = GetComponent<Button>();
            button.onClick.AddListener(_pauseTabView.Show);
        }
    }
}