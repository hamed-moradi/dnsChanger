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

namespace IPComboBox {
    public partial class IPComboBox: UserControl {
        #region ctor
        public IPComboBox() {
            InitializeComponent();

            EnabledChanged += This_EnabledChanged;

            cmbIPs.SelectedIndexChanged += CmbIPs_SelectedIndexChanged;
        }

        private void This_EnabledChanged(object sender, EventArgs e) {
            var ipComboBox = (IPComboBox)sender;

            cmbIPs.Enabled = ipComboBox.Enabled;
            ipTextBox.Enabled = ipComboBox.Enabled;
        }
        #endregion

        #region events

        private void CmbIPs_SelectedIndexChanged(object sender, EventArgs e) {
            if(cmbIPs.SelectedItem != null) {
                var isValidIP = IPAddress.TryParse(cmbIPs.SelectedItem.ToString(), out var ipAddress);
                if(isValidIP) {
                    Value = ipAddress.ToString();
                }
            }
        }
        #endregion

        public string Value {
            get {
                return ipTextBox.Value;
            }
            set {
                ipTextBox.Value = value;
            }
        }

        public ComboBox.ObjectCollection Items {
            get { return cmbIPs.Items; }
        }
    }
}
