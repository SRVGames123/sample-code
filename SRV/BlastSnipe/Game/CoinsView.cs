using UnityEngine;
using UnityEngine.UI;

namespace SRV.BlastSnipe.Game {
    public class CoinsView : MonoBehaviour {
        [SerializeField]
        private Text _coins;

        private void Init() => _coins.text = PlayerPrefs.GetInt("Coins").ToString();

        private void OnEnable() => Init();

        private void OnDisable() => Init();
    }
}