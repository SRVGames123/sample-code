using UnityEngine;

namespace SRV.BlastSnipe.Main.Shop {
    [CreateAssetMenu(fileName = "Pon", menuName = "SRV/ShopItem", order = 1)]
    public class ShopItem : ScriptableObject {
        [SerializeField]
        private string _name;

        [SerializeField]
        private string _description;

        [SerializeField]
        private int _price;

        [SerializeField]
        private string _nameUzbek;

        public string Name => _name;
        public string Description => _description;
        public int Price => _price;
        public string NameUzbek => _nameUzbek;
    }
}