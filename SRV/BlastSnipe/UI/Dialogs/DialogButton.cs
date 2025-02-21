using JetBrains.Annotations;
using System;

namespace SRV.BlastSnipe.UI.Dialogs {
    public class DialogButton {
        public virtual string Name {
            get;
        }

        public DialogButton([NotNull] string name) {
            if (name == null)
                throw new ArgumentNullException("name");
            Name = name;
        }

        protected DialogButton() {
        }
    }
}
