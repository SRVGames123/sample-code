using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace SRV.BlastSnipe.Main {
    public abstract class TabView : MonoBehaviour {
        private const string ShowTrigger = "Show";

        private const string HideTrigger = "Hide";

        [Header("Animation")]
        [SerializeField]
        private Animator _animator;

        [SerializeField]
        private Animator _lastAnimator;

        [SerializeField]
        private Button _close;

        public Action HideHandler {
            get;
            set;
        }

        private void Awake() {
            _close.onClick.AddListener(Hide);
            OnInit();
        }

        public virtual void Show() {
            gameObject.SetActive(true);
            StartCoroutine(AnimShow());
        }

        public IEnumerator AnimShow() {
            _lastAnimator.SetTrigger(HideTrigger);
            yield return new WaitForSeconds(0.2f);
            _animator.SetTrigger(ShowTrigger);
            yield return null;
        }

        public IEnumerator AnimHide() {
            _animator.SetTrigger(HideTrigger);
            yield return new WaitForSeconds(0.2f);
            _lastAnimator.SetTrigger(ShowTrigger);
            yield return new WaitForSeconds(0.2f);
            gameObject.SetActive(false);
            yield return null;
        }

        public virtual void Hide() {
            StartCoroutine(AnimHide());
            HideHandler?.Invoke();
        }

        protected abstract void OnInit();
    }
}