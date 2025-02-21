using JetBrains.Annotations;
using System;
using UnityEngine;

namespace SRV.BlastSnipe.UI {
    // я рот того ебал, кто не юзает пул
    public class ViewPool<T> where T : View {
        private readonly T[] _pool;

        private int _iteratorIndex;

        public T[] Items {
            get;
            private set;
        } = new T[0];


        public int Count {
            get;
            private set;
        }

        public ViewPool([NotNull] T viewPrefab, int count) {
            if (viewPrefab == null) {
                throw new ArgumentNullException("viewPrefab");
            }
            _pool = new T[count];
            Count = count;
            int num2;
            for (int num = 0; num < count; num = num2 + 1) {
                T val = UnityEngine.Object.Instantiate(viewPrefab, viewPrefab.transform.parent, worldPositionStays: false);
                Hide(val);
                _pool[num] = val;
                num2 = num;
            }
            Hide(viewPrefab);
        }

        public T[] GetItems(int lenght) {
            if (lenght > _pool.Length) {
                throw new IndexOutOfRangeException("Pool size exceeded, can't return " + lenght + " elements, pool size is " + _pool.Length);
            }
            _iteratorIndex = 0;
            Items = new T[lenght];
            int num;
            int num2;
            for (num = 0; num < lenght; num = num2 + 1) {
                Items[num] = _pool[num];
                Show(Items[num]);
                num2 = num;
            }
            while (num < _pool.Length) {
                T[] pool = _pool;
                num2 = num;
                num = num2 + 1;
                Hide(pool[num2]);
            }
            return Items;
        }

        public void Clear() {
            GetItems(0);
        }

        protected virtual void Show(T view) {
            view.Show();
        }

        protected virtual void Hide(T view) {
            view.Hide();
        }

        public void BeginIteration() {
            _iteratorIndex = 0;
        }

        public T GetNext() {
            if (!HasNext()) {
                throw new IndexOutOfRangeException($"Pool size exceeded, can't return {_iteratorIndex} element, pool size is {_pool.Length}");
            }
            T val = _pool[_iteratorIndex++];
            Show(val);
            return val;
        }

        public bool HasNext() {
            return _iteratorIndex < _pool.Length;
        }

        public void EndIteration() {
            int num2;
            for (int num = _iteratorIndex; num < _pool.Length; num = num2 + 1) {
                Hide(_pool[num]);
                num2 = num;
            }
        }
    }
}
