using SRV.BlastSnipe.UI;
using SRV.BlastSnipe.UI.Dialogs;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SRV.BlastSnipe.Main.Shop {
    public class ShopItemView : View, IPointerClickHandler {
        [SerializeField]
        private Text _name;

        [SerializeField]
        private Text _description;

        [SerializeField]
        private GameObject _claimedMarker;

        [SerializeField]
        private GameObject _bloked;

        [SerializeField]
        private Text _price;

        [SerializeField]
        private Button _buy;

        private string _nameUzbek;

        private int _coins;

        private bool _isBuy;

        public Action<string, bool> ClickHandler {
            get;
            set;
        }

        public void Init(string name, string description, bool isClaimed, int price, string nameUzbek, bool isBuy) {
            _name.text = name;
            _description.text = description;
            _nameUzbek = nameUzbek;

            _claimedMarker.SetActive(isClaimed);
            _bloked.SetActive(isBuy ? false : true);

            _isBuy = isBuy;

            _price.text = price.ToString();
            _buy.onClick.AddListener(Buy);
        }

        private async void Buy() {
            var buttonYes = new DialogButton("Yes");
            var buttonNo = new DialogButton("No");
            Dialog dialog = Dialogs.Create($"Buy", $"Are you sure you want to buy this item? {_name.text}", buttonNo, buttonYes);
            dialog.Show();
            dialog.Show(delegate (DialogButton button) {
                if (button == buttonYes) {
                    var coins = PlayerPrefs.GetInt("Coins");
                    if(coins >= _coins) {
                        PlayerPrefs.SetInt("Coins", coins -= _coins);
                        _bloked.SetActive(false);
                        PlayerPrefs.SetString(_nameUzbek, _nameUzbek);
                        dialog.Hide();
                        _isBuy = true;
                    }
                    else {
                        var buttonOk = new DialogButton("Ok");
                        Dialog dialog = Dialogs.Create($"Buy", $"You don't have enough {_coins -= coins} coins", buttonOk);
                        dialog.Show();
                        dialog.Show(delegate (DialogButton button) {
                            if (button == buttonOk) {
                                dialog.Hide();
                            }
                        });
                    }
                }
                if (button == buttonNo) {
                    dialog.Hide();
                }
            });
        }

        public void OnPointerClick(PointerEventData eventData) => ClickHandler?.Invoke(_nameUzbek, _isBuy);
    }
}