using SRV.BlastSnipe.UI;
using UnityEngine;

namespace SRV.BlastSnipe.Main {
    public class Main : View {
        [SerializeField]
        private SplashScreen _splashScreen;

        private void Awake() {
            //PlayerPrefs.DeleteAll(); game testing
            PlayerPrefs.SetInt("Coins", 12313);
            _splashScreen.Show();
        }
    }
}