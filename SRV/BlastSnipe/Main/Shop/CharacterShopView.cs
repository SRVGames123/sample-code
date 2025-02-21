using SRV.BlastSnipe.UI;
using UnityEngine;

namespace SRV.BlastSnipe.Main.Shop {
    // ”«¡≈ »
    public class CharacterShopView : View {
        [SerializeField]
        private Transform _contentSpawn;

        private Animator _lastUzbek;

        public void Show(string nameUzbeka) {
            if(_lastUzbek != null)
                Destroy(_lastUzbek.gameObject);

            var uzbek = Resources.Load<Animator>($"CharacterInspect/{nameUzbeka}");
            var spawnedUzbek = Instantiate(uzbek, _contentSpawn);

           // spawnedUzbek.transform.position = new Vect;
          //  spawnedUzbek.transform.rotation = new Quaternion(0f, 90f, 0f, 0f);
            _lastUzbek = spawnedUzbek;
        }
    }
}