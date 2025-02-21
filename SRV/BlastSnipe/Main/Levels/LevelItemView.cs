using SRV.BlastSnipe.UI;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SRV.BlastSnipe.Main.Levels {
    public class LevelItemView : View, IPointerClickHandler {
        [SerializeField]
        private Text _nameLevel;

        [SerializeField]
        private Text _descriptionLevel;

        [SerializeField]
        private GameObject _bloked;

        [SerializeField]
        private Text _coinsLevel;

        [SerializeField]
        private GameObject _claimedMarker;

        private Level _level;

        public Action<Level> OpenLevel {
            get;
            private set;
        }

        public void OnPointerClick(PointerEventData eventData) => OpenLevel.Invoke(_level);

        public void SetUp(string nameLevel, string descriptionLevel, bool bloked, int coinsLevel, bool claimedMarker, Action<Level> openLevel, Level level) {
            _nameLevel.text = nameLevel;
            _descriptionLevel.text = descriptionLevel;
            _coinsLevel.text = coinsLevel.ToString();

            _claimedMarker.SetActive(claimedMarker);
            _bloked.SetActive(bloked);
            _coinsLevel.enabled = !claimedMarker;

            OpenLevel = openLevel;
            _level = level;
        }
    }
}
 