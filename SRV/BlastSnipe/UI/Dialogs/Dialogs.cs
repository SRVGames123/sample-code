using JetBrains.Annotations;
using System;
using UnityEngine;

namespace SRV.BlastSnipe.UI.Dialogs {
    public class Dialogs : MonoBehaviour {
        private class OkButton : DialogButton {
            public string Name => "Ok";
        }

        private class CancelButton : DialogButton {
            public string Name => "Cancel";
        }

        private static Dialogs _instance;

        public static readonly DialogButton Ok = new OkButton();

        public static readonly DialogButton Cancel = new CancelButton();

        [SerializeField]
        private Dialog _dialogPrefab;

        private static Dialogs Instance {
            get {
                if (_instance == null) {
                    GameObject original = Resources.Load<GameObject>("UI/Dialogs");
                    _instance = Instantiate(original, Vector3.zero, Quaternion.identity).GetComponent<Dialogs>();
                }
                return _instance;
            }
        }

        public static Dialog Create() {
            return Instance.CreateDialog();
        }

        public static Dialog Create(string title, string contentText, params DialogButton[] buttons) {
            Dialog dialog = Create();
            dialog.Title = title;
            dialog.ContentText = contentText;
            dialog.AddButtons(buttons);
            return dialog;
        }

        public static Dialog Create(string title, Sprite content, params DialogButton[] buttons) {
            Dialog dialog = Create();
            dialog.Title = title;
            dialog.ContentSprite = content;
            dialog.AddButtons(buttons);
            return dialog;
        }

        public static Dialog Message([NotNull] string title, [NotNull] string message, Action okAction = null) {
            if (title == null) {
                throw new ArgumentNullException("title");
            }
            if (message == null) {
                throw new ArgumentNullException("message");
            }
            Dialog dialog = Create(title, message, new DialogButton("Ok"));
            dialog.Show(delegate {
                okAction?.Invoke();
            });
            return dialog;
        }

        public static Dialog Message([NotNull] string title, [NotNull] string message, string label, Action okAction = null) {
            if (title == null) {
                throw new ArgumentNullException("title");
            }
            if (message == null) {
                throw new ArgumentNullException("message");
            }
            Dialog dialog = Create(title, message, new DialogButton(label));
            dialog.Show(delegate {
                okAction?.Invoke();
            });
            return dialog;
        }

        public static Dialog Confirm([NotNull] string title, [NotNull] string message) {
            return Create(title, message, Ok, Cancel);
        }

        public static Dialog Wait([NotNull] string title, [NotNull] string message) {
            Dialog dialog = Create(title, message);
            dialog.Show();
            return dialog;
        }

        private void Awake() {
            _dialogPrefab.gameObject.SetActive(value: false);
        }

        private Dialog CreateDialog() {
            Dialog dialog = UnityEngine.Object.Instantiate(_dialogPrefab, base.transform, worldPositionStays: false);
            dialog.gameObject.SetActive(value: true);
            dialog.gameObject.SetActive(value: false);
            return dialog;
        }

        private void OnDestroy() {
            _instance = null;
        }
    }
}
