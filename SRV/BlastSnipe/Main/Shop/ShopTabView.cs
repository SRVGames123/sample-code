using SRV.BlastSnipe.UI;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SRV.BlastSnipe.Main.Shop {
    public class ShopTabView : TabView {
        [Header("Shop")]
        [SerializeField]
        private ShopItemView _shopItemView;

        [SerializeField]
        private List<ShopItem> _characters;

        [SerializeField]
        private CharacterShopView _characterShopView;

        private ViewPool<ShopItemView> _character;

        protected override void OnInit() {
            if (!PlayerPrefs.HasKey("Amazon")) {
                PlayerPrefs.SetInt("Amazon", 1); // типо первый узбек (персонаж)
                PlayerPrefs.SetString("SelectCharacter", "Amazon");
            }
            if (_character == null)
                _character = new ViewPool<ShopItemView>(_shopItemView, _characters.Count);

            ShopItemView[] characterItems = _character.GetItems(_characters.Count);

            for (int i = 0; i < characterItems.Length; i++) {
                characterItems[i].Init(_characters[i].Name,
                    _characters[i].Description,
                    PlayerPrefs.GetString("SelectCharacter") == _characters[i].Name,
                    _characters[i].Price,
                    _characters[i].NameUzbek,
                    PlayerPrefs.HasKey(_characters[i].Name)
                    );
                characterItems[i].Show();
                characterItems[i].ClickHandler = SetCharacter;
            }
            _characterShopView.Show(PlayerPrefs.GetString("SelectCharacter"));
        }

        private void SetCharacter(string name, bool isBuy) {
            _characterShopView.Show(name);
            if (isBuy) {
                PlayerPrefs.SetString("SelectCharacter", name);
                OnInit();
            }
        }
    }
}