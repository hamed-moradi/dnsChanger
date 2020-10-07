using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Presentation.DesktopApp.Helper;
using System.Net;
using Presentation.DesktopApp;

namespace IPTextBox {
    public partial class IPTextBox: UserControl {
        #region ctor
        public IPTextBox() {
            InitializeComponent();

            txtSegment1.KeyDown += TxtSegment_KeyDown;
            txtSegment2.KeyDown += TxtSegment_KeyDown;
            txtSegment3.KeyDown += TxtSegment_KeyDown;
            txtSegment4.KeyDown += TxtSegment_KeyDown;

            txtSegment1.KeyUp += TxtSegment_KeyUp;
            txtSegment2.KeyUp += TxtSegment_KeyUp;
            txtSegment3.KeyUp += TxtSegment_KeyUp;
            txtSegment4.KeyUp += TxtSegment_KeyUp;

            txtSegment1.KeyPress += TxtSegment_KeyPress;
            txtSegment2.KeyPress += TxtSegment_KeyPress;
            txtSegment3.KeyPress += TxtSegment_KeyPress;
            txtSegment4.KeyPress += TxtSegment_KeyPress;

            txtSegment1.Pasted += TxtSegment_Pasted;
            txtSegment2.Pasted += TxtSegment_Pasted;
            txtSegment3.Pasted += TxtSegment_Pasted;
            txtSegment4.Pasted += TxtSegment_Pasted;

            EnabledChanged += This_EnabledChanged;
        }

        private void This_EnabledChanged(object sender, EventArgs e) {
            var ipTextBox = (IPTextBox)sender;

            txtSegment1.Enabled = ipTextBox.Enabled;
            txtSegment2.Enabled = ipTextBox.Enabled;
            txtSegment3.Enabled = ipTextBox.Enabled;
            txtSegment4.Enabled = ipTextBox.Enabled;

            lblDot1.Enabled = ipTextBox.Enabled;
            lblDot2.Enabled = ipTextBox.Enabled;
            lblDot3.Enabled = ipTextBox.Enabled;
        }
        #endregion

        #region private
        private void SetValueFromClipBoard() {
            if(Clipboard.ContainsText()) {
                var ip = Clipboard.GetText();
                if(!string.IsNullOrWhiteSpace(ip)) {
                    var isValidIP = IPAddress.TryParse(ip, out var ipAddress);
                    if(isValidIP) {
                        Value = ipAddress.ToString();
                    }
                }
            }
        }
        #endregion

        #region events
        private void TxtSegment_KeyDown(object sender, KeyEventArgs e) {
            var textBox = (TextBox)sender;
            var selectedTag = textBox.Tag.ToInt();

            if(e.KeyCode == Keys.Left) {
                if(selectedTag > 1 && textBox.SelectionStart == 0) {
                    SendKeys.Send("+{TAB}");
                }
            }
            else if(e.KeyCode == Keys.Right) {
                if(selectedTag < 4 && textBox.SelectionStart == textBox.TextLength) {
                    SendKeys.Send("{TAB}");
                }
            }
        }

        private void TxtSegment_KeyUp(object sender, KeyEventArgs e) {
            if(e.Control && e.KeyCode == Keys.V) {
                SetValueFromClipBoard();
            }
        }

        private void TxtSegment_KeyPress(object sender, KeyPressEventArgs e) {
            var keyCode = e.KeyChar.ToKeyCode();
            var textBox = (TextBox)sender;
            var selectedTag = textBox.Tag.ToInt();

            if((keyCode >= Keys.D0 && keyCode <= Keys.D9) || (keyCode >= Keys.NumPad0 && keyCode <= Keys.NumPad9)) {
                if(textBox.TextLength == 3) {
                    e.Handled = true;
                    return;
                }
                if(selectedTag < 4 && textBox.TextLength >= 2) {
                    SendKeys.Send("{TAB}");
                }
                var isValidNumber = int.TryParse($"{textBox.Text}{e.KeyChar}", out int segment);
                if(isValidNumber) {
                    if(segment > 255) {
                        textBox.Text = "255";
                        textBox.SelectionStart = 3;
                    }
                    else {
                        e.Handled = true;
                        textBox.Text = segment.ToString();
                        textBox.SelectionStart = textBox.TextLength;
                    }
                }
            }
            else if(keyCode == Keys.Back) {
                if(selectedTag > 1 && textBox.SelectionStart <= 0) {
                    SendKeys.Send("+{TAB}");
                }
            }
            else if(keyCode == Keys.Decimal || keyCode == Keys.OemPeriod) {
                e.Handled = true;
                if(selectedTag < 4 && textBox.TextLength > 0) {
                    SendKeys.Send("{TAB}");
                }
            }
            else {
                e.Handled = true;
            }
        }

        private void TxtSegment_Pasted(object sender, ClipboardEventArgs e) {
            SetValueFromClipBoard();
        }
        #endregion

        public string Value {
            get {
                return $"{txtSegment1.Text}.{txtSegment2.Text}.{txtSegment3.Text}.{txtSegment4.Text}";
            }
            set {
                var ipSegments = value.Split('.');
                txtSegment1.Text = ipSegments[0];
                txtSegment2.Text = ipSegments[1];
                txtSegment3.Text = ipSegments[2];
                txtSegment4.Text = ipSegments[3];
            }
        }
    }
}
