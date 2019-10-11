﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace presentation.desktopApp.helper {
    public class ClipboardEventArgs: EventArgs {
        public string ClipboardText { get; set; }

        public ClipboardEventArgs(string clipboardText) {
            ClipboardText = clipboardText;
        }
    }
    public class ExtendedTextBox: TextBox {
        public event EventHandler<ClipboardEventArgs> Pasted;

        private const int WM_PASTE = 0x0302;
        protected override void WndProc(ref Message m) {
            if(m.Msg == WM_PASTE) {
                var evt = Pasted;
                if(evt != null) {
                    evt(this, new ClipboardEventArgs(Clipboard.GetText()));
                    // don't let the base control handle the event again
                    return;
                }
            }

            base.WndProc(ref m);
        }
    }
}
